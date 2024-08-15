using Docker.Redis.Sample.Features.UseCases.CreateFundsCache.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Redis.Search.Controllers
{
    [ApiController]
    [Produces("application/json")]
    
    public class CacheController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CacheController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("funds-cache")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetFundsAsync(
            [FromQuery] CreateFundsCacheInput input,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(input, cancellationToken);

            return Ok();
        }
    }
}
