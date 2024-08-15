namespace UserAuthenticateService.Service.Implementation;

public interface ITokenBlacklistService
{
    void BlacklistToken(string token);
    bool IsTokenBlacklisted(string token);
}