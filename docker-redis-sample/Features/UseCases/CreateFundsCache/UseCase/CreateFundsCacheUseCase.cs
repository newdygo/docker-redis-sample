using Docker.Redis.Sample.Features.UseCases.CreateFundsCache.Models;
using Docker.Redis.Sample.Shared.Domain.Funds;
using MediatR;
using StackExchange.Redis;
using System.Text.Json;

namespace Docker.Redis.Sample.Features.UseCases.CreateFundsCache.UseCase
{
    public class CreateFundsCacheUseCase : IRequestHandler<CreateFundsCacheInput, bool>
    {
        private readonly IDatabase _database;
        private readonly ILogger<CreateFundsCacheUseCase> _logger;

        private List<string> FundsIndexCommandArgs = new()
        {
            "fund-index",
            "ON",
            "HASH",
            "PREFIX",
            "1",
            "fund:",
            "SCHEMA",
            "id",
            "TAG",
            "SORTABLE",
            "name",
            "TEXT",
            "SORTABLE",
            "class",
            "TEXT",
            "NOINDEX",
            "start",
            "TEXT",
            "NOINDEX",
            "manager",
            "TEXT",
            "NOINDEX",
            "fundType",
            "TEXT",
            "NOINDEX",
            "updatedDate",
            "TEXT",
            "NOINDEX",
            "administrator",
            "TEXT",
            "NOINDEX"
        };

        public CreateFundsCacheUseCase(
            IDatabase database,
            ILogger<CreateFundsCacheUseCase> logger)
        {
            _logger = logger;
            _database = database;
        }

        public async Task<bool> Handle(CreateFundsCacheInput request, CancellationToken cancellationToken)
        {
            var updatedDate = $"{DateTime.Now:o}";
            
            try
            {
                await _database.ExecuteAsync("FT.CREATE", FundsIndexCommandArgs.ToArray());
            }
            catch
            {
            }

            foreach (var fund in request.Funds ?? new List<Fund>())
            {
                try
                {
                    
                    await _database.HashSetAsync($"fund:{fund.Id}", new HashEntry[]
                    {
                        new HashEntry("updatedDate", updatedDate),

                        new HashEntry("id", fund.Id),
                        new HashEntry("name", fund.Name),
                        new HashEntry("class", fund.Class),
                        new HashEntry("start", fund.Start),
                        new HashEntry("manager", fund.Manager),
                        new HashEntry("fundType", fund.FundTypeFormatted),
                        new HashEntry("administrator", fund.Administrator)
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Erro ao persistir cache Redis para o fundo: {fund}", fund.Id);
                }
            }

            return true;
        }
    }
}
