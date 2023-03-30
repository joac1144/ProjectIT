using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ProjectIT.Client.Components.Login;

public partial class LoginDisplay
{
    public void BeginLogOut()
    {
        Navigation.NavigateToLogout("authentication/logout");
    }

        public void BeginLogIn()
    {
        Navigation.NavigateToLogin("authentication/login");
    }
}