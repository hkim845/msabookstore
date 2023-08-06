using Back_End.Contexts;
using Back_End.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//// Retrieve the configuration from appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<BookContext>(opt =>
    opt.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"))
);

builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"))
);

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<BookService>();

var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var issuer = "https://localhost:7206/";
var audience = "http://localhost:3000/";
var key = Encoding.ASCII.GetBytes("my-32-character-ultra-secure-and-ultra-long-secret");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();

// register the required Swagger services for NSwag
builder.Services.AddOpenApiDocument(document => 
{
    document.Title = "My Todo Api";
    document.Version = "v1";
    document.AddSecurity("Basic", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.Basic,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Input your username and password to access the API"
    });

    document.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor("Basic")
    );
});

var app = builder.Build();

app.UseCors(policy =>
{
    policy.WithOrigins("http://localhost:3000")
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Register the Swagger generator and the Swagger UI middlewares
    app.UseOpenApi();
    app.UseSwaggerUi3();    
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
