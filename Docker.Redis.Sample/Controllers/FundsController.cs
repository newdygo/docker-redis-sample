using Docker.Redis.Sample.Features.UseCases.GetFunds.Models.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Docker.Sample.Redis.Controllers
{
    [ApiController]
    [Produces("application/json")]

    public class FundsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FundsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("funds")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetFundsAsync(
            [FromQuery] GetFundsInput input,
            CancellationToken cancellationToken)
        {
            if (!input.IsValid())
            {
                return BadRequest();
            }

            var result = await _mediator.Send(input, cancellationToken);

            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}