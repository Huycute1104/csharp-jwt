using AutoMapper.Execution;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.DependencyInjection;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Helper;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Models;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Service;
using SE160548_IdetityAjaxASP.NETCoreWebAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your dependencies
builder.Services.AddDatabase();
builder.Services.addUnitOfWork();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

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
