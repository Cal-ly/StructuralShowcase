global using BookLibrary; // Global usings for shared libraries
global using BookAPI.Data; // Data layer
global using BookAPI.Repositories; // Repository layer

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;

// Step 1: Configure App Configuration
// Add JSON files for configuration (appsettings.json and environment-specific settings)
// Add environment variables for flexible configuration (e.g., in Azure)
// Add User Secrets for local development if in Development environment
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

if (environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Step 2: Configure Services
// Add Controllers to the service container
builder.Services.AddControllers();

// Enable Swagger for API documentation in development mode
// Learn more at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS to allow any origin, method, and header for API access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Step 3: Configure Database (MySQL)
// Retrieve the connection string (from appsettings, user secrets, or Azure environment)
// Use MySQL with Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register the repository service for dependency injection
builder.Services.AddScoped<BookRepository>();

var app = builder.Build();

// Step 4: Configure the HTTP Request Pipeline

// Enable Swagger UI for API testing in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforce HTTPS
app.UseHttpsRedirection();

// Enable CORS with the "AllowAll" policy to handle cross-origin requests
app.UseRouting();
app.UseCors("AllowAll");

// Enable Authorization (if applicable, you can add authentication services if needed)
app.UseAuthorization();

// Map the controllers to handle API requests
app.MapControllers();

// Run the application
app.Run();