using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
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
                    return this.NotFound(new { success = false, message = "Unable to Retrieve data" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize]
        [HttpPut]
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
                    return this.NotFound(new { success = false, message = "Unable to Update Data" });
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
        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public ActionResult Archive(long NotesID)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = notesBL.Archive(NotesID);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note is Achived Successful", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Note Unable to Achive" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public ActionResult Pin(long NotesID)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = notesBL.Pin(NotesID);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note is Pinned Successful", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Note Unable to Pin" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public ActionResult Trash(long NotesID)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = notesBL.Trash(NotesID);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note is Trashed Successful", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Note Unable to Trash" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpPut]
        [Route("Image")]
        public IActionResult Image(IFormFile image, long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserID").Value);
                var result = notesBL.Image(image, NoteID, userID);
                if (result != null)
                {
                    return Ok(new { success = true, message = result, data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to upload image." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Color")]
        public ActionResult ChoiceColor(long NotesID, string Color)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.All(x => x.Type == "UserID"));
                var result = notesBL.ChoiceColor(NotesID, Color);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Color Changed Successfully", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Unable to Change color" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
