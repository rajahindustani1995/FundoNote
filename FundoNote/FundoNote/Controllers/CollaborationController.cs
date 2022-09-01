using BusinessLayer.Interface;
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
    public class CollaborationController : Controller
    {
        private readonly ICollaborationBL collaborationBL;
        private readonly IMemoryCache memoryCache;
        private readonly FundoContext fundoContext;
        private readonly IDistributedCache distributedCache;
        public CollaborationController(ICollaborationBL collaborationBL, IMemoryCache memoryCache, FundoContext fundoContext, IDistributedCache distributedCache)
        {
            this.collaborationBL = collaborationBL;
            this.memoryCache = memoryCache;
            this.fundoContext = fundoContext;
            this.distributedCache = distributedCache;
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

        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            var cacheKey = "CollabList";
            string serializedCollabList;
            var collabList = new List<CollaborationEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<CollaborationEntity>>(serializedCollabList);
            }
            else
            {
                collabList = await fundoContext.CollaboratorTable.ToListAsync();
                serializedCollabList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(collabList);
        }
    }
}
