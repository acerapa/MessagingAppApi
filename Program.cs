using MessagingApp.Configurations;
using MessagingApp.Context;
using MessagingApp.Events;
using MessagingApp.Services.Auth;
using MessagingApp.Services.Users;
using MessagingApp.Services.Users.Passwords;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// add configurations
builder.Services.Configure<Argon2Settings>(builder.Configuration.GetSection("Argon2Settings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<CookieSettings>(builder.Configuration.GetSection("CookieSettings"));

// add http context
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// add authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options => {
    CookieSettings? cookieSettings = builder.Configuration.GetSection("CookieSettings").Get<CookieSettings>();
    
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.Name = cookieSettings!.CookieName;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.MaxAge =  TimeSpan.FromMinutes(cookieSettings.MaxAge);
    options.ExpireTimeSpan = TimeSpan.FromMinutes(cookieSettings.MaxAge);
    options.EventsType = typeof(JwtCookieAuthenticationEvents);
});


// add scoped services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserServices>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
