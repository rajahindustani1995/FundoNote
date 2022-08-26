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
    public class NotesController : Controller
    {
        private readonly INotesBL notesBL;
        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(NotesModel model)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Create(model, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes Created Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Create Notes" });
                }
            }
            catch (System.Exception e)
            {

                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Retrieve")]
        public ActionResult Retrieve(long NotesID)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.All(x => x.Type == "UserID"));
                var result = notesBL.Retrieve(NotesID);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "data Reterieve Successful", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "data not Reterieve" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize]
        [HttpPost]
        [Route("Update")]
        public ActionResult UpdateNote(NotesModel model, long NotesID)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.All(x => x.Type == "UserID"));
                var result = notesBL.UpdateNote(model, NotesID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Data Updated Successful", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Data NotUpdated" });
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [Authorize]
        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(long NotesID)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = notesBL.Delete(NotesID);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Deleted Successful" });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Note Unable to Delete" });
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
