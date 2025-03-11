using System.Text;
using MessagingApp.Configurations;
using MessagingApp.Context;
using MessagingApp.Events;
using MessagingApp.Models.Responses;
using MessagingApp.Services.Auth;
using MessagingApp.Services.Token;
using MessagingApp.Services.Users;
using MessagingApp.Services.Users.Passwords;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// add configurations
builder.Services.Configure<Argon2Settings>(builder.Configuration.GetSection("Argon2Settings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<CookieSettings>(builder.Configuration.GetSection("CookieSettings"));

// add http context
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// add logger
builder.Logging.AddConsole();

// add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    JwtSettings? jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
    CookieSettings? cookieSettings = builder.Configuration.GetSection("CookieSettings").Get<CookieSettings>();

    options.Audience = jwtSettings!.ValidAudience;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.ValidIssuer,
        ValidAudience = jwtSettings.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessKey))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = async context =>
        {
            ITokenService tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
            string? accessToken = context.Request.Cookies[cookieSettings!.CookieName];
            string? refreshToken = context.Request.Cookies[cookieSettings.CookieNameRefresh];

            if (!string.IsNullOrEmpty(accessToken))
            {
                bool isAccessTokenValid = await tokenService.ValidateTokenAsync(accessToken);
                if (!string.IsNullOrEmpty(refreshToken) && !isAccessTokenValid)
                {
                    bool isRefreshTokenValid = await tokenService.ValidateTokenAsync(refreshToken, true);
                    // call regenerate token
                    TokenResponse tokenResponse = tokenService.RegenerateTokens(refreshToken);
                    tokenService.SetTokenToCookie(tokenResponse.AccessToken);
                    tokenService.SetTokenToCookie(tokenResponse.RefreshToken, true);
                }
                else
                {
                    context.Token = accessToken;
                }
            }
        },
    };

    options.MapInboundClaims = false;
});

// add scoped services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

// add autoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure dbContext
string? connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
if (connectionString != null)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}
else
{
    throw new Exception("Connection string not found");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
