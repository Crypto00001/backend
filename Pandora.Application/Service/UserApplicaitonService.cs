using Pandora.Application.Command.Users;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
using System;

namespace Pandora.Application.Service
{
    public class UserService: IUserService
    {
        private readonly UserRepository _UserRepository;
        public UserService(UserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public void Add(CreateUserCommand command)
        {
            if (_UserRepository.HasUserByEmail(command.Email))
                throw new AppException("Email '" + command.Email + "' is already taken");

            User user = command.ToUser();
            user.PasswordHash = BCryptNet.HashPassword(command.Password);

            _UserRepository.Add(user);
        }
        public User Authenticate(LoginCommand model)
        {
            var user = _UserRepository.GetUserByEmail(model.Email);

            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            return user;
        }
        public User GetById(Guid UserId)
        {
            return _UserRepository.Get(UserId);
        }

        public List<User> GetAll()
        {
            return _UserRepository.Get();
        }

        public void Remove(RemoveUserCommand command)
        {
            _UserRepository.Remove(command.Id);
        }
    }
}
