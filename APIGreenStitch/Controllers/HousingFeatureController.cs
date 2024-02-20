using Application.Roamly.Features.CQRS.Commands.HousingCommands;
using Application.Roamly.Features.CQRS.Commands.HousingFeatureCommands;
using Application.Roamly.Features.CQRS.Queries.HousingFeaturesQueries;
using Application.Roamly.Features.CQRS.Queries.HousingQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Roamly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousingFeatureController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HousingFeatureController(IMediator mediator)
        {
            _mediator = mediator; 
        }

        [HttpGet]
        public async Task<IActionResult> ListFeatures()
        {
            var response = await _mediator.Send(new GetHousingFeatureQuery());
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeature(string id)
        {
            var response = await _mediator.Send(new GetHousingFeatureByIdQuery(id));

            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateHousingFeatureCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.Succeeded)
            {
                return Ok(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFeature(string id)
        {
            var response = await _mediator.Send(new RemoveHousingFeatureCommand(id));

            if (response.Succeeded)
            {
                return Ok(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeature(UpdateHousingFeatureCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.Succeeded)
            {
                return Ok(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
