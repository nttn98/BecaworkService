using BecaworkService.Interfaces;
using BecaworkService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BecaworkService.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class FCMTokenLogController : ControllerBase
    {
        public readonly IFCMTokenLogService _FCMTokenLogService;

        public FCMTokenLogController(IFCMTokenLogService FCMTokenService)
        {
            _FCMTokenLogService = FCMTokenService ?? throw new ArgumentNullException(nameof(FCMTokenService));
        }

        //Get 
        [HttpGet]
        [Route("GetFCMTokenLogs")]
        public async Task<IActionResult> GetFCMTokenLogs(int page, int pageSize)
        {
            var FCMTokenLogs = await _FCMTokenLogService.GetFCMTokenLogs(page, pageSize);
            return Ok(FCMTokenLogs);
        }

        //Get v2
        [HttpGet]
        [Route("GetFCMTokenLogs2")]
        public async Task<IActionResult> GetFCMTokenLogs2([FromQuery] QueryParams queryParams)
        {
            var FCMTokenLogs = await _FCMTokenLogService.GetFCMTokenLogs2(queryParams);
            return Ok(FCMTokenLogs);
        }

        //Get by ID 
        [HttpGet]
        [Route("GetFCMTokenLogByID")]
        public async Task<IActionResult> GetFCMTokenLogByID(long ID)
        {
            var tempFCMTokenLog = await _FCMTokenLogService.GetFCMTokenLogByID(ID);
            return Ok(tempFCMTokenLog);
        }
        //Add FCMTokenLog
        /* [HttpPost]
         [Route("AddFCMTokenLog")]
         public async Task<IActionResult> Post(FCMTokenLog objFCMTokenLog)
         {
             var tempFCMTokenLog = await _FCMTokenLogService.AddFCMTokenLog(objFCMTokenLog);
             if (tempFCMTokenLog.Id == 0)
             {
                 return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");

             }
             return Ok("Added FCMTokenLog Successfully");
         }
 */
        //Update FCMTokenLog
        [HttpPut]
        [Route("UpdateFCMTokenLog")]
        public async Task<IActionResult> Put(FCMTokenLog objFCMTokenLog)
        {
            await _FCMTokenLogService.UpdateFCMTokenLog(objFCMTokenLog);
            return Ok("Update FCMTokenLog Successfully");
        }

        //Delete FCMTokenLog
        [HttpDelete]
        [Route("DeleteFCMTokenLog")]
        public JsonResult DeleteFCMTokenLog(long ID)
        {
            _FCMTokenLogService.DeleteFCMTokenLog(ID);
            return new JsonResult("Delete FCMTokenLog Successfully");
        }
    }
}
