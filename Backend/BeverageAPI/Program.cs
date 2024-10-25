var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddUserSecrets<Program>()  // Ensure secrets.json is prioritized for dev environment
    .AddEnvironmentVariables();

// Retrieve the JWT settings from configuration (secrets.json or appsettings)
var jwtSettings = builder.Configuration.GetSection("Jwt") ?? throw new InvalidOperationException("JWT settings are not configured.");
var keyString = jwtSettings["Key"] ?? string.Empty;
if (string.IsNullOrEmpty(keyString))
{
    throw new InvalidOperationException("JWT Secret-Key is not configured.");
}
var key = Encoding.ASCII.GetBytes(keyString);

// Configure JWT authentication
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
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured"),    // Validate the issuer
        ValidAudience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT Audience is not configured"), // Validate the audience
        IssuerSigningKey = new SymmetricSecurityKey(key), // Sign key
        ClockSkew = TimeSpan.Zero  // Expire exactly after the set time
    };
});

var connectionString = builder.Configuration.GetConnectionString("BeverageDatabase");
builder.Services.AddDbContext<BeverageContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))));

builder.Services.AddAutoMapper(typeof(BeverageProfile), typeof(UserProfile), typeof(CustomerProfile), typeof(OrderProfile), typeof(OrderItemProfile));

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<AnalyticsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();