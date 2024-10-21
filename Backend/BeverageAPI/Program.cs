var builder = WebApplication.CreateBuilder(args);

// Retrieve the JWT settings from configuration (secrets.json or appsettings)
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings?["Key"] ?? throw new InvalidOperationException("JWT Key is not configured."));

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
        ValidIssuer = jwtSettings["Issuer"],    // Validate the issuer
        ValidAudience = jwtSettings["Audience"], // Validate the audience
        IssuerSigningKey = new SymmetricSecurityKey(key), // Sign key
        ClockSkew = TimeSpan.Zero  // Expire exactly after the set time
    };
});


var connectionString = builder.Configuration.GetConnectionString("BeverageDatabase");
builder.Services.AddDbContext<BeverageContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))));

builder.Services.AddAutoMapper(typeof(BeverageProfile), typeof(UserProfile), typeof(CustomerProfile), typeof(OrderProfile), typeof(OrderItemProfile));

builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<AnalyticsService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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