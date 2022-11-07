using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AppLogic;
using ServiceModel;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LegalMatterController : ControllerBase
    {
        private readonly ILegalLogic legalLogic;

        public LegalMatterController(
            ILegalLogic legalLogic)
        {
            this.legalLogic = legalLogic;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<LegalMatter>>> Get([FromQuery] int skip = 0, int take = 100)
        {
            var result = await legalLogic.GetMattersAsync(skip, take).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LegalMatter>> Get([FromRoute] Guid id)
        {
            var result = await legalLogic.GetMatterAsync(id).ConfigureAwait(false);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LegalMatter matter)
        {
            await legalLogic.CreateMatterAsync(matter).ConfigureAwait(false);

            return Ok();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LegalMatterAssignmentResponse>> AssignLawyer([FromBody] LegalMatterAssignmentRequest legalMatterLawyerAssignment)
        {
            var result = await legalLogic.AssignMattersAsync(legalMatterLawyerAssignment.Ids, legalMatterLawyerAssignment.LawyerId);
            return result == null 
                ? BadRequest() 
                : Ok(new LegalMatterAssignmentResponse { LegalMatters = result.ToList() });
        }
    }
}
