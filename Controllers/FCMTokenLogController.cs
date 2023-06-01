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
       /* private readonly HttpClient _httpClient;*/

        public FCMTokenLogController(IFCMTokenLogService FCMTokenLogService)
        {
            _FCMTokenLogService = FCMTokenLogService ?? throw new ArgumentNullException(nameof(FCMTokenLogService));
            /*_httpClient = httpClient ?? throw new ArgumentNullException();*/
        }


        //Get 
        [HttpGet]
        [Route("GetFCMTokenLogs")]
        public async Task<IActionResult> GetFCMTokenLogs([FromQuery] QueryParams queryParams)
        {
            var FCMTokenLogs = await _FCMTokenLogService.GetFCMTokenLogs(queryParams);
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
        [HttpPost]
        [Route("AddFCMTokenLog")]
        public async Task<IActionResult> Post(FCMTokenLog objFCMTokenLog)
        {
            var tempFCMTokenLog = await _FCMTokenLogService.AddFCMTokenLog(objFCMTokenLog);
            /*if (tempFCMTokenLog.Id == 0)
            {
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("ID", objFCMTokenLog.Id.ToString())
                });

                var response = await _httpClient.PostAsync("https://service.vntts.vn/FcmToken/SendNotification", content);
                if (!response.IsSuccessStatusCode)
                {
                    return Ok(content);
                }
            }*/
            return Ok("Added FCMTokenLog Successfully");
        }

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
