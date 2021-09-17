using System.ComponentModel.DataAnnotations;

namespace Pandora.Application.Command.Users
{
    public class AuthenticateResponse
    {
        public string Email { get; set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Token { get; internal set; }
    }
}
