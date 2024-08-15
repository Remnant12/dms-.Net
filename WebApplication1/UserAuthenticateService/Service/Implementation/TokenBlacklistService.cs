using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using UserAuthenticateService.Service.Implementation;
using UserService.Data;

namespace UserAuthenticateService.Service;

public class DatabaseTokenBlacklistService : ITokenBlacklistService
{
    private readonly UserDbContext _context;

    public DatabaseTokenBlacklistService(UserDbContext context)
    {
        _context = context;
    }

    public void BlacklistToken(string token)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtSecurityTokenHandler.ReadJwtToken(token);

        var blacklistedToken = new TokenBlacklist
        {
            Token = token,
            ExpirationDate = jwtToken.ValidTo
        };

        _context.TokenBlacklist.Add(blacklistedToken);
        _context.SaveChanges();
    }

    public bool IsTokenBlacklisted(string token)
    {
        return _context.TokenBlacklist.Any(t => t.Token == token);
    }
}