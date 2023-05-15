using BecaworkService.Interfaces;
using BecaworkService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace BecaworkService.Controllers
{
    public class ElectrolyticTokenLogController : Controller
    {
        public readonly IElectrolyticTokenLogService _eTokenLogService;

        public ElectrolyticTokenLogController(IElectrolyticTokenLogService eTokenLogService)
        {
            _eTokenLogService = eTokenLogService ?? throw new ArgumentNullException(nameof(eTokenLogService));
        }

        [HttpGet]
        [Route("GetElectrolyticTokens")]
        public async Task<IActionResult> GetElectrolyticLogTokens(int page, int pageSize)
        {
            var eTokenLogs = await _eTokenLogService.GetElectrolyticTokenLogs(page, pageSize);
            return Ok(eTokenLogs);
        }

        [HttpGet]
        [Route("GetElectrolyticLogTokens")]
        public async Task<IActionResult> GetElectrolyticTokenLogs2([FromQuery] QueryParams queryParams)
        {
            var eTokenLogs = await _eTokenLogService.GetElectrolyticTokenLogs2(queryParams);
            return Ok(eTokenLogs);
        }


        [HttpGet]
        [Route("GetElectrolyticTokenLogByID/{ID}")]
        public async Task<IActionResult> GetElectrolyticTokenLogByID(long ID)
        {
            var eTokenLog = await _eTokenLogService.GetElectrolyticTokenLogByID(ID);
            return Ok(eTokenLog);
        }

        [HttpPost]
        [Route("AddElectrolyticTokenLog")]
        public async Task<IActionResult> Post(ElectrolyticTokenLog objETokenLog)
        {
            var eTokens = await _eTokenLogService.AddElectrolyticTokenLog(objETokenLog);
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added ElectrolyticToken Successfully");
        }

        [HttpPut]
        [Route("UpdateElectrolyticToken")]
        public async Task<IActionResult> Put(ElectrolyticTokenLog objETokenLog)
        {
            await _eTokenLogService.AddElectrolyticTokenLog(objETokenLog);
            return Ok("Update ElectrolyticToken Successfully");
        }

        [HttpDelete]
        [Route("DeleteElectrolyticTokenLog/{ID}")]
        public JsonResult Delete(long ID)
        {
            _eTokenLogService.DeleteElectrolyticTokenLog(ID);
            return new JsonResult("Delete ElectrolyticToken Successfully");
        }
    }
}
