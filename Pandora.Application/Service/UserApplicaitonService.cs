using Pandora.Application.Command.Users;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using System.Collections.Generic;

namespace Pandora.Application.Service
{
    public class UserApplicaitonService
    {
        private readonly UserRepository _UserRepository;

        public UserApplicaitonService(UserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public void Add(CreateUserCommand command)
        {
            _UserRepository.Add(command.ToUser());
        }

        public User Get(int UserId)
        {
            return _UserRepository.Get(UserId);
        }

        public List<User> Get()
        {
            return _UserRepository.Get();
        }

        public void Remove(RemoveUserCommand command)
        {
            _UserRepository.Remove(command.Id);
        }
    }
}
