﻿using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL:IUserRL
    {
        private readonly FundoContext fundoContext;
        public UserRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public UserEntity Register(UserRegistrationModel userRegistration)
        {
            try
            {
                UserEntity user = new UserEntity();
                user.FirstName = userRegistration.FirstName;
                user.LastName = userRegistration.LastName;  
                user.Email = userRegistration.Email;    
                user.Password = userRegistration.Password;
                fundoContext.UserTable.Add(user);
                int res = fundoContext.SaveChanges();
                if (res > 0)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
