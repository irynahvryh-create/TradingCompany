using TradingCompany.BL.Interfaces;

public class UserDebugViewModel
{
    public IAuthManager AuthManager { get; }
    public TradingCompany.DTO.User? CurrentUser => AuthManager.CurrentUser;

    public string IsAdminText =>
        CurrentUser == null ? "No user"
        : AuthManager.IsAdmin(CurrentUser) ? "YES"
        : "NO";

    public UserDebugViewModel(IAuthManager authManager)
    {
        AuthManager = authManager;
    }
}
