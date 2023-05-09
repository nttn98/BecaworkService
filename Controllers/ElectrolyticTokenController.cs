/*using BecaworkService.Interfaces;
using BecaworkService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BecaworkService.Services;

namespace BecaworkService.Controllers
{
    public class ElectrolyticTokenController : ControllerBase
    {
        public readonly IElectrolyticTokenService _eTokenService;

        public ElectrolyticTokenController(IElectrolyticTokenService eTokenService)
        {
            _eTokenService = eTokenService ?? throw new ArgumentNullException(nameof(eTokenService));
        }

        [HttpGet]
        [Route("GetElectrolyticTokens")]
        public async Task<IActionResult> GetElectrolyticTokens(int page, int pageSize)
        {
            var ElectrolyticTokens = await _eTokenService.GetElectrolyticTokens(page, pageSize);
            return Ok(ElectrolyticTokens);
        }

        [HttpGet]
        [Route("GetElectrolyticTokenByID/{ID}")]
        public async Task<IActionResult> GetElectrolyticTokenByID(long ID)
        {
            var tempElectrolyticToken = await _eTokenService.GetElectrolyticTokenByID(ID);
            return Ok(tempElectrolyticToken);
        }

        [HttpPost]
        [Route("AddElectrolyticToken")]
        public async Task<IActionResult> Post(ElectrolyticToken eToken)
        {
            var tempETokens = await _eTokenService.AddElectrolyticToken(eToken);
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added ElectrolyticToken Successfully");
        }

        [HttpPut]
        [Route("UpdateElectrolyticToken")]
        public async Task<IActionResult> Put(ElectrolyticToken eToken)
        {
            await _eTokenService.AddElectrolyticToken(eToken);
            return Ok("Update ElectrolyticToken Successfully");
        }

        [HttpDelete]
        [Route("DeleteElectrolyticToken/{ID}")]
        public JsonResult Delete(long ID)
        {
            _eTokenService.DeteleElectrolyticToken(ID);
            return new JsonResult("Delete ElectrolyticToken Successfully");
        }
    }
}
*/