using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;
using System;
using System.Linq;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : Controller
    {

        private readonly ILabelBL labelBL;
        private readonly IMemoryCache memoryCache;
        private readonly FundoContext fundoContext;
        private readonly IDistributedCache distributedCache;

        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, FundoContext fundoContext, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.fundoContext = fundoContext;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(long notesID, string labelname)
        {
            long UserId = Convert.ToInt64(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            var result = labelBL.Create(UserId, notesID, labelname);

            if (result != null)
            {
                return Ok(new { success = true, message = "Label is Added Successfully", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Unable to Add Label" });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Retrieve")]
        public ActionResult Retrieve(long LabelID)
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
        [HttpPost]
        [Route("Update")]
        public ActionResult UpdateLabel(long labelID, string labelname)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = labelBL.UpdateLabel(labelID, labelname);
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
