using Pandora.Application.Command.Users;
using Pandora.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.Contract
{
    public interface IUserService
    {
        void Add(CreateUserCommand command);
        User Authenticate(LoginCommand model);
        User GetById(Guid userId);
        List<User> GetAll();
        void Remove(RemoveUserCommand command);
    }
}
