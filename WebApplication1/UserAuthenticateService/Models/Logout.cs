
public class TokenBlacklist
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime BlacklistedAt { get; set; }
}
