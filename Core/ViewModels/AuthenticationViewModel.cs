using Core.Entities;

namespace Core.ViewModels
{
    public sealed class AuthenticationViewModel
    {
        public AuthenticationViewModel(SystemUser user, string token)
        {
            Username = user.Username;
            Name = user.Name;
            Lastname = user.Lastname;
            Email = user.Email;
            Token = token;
        }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
