using BecaworkService.Interfaces;
using BecaworkService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace BecaworkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectrolyticTokenLogController : Controller
    {
        public readonly IElectrolyticTokenLogService _eTokenLogService;

        public ElectrolyticTokenLogController(IElectrolyticTokenLogService eTokenLogService)
        {
            _eTokenLogService = eTokenLogService ?? throw new ArgumentNullException(nameof(eTokenLogService));
        }

       /* [HttpGet]
        [Route("GetElectrolyticTokens")]
        public async Task<IActionResult> GetElectrolyticLogTokens(int page, int pageSize)
        {
            var eTokenLogs = await _eTokenLogService.GetElectrolyticTokenLogs(page, pageSize);
            return Ok(eTokenLogs);
        }*/

        [HttpGet]
        [Route("GetElectrolyticTokenLogs")]
        public async Task<IActionResult> GetElectrolyticTokenLogs([FromQuery] QueryParams queryParams)
        {
            var eTokenLogs = await _eTokenLogService.GetElectrolyticTokenLogs(queryParams);
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
            if (eTokens.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added ElectrolyticTokenLog Successfully");
        }

        [HttpPut]
        [Route("UpdateElectrolyticTokenLog")]
        public async Task<IActionResult> Update(ElectrolyticTokenLog objETokenLog)
        {
            await _eTokenLogService.AddElectrolyticTokenLog(objETokenLog);
            return Ok("Update ElectrolyticTokenLog Successfully");
        }

        [HttpDelete]
        [Route("DeleteElectrolyticTokenLog/{ID}")]
        public JsonResult Delete(long ID)
        {
            _eTokenLogService.DeleteElectrolyticTokenLog(ID);
            return new JsonResult("Delete ElectrolyticTokenLog Successfully");
        }
    }
}
