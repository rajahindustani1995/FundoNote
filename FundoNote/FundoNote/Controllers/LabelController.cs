using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : Controller
    {

        private readonly ILabelBL labelBL;

        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(LabelModel labelModel, long noteid)
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
            var result = labelBL.Create(labelModel, userid, noteid);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "label  Created Successful", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "label is not Created" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Retrieve")]
        public ActionResult Retrieve(long NotesID)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labelBL.Retrieve(UserID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label is Retrieved Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Retrieved Label" });
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
        public IActionResult Delete(long LabelID)
        {
            long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
            var result = labelBL.Delete(LabelID);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Label Delete Successful" });
            }
            else
            {
                return this.NotFound(new { success = false, message = "Label does not Delete" });
            }
        }
    }
}
