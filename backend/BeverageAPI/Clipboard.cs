//var builder = WebApplication.CreateBuilder(args);

//Add JWT Authentication
//var jwtSecretKey = "your-very-strong-secret"; // Use a strong secret key and store it securely
//var key = Encoding.ASCII.GetBytes(jwtSecretKey);

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false
//    };
//});

//builder.Services.AddControllers();

//var connectionString = builder.Configuration.GetConnectionString("BeverageDatabase");
//builder.Services.AddDbContext<BeverageContext>(options =>
//    options.UseMySql(connectionString, new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))));

//builder.Services.AddAutoMapper(typeof(BeverageProfile), typeof(UserProfile), typeof(CustomerProfile), typeof(OrderProfile));

//builder.Services.AddScoped<AdminService>();
//builder.Services.AddScoped<AnalyticsService>();
//builder.Services.AddScoped<AuthService>();
//builder.Services.AddScoped<TokenService>();
//builder.Services.AddScoped<DataSeeder>();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    var jwtKey = builder.Configuration["Jwt:Key"];
//    var jwtSettings = builder.Configuration.GetSection("Jwt");
//    if (string.IsNullOrEmpty(jwtKey))
//    {
//        throw new InvalidOperationException("JWT Key is not configured.");
//    }

//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
//        ClockSkew = TimeSpan.Zero // Reduce the default clock skew
//    };
//});

//var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
//    seeder.SeedData();
//}

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseCors("AllowAll");
//app.UseHttpsRedirection(); // Ensure HTTPS is used
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();

//app.Run();




//private string GenerateJwtToken(User user)
//{
//    var tokenHandler = new JwtSecurityTokenHandler();
//    var secret = _configuration["Jwt:Secret"];
//    if (string.IsNullOrEmpty(secret))
//    {
//        Console.WriteLine("JWT Secret is missing or null in AuthService.");
//        throw new InvalidOperationException("JWT Secret is not configured.");
//    }
//    var key = Encoding.ASCII.GetBytes(secret);
//    var tokenDescriptor = new SecurityTokenDescriptor
//    {
//        Subject = new ClaimsIdentity(
//        [
//            new Claim(ClaimTypes.Name, user.Email),
//            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
//        ]),
//        Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"] ?? "720")),
//        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
//        Issuer = _configuration["Jwt:Issuer"],
//        Audience = _configuration["Jwt:Audience"]
//    };

//    var token = tokenHandler.CreateToken(tokenDescriptor);
//    return tokenHandler.WriteToken(token);
//}