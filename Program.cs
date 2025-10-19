using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using BasetApi.Service;
using DotNetEnv;
using BasetApi.Extansions;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Configuration["Jwt:Key"] = Environment.GetEnvironmentVariable("JWT_KEY");
builder.Configuration["Jwt:Issuer"] = Environment.GetEnvironmentVariable("JWT_ISSUER");
builder.Configuration["Jwt:Audience"] = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
builder.Configuration["Jwt:ExpireDays"] = Environment.GetEnvironmentVariable("JWT_EXPIRE_DAYS");

builder.Configuration["SendGrid:ApiKey"] = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
builder.Configuration["SendGrid:FromEmail"] = Environment.GetEnvironmentVariable("SENDGRID_FROM_EMAIL");
builder.Configuration["SendGrid:FromName"] = Environment.GetEnvironmentVariable("SENDGRID_FROM_NAME");

builder.Services.AddControllers();
builder.Services.ConfigureControllersBased();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.AddAuthentication();
builder.Services.ConfigureSwaggerBased();
builder.Services.ConfigureCorsBased();
builder.Services.AddScoped<JwtService>();

var app = builder.Build();

app.MapGet("/ping", () => "pong");
app.UseSwagger();
app.ConfigureSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("AllowNext");
app.UseAuthentication();
await app.ConfigureAdminUser();
app.UseAuthorization();
app.MapControllers();
app.Run();
