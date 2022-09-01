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
        [Authorize]
        [HttpGet]
        [Route("Retrieve")]
        public ActionResult Retrieve(long notesID)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = collaborationBL.Retrieve(UserID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes is Retrieved Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Retrieved Notes" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(long CollaboratorID)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = collaborationBL.Delete(CollaboratorID);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Collaboration Deleted Successful" });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Collaboration is Unable to Delete" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
