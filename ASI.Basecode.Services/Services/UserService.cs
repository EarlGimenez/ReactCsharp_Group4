using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Manager;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using static ASI.Basecode.Resources.Constants.Enums;

namespace ASI.Basecode.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public LoginResult AuthenticateUser(string userId, string password, ref User user)
        {
            user = new User();
            var passwordKey = PasswordManager.EncryptPassword(password);
            user = _repository.GetUsers().Where(x => x.UserId == userId &&
                                                     x.Password == passwordKey).FirstOrDefault();

            return user != null ? LoginResult.Success : LoginResult.Failed;
        }

        public UserViewModel GetUserByEmail(string email)
        {
            var user = _repository.GetUsers().FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            return new UserViewModel
            {
                UserId = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Company = user.Company,
                IsActive = user.IsActive
            };
        }

        public UserViewModel GetUserByUsername(string username)
        {
            var user = _repository.GetUsers().FirstOrDefault(u => u.Username == username);
            if (user == null) return null;

            return new UserViewModel
            {
                UserId = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Company = user.Company,
                IsActive = user.IsActive
            };
        }

        public UserViewModel GetUserById(Guid id)
        {
            var user = _repository.GetUsers().FirstOrDefault(u => u.UserID == id);
            if (user == null) return null;

            return new UserViewModel
            {
                UserId = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Company = user.Company,
                IsActive = user.IsActive
            };
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var users = _repository.GetUsers();
            return users.Select(u => new UserViewModel
            {
                UserId = u.UserID,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Username = u.Username,
                Email = u.Email,
                Company = u.Company,
                IsActive = u.IsActive
            }).ToList();
        }

        public UserViewModel CreateUser(dynamic model)
        {
            // Hash the password using BCrypt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new User
            {
                UserID = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Email = model.Email,
                PasswordHash = hashedPassword,
                Company = model.Company ?? "",
                IsActive = true,
                CreatedAt = DateTimeOffset.Now,
                CreatedBy = model.Email,
                ModifiedAt = DateTimeOffset.Now,
                ModifiedBy = model.Email
            };

            _repository.AddUser(user);

            return new UserViewModel
            {
                UserId = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Company = user.Company,
                IsActive = user.IsActive
            };
        }

        public void UpdateUser(UserViewModel model)
        {
            var user = _repository.GetUsers().FirstOrDefault(u => u.UserID == model.UserId);
            if (user == null)
                throw new Exception("User not found");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Username = model.Username;
            user.Email = model.Email;
            user.Company = model.Company;
            user.IsActive = model.IsActive;
            user.ModifiedAt = DateTimeOffset.Now;
            user.ModifiedBy = model.Email;

            // If password is being changed
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            }

            _repository.UpdateUser(user);
        }

        public void DeleteUser(Guid id)
        {
            var user = _repository.GetUsers().FirstOrDefault(u => u.UserID == id);
            if (user == null)
                throw new Exception("User not found");

            _repository.DeleteUser(user);
        }

        public UserViewModel AuthenticateUser(string email, string password)
        {
            var user = _repository.GetUsers().FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            // Verify password using BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isPasswordValid) return null;

            return new UserViewModel
            {
                UserId = user.UserID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Company = user.Company,
                IsActive = user.IsActive
            };
        }
    }
}
