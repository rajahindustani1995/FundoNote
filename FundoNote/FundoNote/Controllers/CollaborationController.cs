using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaborationController : Controller
    {
        private readonly ICollaborationBL collaborationBL;
        public CollaborationController(ICollaborationBL collaborationBL)
        {
            this.collaborationBL = collaborationBL;
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(string Email, long notesID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID" ).Value);
                var result = collaborationBL.Create(Email, notesID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collab Created Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Create Collab" });
                }
            }
            catch (System.Exception e)
            {

                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }
    }
}
