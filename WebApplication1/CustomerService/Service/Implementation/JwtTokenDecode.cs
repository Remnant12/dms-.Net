using System.IdentityModel.Tokens.Jwt;

namespace CustomerService.Service.Implementation;

public class JwtTokenDecode
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtTokenDecode(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetPhoneNumberFromToken()
    {
        var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token)) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        var phoneNumberClaim = jwtToken?.Claims.FirstOrDefault(x => x.Type == "PhoneNumber")?.Value;

        return phoneNumberClaim;
    }
}