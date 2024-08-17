using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CustomerService.Models;
using CustomerService.Service.Interface;
using Microsoft.IdentityModel.Tokens;

namespace CustomerService.Service.Implementation;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration; 
    } 
        
        
    public string GenerateJwtToken(Customer customer)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        if (signingKey == null)
        {
            throw new InvalidOperationException("JWT Key is missing from configuration");
        }

        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, customer.Email),
            new Claim(ClaimTypes.MobilePhone, customer.Phone) 
        };
        
        var token = new JwtSecurityToken(
            
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}