using BecaworkService.Interfaces;
using BecaworkService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace BecaworkService.Controllers
{
    public class FCMTokenController : ControllerBase
    {
        public readonly IFCMTokenService _fCMTokenService;

        public FCMTokenController(IFCMTokenService fCMTokenService)
        {
            _fCMTokenService = fCMTokenService ?? throw new ArgumentNullException(nameof(fCMTokenService));
        }

        [HttpGet]
        [Route("GetFCMTokens")]
        public async Task<IActionResult> GetFCMTokens(int page, int pageSize)
        {
            var FCMTokens = await _fCMTokenService.GetFCMTokens(page, pageSize);
            return Ok(FCMTokens);
        }

        [HttpGet]
        [Route("GetFCMTokenByID/{ID}")]
        public async Task<IActionResult> GetFCMTokenByID(long ID)
        {
            var tempFCMToken = await _fCMTokenService.GetFCMTokenByID(ID);
            return Ok(tempFCMToken);
        }

        [HttpPost]
        [Route("AddFCMToken")]
        public async Task<IActionResult> Post(FCMToken fCMToken)
        {
            var tempFCMToken = await _fCMTokenService.AddFCMToken(fCMToken);
            if (tempFCMToken.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added FCMToken Successfully");
        }

        [HttpPut]
        [Route("UpdateFCMToken")]
        public async Task<IActionResult> Put(FCMToken fCMToken)
        {
            await _fCMTokenService.AddFCMToken(fCMToken);
            return Ok("Update FCMToken Successfully");
        }

        [HttpDelete]
        [Route("DeleteFCMToken/{ID}")]
        public JsonResult Delete(long ID)
        {
            _fCMTokenService.DeteleFCMToken(ID);
            return new JsonResult("Delete FCMToken Successfully");
        }
    }
}