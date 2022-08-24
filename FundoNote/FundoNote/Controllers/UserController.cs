using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost]
        [Route("Register")]
        public ActionResult Registration(UserRegistrationModel userRegistration)
        {
            try
            {
                var result = userBL.Register(userRegistration);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Not Successfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(UserLoginModel userLogin)
        {
            try
            {
                var result = userBL.Login(userLogin);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Not Successfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(string Email)
        {
            try
            {
                var result = userBL.ForgotPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Password Reset Link Send Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Send Password Reset Link" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]

        public ActionResult ResetPassword(string password, string newPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                if (userBL.ResetPassword(email, password, newPassword))
                    return this.Ok(new { Success = true, message = "Password updated sucessfully" });
                else
                    return this.BadRequest(new { Success = false, message = "Unable to reset password. Please try again!" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
