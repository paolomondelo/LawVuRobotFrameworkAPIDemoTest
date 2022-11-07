using AppLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LawyerController : ControllerBase
    {
        private readonly ILegalLogic legalLogic;

        public LawyerController(ILegalLogic lawyerLogic)
        {
            this.legalLogic = lawyerLogic;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Lawyer>>> Get([FromQuery] int skip = 0, int take = 100)
        {
            var result = await legalLogic.GetLawyersAsync(skip, take);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Lawyer>> Get([FromRoute] Guid id)
        {
            var result = await legalLogic.GetLawyerAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Lawyer lawyer)
        {
            await legalLogic.CreateLawyerAsync(lawyer);
            return Ok();
        }
    }
}
