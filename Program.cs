using MessagingApp.Configurations;
using MessagingApp.Context;
using MessagingApp.Services.Users;
using MessagingApp.Services.Users.Passwords;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// add configurations
builder.Services.Configure<Argon2Settings>(builder.Configuration.GetSection("Argon2Settings"));

// add scoped services
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
