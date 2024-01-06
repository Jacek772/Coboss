using Coboss.Application;
using Coboss.Application.Configuration;
using Coboss.Application.Seeds;
using Coboss.Application.Seeds.abstracts;
using Coboss.Middlewares;
using Coboss.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder
            .WithOrigins("*")
            .WithExposedHeaders("Token-Expired")
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
TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = authenticationConfiguration.JwtIssuer,
    ValidAudience = authenticationConfiguration.JwtAudience,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.JwtKey)),
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = tokenValidationParameters;
    cfg.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if(context.Exception is SecurityTokenExpiredException)
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
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
    app.UseDeveloperExceptionPage();
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
    ISeed globalSettingsSeed = scope.ServiceProvider.GetService<GlobalSettingsSeed>();
    await globalSettingsSeed.Seed();

    ISeed rolesSeed = scope.ServiceProvider.GetService<RolesSeed>();
    await rolesSeed.Seed();

    ISeed usersSeed = scope.ServiceProvider.GetService<UsersSeed>();
    await usersSeed.Seed();

    ISeed employeesSeed = scope.ServiceProvider.GetService<EmployeesSeed>();
    await employeesSeed.Seed();

    ISeed projectsSeed = scope.ServiceProvider.GetService<ProjectsSeed>();
    await projectsSeed.Seed();
}

app.Run();



