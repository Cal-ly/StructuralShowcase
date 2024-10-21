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