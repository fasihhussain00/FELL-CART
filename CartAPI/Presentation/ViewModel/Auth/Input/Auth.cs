namespace CartAPI.Presentation.ViewModel.Auth.Input
{
    public class Auth
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public Domain.Model.Auth ToDomain()
        {
            return new Domain.Model.Auth
            {
                Email = Email,
                Password = Password
            };
        }
    }
}