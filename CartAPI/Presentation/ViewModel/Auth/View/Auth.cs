using System;

namespace CartAPI.Presentation.ViewModel.Auth.View
{
    public class Auth
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}