using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var labelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                labelList = await fundoContext.LabelTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(labelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(labelList);
        }
    }
}
