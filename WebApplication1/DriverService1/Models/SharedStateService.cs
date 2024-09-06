namespace DriverService1.Models;

public class SharedStateService
{
    private int? _userId;
    private readonly object _lock = new object();
    public int? UserId
    {
        get
        {
            lock (_lock)
            {
                return _userId;
            }
        }
        set
        {
            lock (_lock)
            {
                _userId = value;
            }
        }
    }
}