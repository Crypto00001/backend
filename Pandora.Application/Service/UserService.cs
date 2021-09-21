using Pandora.Application.Command.Users;
using Pandora.Domain.Domain;
using Pandora.Domain.Repository;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using Pandora.Application.Contract;
using System;
using Pandora.Application.ViewModel;

namespace Pandora.Application.Service
{
    public class UserService : IUserService
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
            return _UserRepository.GetAll();
        }

        public void Remove(RemoveUserCommand command)
        {
            _UserRepository.Remove(command.Id);
        }

        public UserViewModel Update(UpdateUserCommand command, Guid userId)
        {
            User user = _UserRepository.Get(userId);

            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Country = command.Country;
            user.Email = command.Email;

            _UserRepository.Update(user);
            return new UserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country
            };
        }

        public void UpdatePassword(UpdatePasswordUserCommand command, Guid userId)
        {
            User user = _UserRepository.Get(userId);

            if (!BCryptNet.Verify(command.OldPassword, user.PasswordHash))
                throw new AppException("OldPassword is incorrect");

            user.PasswordHash = BCryptNet.HashPassword(command.NewPassword);

            _UserRepository.Update(user);
        }
    }
}
