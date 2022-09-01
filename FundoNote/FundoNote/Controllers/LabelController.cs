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

        //[Authorize]
        //[HttpPost]
        //[Route("Delete")]
        //public IActionResult Delete(long LabelID)
        //{
        //    long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
        //    var result = labelBL.Delete(LabelID);
        //    if (result != null)
        //    {
        //        return this.Ok(new { success = true, message = "Label Delete Successful" });
        //    }
        //    else
        //    {
        //        return this.NotFound(new { success = false, message = "Label does not Delete" });
        //    }
        //}
    }
}
