using BecaworkService.Interfaces;
using BecaworkService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BecaworkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public readonly INotificationService _notificationService;
        private readonly HttpClient _httpClient;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _httpClient = new HttpClient();
        }

        /*   [HttpGet]
           [Route("GetNotifications1")]
           public async Task<IActionResult> GetNotifications1([FromQuery] QueryParams queryParams)
           {
               var tempNotifi = await _notificationService.GetNotifications1(queryParams);
               return Ok(tempNotifi);
           }*/

        [HttpGet]
        [Route("GetNotifications")]
        public async Task<IActionResult> GetNotifications([FromQuery] QueryParams queryParams)
        {
            var tempNotifi = await _notificationService.GetNotifications(queryParams);
            return Ok(tempNotifi);
        }

        [HttpGet]
        [Route("GetNotificationByID/{ID}")]
        public async Task<IActionResult> GetNotificationByID(long ID)
        {
            var tempNotifi = await _notificationService.GetNotificationByID(ID);
            return Ok(tempNotifi);
        }

        [HttpPost]
        [Route("AddNotifi")]
        public async Task<IActionResult> Post(Notification objNotifi)
        {
            var tempNotifi = await _notificationService.AddNotifi(objNotifi);
            if (tempNotifi.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok(tempNotifi);
        }

        [HttpPut]
        [Route("UpdateNotifi")]
        public async Task<IActionResult> Put(Notification objNotifi)
        {
            await _notificationService.UpdateNotifi(objNotifi);
            return Ok("Update Notification Successfully");
        }

        [HttpDelete]
        [Route("DeleteNofiti")]
        public JsonResult Delete(long ID)
        {
            _notificationService.DeleteNotifi(ID);
            return new JsonResult("Delete Notification Successfully");

        }

        [HttpPost]
        [Route("SendNofiti")]
        public async Task<IActionResult> SendNotifi(Notification notification)
        {
            bool isSent = await _notificationService.SendNotifi(notification);
            if (isSent)
            {
                return Ok("Notification sent successfully");
            }
            else
            {
                return BadRequest("Failed to send notification");
            }
        }
    }
}
