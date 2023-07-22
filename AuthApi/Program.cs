using AuthApi;
using AuthApi.Data;
using AuthApi.Extensions;
using AuthApi.Models;
using AuthApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(x => 
    x.AddPolicy("Frontend", policyBuilder => 
        policyBuilder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod()));


builder.Services.AddDbContext<UserDbContext>(dbOptions => {
    dbOptions.UseNpgsql(builder.Configuration.GetConnectionString("UserDb"));
});

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddAuthentication(authBuilder => {
        authBuilder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        authBuilder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtAuthentication(
    builder.Configuration["Jwt:Issuer"]!,
    builder.Configuration["Jwt:Secret"]!,
    new[]{builder.Configuration["Jwt:Audiences"]!});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions => {
    identityOptions.Password.RequireDigit = true;
    
})
.AddEntityFrameworkStores<UserDbContext>();

builder.Services.ConfigureApplicationCookie(config => {
    config.Cookie.HttpOnly = true;
    config.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    config.Cookie.Name = "BookLibrary.AuthApi.Cookie";
    config.LoginPath = "/Account/Login";
    config.LogoutPath = "/Account/Logout";
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
