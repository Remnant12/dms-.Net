// using System.Security.Claims;
// using System.Text;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.IdentityModel.Tokens;
// using WebApplication1.UserService.Models;
// using Microsoft.IdentityModel.Tokens;
//
// [Route("api/[controller]")]
// [ApiController]
// public class AuthController : ControllerBase
// {
//     private readonly IUserRepository _userRepository;
//     private readonly IPasswordHasher<User> _passwordHasher;
//     private readonly JwtSettings _jwtSettings;
//
//     public AuthController(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
//     {
//         _userRepository = userRepository;
//         _passwordHasher = passwordHasher;
//         _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
//     }
//
//     [HttpPost("login")]
//     public async Task<IActionResult> Login(Login model)
//     {
//         var user = await _userRepository.GetUserByUsernameAsync(model.Username);
//
//         if (user == null || !_passwordHasher.VerifyHashedPassword(user, model.Password))
//         {
//             return Unauthorized();
//         }
//
//         var token = GenerateJwtToken(user);
//         return Ok(new { token });
//     }
//
//     private string GenerateJwtToken(User user)
//     {
//         var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
//         var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);   
//
//
//         var claims = new[]
//         {
//             new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
//             new Claim(JwtRegisteredClaimNames.Email,   
//                 user.Email),
//             // Add other claims as needed
//         };
//
//         var token = new JwtSecurityToken(
//             _jwtSettings.Issuer,
//             _jwtSettings.Audience,
//             claims,
//             expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes),   
//
//             signingCredentials: credentials
//         );
//
//         return new JwtSecurityTokenHandler().WriteToken(token);
//     }
//
// }