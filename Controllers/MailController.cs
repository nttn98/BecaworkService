using BecaworkService.Interfaces;
using BecaworkService.Models;
using BecaworkService.Respository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BecaworkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    {
        public readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        //Get Mail
        [HttpGet]
        [Route("GetMails")]
        public async Task<IActionResult> GetMails([FromQuery] QueryParams queryParams)
        {
            var mails = await _mailService.GetMails(queryParams);
            return Ok(mails);
        }

        //Get Mail by ID
        [HttpGet]
        [Route("GetMailByID/{ID}")]
        public async Task<IActionResult> GetMailByID(long ID)
        {
            var tempMail = await _mailService.GetMailByID(ID);
            return Ok(tempMail);
        }

        //Add Mail
        [HttpPost]
        [Route("AddMail")]
        public async Task<IActionResult> AddMail(Mail mail)
        {
            var tempMail = await _mailService.AddMail(mail);
            if (tempMail.ID == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Mail Successfully");
        }

        //Update Mail
        [HttpPut]
        [Route("UpdateMail")]
        public async Task<IActionResult> UpdateMail(Mail mail)
        {
            await _mailService.UpdateMail(mail);
            return Ok("Update Mail Successfully");
        }

        //Delete Mail
        [HttpDelete]
        [Route("DeleteMail/{ID}")]
        public JsonResult Delete(long ID)
        {
            _mailService.DeteleMail(ID);
            return new JsonResult("Delete Mail Successfully");
        }
    }
}
