using CMSUI.Models;
using CMSUI.Services;
using CMSXDAO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var connPostgres = builder.Configuration.GetConnectionString("NPGSQL");
var connSqlServer = builder.Configuration.GetConnectionString("SqlServer");
if (!string.IsNullOrWhiteSpace(connPostgres))
    builder.Services.AddDbContext<CmsxDbContext>(o => o.UseNpgsql(connPostgres));
else
    builder.Services.AddDbContext<CmsxDbContext>(o => o.UseSqlServer(connSqlServer));
builder.Services.AddSwaggerGen();
builder.Services.AddCMSXDAL();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IAgentIAFactory, AgentIAFactory>();

var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
