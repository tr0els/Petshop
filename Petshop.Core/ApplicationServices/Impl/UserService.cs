using Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petshop.Core.DomainServices;
using Petshop.Core.Filter;

namespace Petshop.Core.ApplicationServices.Impl
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            this._userRepository = repository;
        }

        public List<User> ReadAll()
        {
            return _userRepository.ReadAll();
        }

        public User Read(int id)
        {
            if (id < 1) throw new ArgumentException($"Id must be a positive integer.");
            User user = _userRepository.Read(id);
            if (user is null) throw new KeyNotFoundException($"No record with id {id} was found.");
            return user;
        }

        public User Create(User user)
        {
            ValidateOwnerData(user);
            return _userRepository.Create(user);
        }

        private void ValidateOwnerData(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || user.Username.Length< 2) throw new ArgumentException("Username must be minimum 2 characters long.");
            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 6) throw new ArgumentException("Password must be minimum 6 characters long.");
        }
}
}