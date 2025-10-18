using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using static ASI.Basecode.Resources.Constants.Enums;

namespace ASI.Basecode.Services.Interfaces
{
    public interface IUserService
    {
        // Authentication methods
        LoginResult AuthenticateUser(string userid, string password, ref User user);
        UserViewModel AuthenticateUser(string email, string password);
        
        // Query methods
        UserViewModel GetUserByEmail(string email);
        UserViewModel GetUserByUsername(string username);
        UserViewModel GetUserById(Guid id);
        IEnumerable<UserViewModel> GetAllUsers();
        
        // CRUD methods
        UserViewModel CreateUser(dynamic model);
        void UpdateUser(UserViewModel model);
        void DeleteUser(Guid id);
    }
}
