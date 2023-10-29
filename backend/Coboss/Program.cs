using Coboss.Application;
using Coboss.Application.Configuration;
using Coboss.Application.Seeds;
using Coboss.Application.Seeds.abstracts;
using Coboss.Middlewares;
using Coboss.Persistance;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder
            .WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configuration
DatabaseConfiguration databaseConfiguration = new();
builder.Configuration.GetSection(nameof(DatabaseConfiguration)).Bind(databaseConfiguration);
builder.Services.AddSingleton(databaseConfiguration);

AuthenticationConfiguration authenticationConfiguration = new();
builder.Configuration.GetSection(nameof(AuthenticationConfiguration)).Bind(authenticationConfiguration);
builder.Services.AddSingleton(authenticationConfiguration);

// Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationConfiguration.JwtIssuer,
        ValidAudience = authenticationConfiguration.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.JwtKey))
    };
});

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddPersistence();
builder.Services.AddApplication();

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

if(builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coboss v1");
    });
}

// Middlewares
app.UseMiddleware<ErrorHandlingMiddleware>();

using (IServiceScope scope = app.Services.CreateScope())
{
    ApplicationDbContext dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
    if(!dbContext.Database.CanConnect())
    {
        throw new Exception("Connection string is incorrect or database not exists!");
    }

    // Seeds
    ISeed usersSeed = scope.ServiceProvider.GetService<UsersSeed>();
    await usersSeed.Seed();
}

app.Run();



