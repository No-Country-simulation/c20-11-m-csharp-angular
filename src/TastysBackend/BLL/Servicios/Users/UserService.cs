
using Tastys.Infrastructure;

public class UserServices
{
    private readonly TastysContext _userService;
    public UserServices(TastysContext tastysContext)
    {
        _userService = tastysContext;
    }
    
    
}