using Pandora.Application.Command.Users;
using Pandora.Application.ViewModel;
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
        Task CreateAsync(CreateUserCommand command);
        Task<UserViewModel> UpdateAsync(UpdateUserCommand command, Guid userId);
        Task UpdatePassword(UpdatePasswordUserCommand command, Guid userId);
        Task<User> Authenticate(LoginCommand model);
        Task<User> GetById(Guid userId);
        Task<List<User>> GetAll();
        Task Remove(RemoveUserCommand command);
    }
}
