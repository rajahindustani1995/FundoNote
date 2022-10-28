using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<LabelController> _logger;

        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, FundoContext fundoContext, IDistributedCache distributedCache, ILogger<LabelController> logger)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.fundoContext = fundoContext;
            this.distributedCache = distributedCache;
            this._logger = logger;
        }

        [Authorize]
        [HttpPost]
        [Route("AddLabel")]
        public IActionResult AddLabel(long notesID, string labelname)
        {
            try
            {
                long UserId = Convert.ToInt64(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);

                var result = labelBL.AddLabel(UserId, notesID, labelname);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Label is Added Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Add Label" });
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [Authorize]
        [HttpPost]
        [Route("CreateLabel")]
        public ActionResult CreateLabel(LabelModel model)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labelBL.CreateLabel(model, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Created Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Create Label" });
                }
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.ToString());
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Retrieve")]
        public IActionResult GetAllLabels()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "UserID").Value);
                var labels = labelBL.GetLabels(userId);
                if (labels != null)
                {
           
                    return this.Ok(new { Success = true, Message = "Labels found Successfully", data = labels });
                }
                else
                {
                    return this.NotFound(new { Success = false, Message = "No label found" });
                }
            }
            catch (Exception)
            {
                _logger.LogError(ToString());
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateLabel(LabelModel labelModel,long labelID)
        {
            try
            {
                long ID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                var result = labelBL.UpdateLabel(labelModel, labelID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Data Updated Successful", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Unable to Update Data" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(long LabelID)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
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
