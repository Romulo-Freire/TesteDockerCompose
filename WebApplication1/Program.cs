
using DependencyInjection;
using Domain.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes("43e4dbf0-52ed-4203-895d-42b586496bd4");
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("user", policy => policy.RequireClaim("Store", "user"));
    options.AddPolicy("admin", policy => policy.RequireClaim("Store", "admin"));
});

builder.Services.AddControllers();

Initialize.Init(builder.Services);

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dataBase = scope.ServiceProvider.GetRequiredService<FirstDbContext>();
    dataBase.Database.Migrate();
}

app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader()
);

app.UseAuthentication();
app.MapControllers();

app.Run();

