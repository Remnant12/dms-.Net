using System.IdentityModel.Tokens.Jwt;

namespace CustomerService.Service.Implementation;

public class JwtTokenDecode
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<JwtTokenDecode> _logger; 
    public JwtTokenDecode(IHttpContextAccessor httpContextAccessor, ILogger<JwtTokenDecode> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger; 
    }

    public string GetPhoneNumberFromToken()
    {
        var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        _logger.LogInformation("Token received: {Token}", token);
        if (string.IsNullOrEmpty(token)) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        var phoneNumberClaim = jwtToken?.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone")?.Value;

        return phoneNumberClaim;
    }
}