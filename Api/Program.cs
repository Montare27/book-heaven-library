using business;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using persistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(x => 
    x.AddPolicy("Frontend", policyBuilder => 
        policyBuilder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod()));

builder.Services.AddAuthentication(authBuilder => {
        authBuilder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        authBuilder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authBuilder.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwtOptions => {
        jwtOptions.RequireHttpsMetadata = false;
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"]!,
            ValidAudience = builder.Configuration["Jwt:Audience"]!,
            IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
        };
});

builder.Services.AddAuthorization();

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("Books", new OpenApiInfo{Title = "Books", Version = "v1"});
    c.SwaggerDoc("Authors", new OpenApiInfo{Title = "Authors", Version = "v2"});
    c.SwaggerDoc("Genres", new OpenApiInfo{Title = "Genres", Version = "v3"});
    c.SwaggerDoc("Reviews", new OpenApiInfo{Title = "Reviews", Version = "v4"});
    c.SwaggerDoc("SaveList", new OpenApiInfo{Title = "SaveList", Version = "v5"});
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions(){
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
});

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
    c => {
        c.SwaggerEndpoint("/swagger/Books/swagger.json", "Books");
        c.SwaggerEndpoint("/swagger/Authors/swagger.json", "Authors");
        c.SwaggerEndpoint("/swagger/Genres/swagger.json", "Genres");
        c.SwaggerEndpoint("/swagger/Reviews/swagger.json", "Reviews");
        c.SwaggerEndpoint("/swagger/SaveList/swagger.json", "SaveList");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
