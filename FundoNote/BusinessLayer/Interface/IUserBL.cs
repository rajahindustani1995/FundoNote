using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity Register(UserRegistrationModel userRegistration);
        public string Login(UserLoginModel userLogin);
        public string ForgotPassword(string Email);
        public bool ResetPassword(string email, string password, string newPassword);
    }
}
