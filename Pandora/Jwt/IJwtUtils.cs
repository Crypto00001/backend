using Pandora.Domain.Domain;
using System;

namespace Pandora.Jwt
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public Guid? ValidateToken(string token);
    }
}
