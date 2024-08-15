using Docker.Redis.Sample.Features.UseCases.GetFunds.Models.v1;
using MediatR;
using Redis.OM;
using Redis.OM.Searching.Query;

namespace Docker.Redis.Sample.Features.UseCases.GetFunds.UseCase.v1
{
    public class GetFundsUseCase : IRequestHandler<GetFundsInput, IEnumerable<GetFundsOutput>>
    {
        private readonly RedisConnectionProvider _database;

        public GetFundsUseCase(
            RedisConnectionProvider database)
        {
            _database = database;
        }

        public async Task<IEnumerable<GetFundsOutput>> Handle(GetFundsInput request, CancellationToken cancellationToken)
        {
            var redisQuery = new RedisQuery("fund-index")
            {
                Limit = new SearchLimit
                {
                    Number = request.Limit ?? 10,
                    Offset = request.Offset ?? 0,
                },
                QueryText = request.QueryText,
                Return = new ReturnFields(request.Select.Split(","))
            };
            var redisResults = await _database.Connection.SearchAsync<GetFundsOutput>(redisQuery);

            return redisResults.Documents.Select(fund => fund.Value);
        }
    }
}
