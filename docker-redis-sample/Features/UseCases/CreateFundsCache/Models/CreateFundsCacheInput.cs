using Docker.Redis.Sample.Shared.Domain.Funds;
using MediatR;

namespace Docker.Redis.Sample.Features.UseCases.CreateFundsCache.Models
{
    public class CreateFundsCacheInput : IRequest<bool>
    {
        public List<Fund>? Funds { get; set; }
    }
}
