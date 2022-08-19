using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("Register")]
        public ActionResult Registration(UserRegistrationModel userRegistration)
        {
            try
            {
                var result = userBL.Register(userRegistration);
                if(result != null)
                {
                    return Ok(new {success = true,message = "Registration Successfull",data = result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration is Not Successfull"});
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
