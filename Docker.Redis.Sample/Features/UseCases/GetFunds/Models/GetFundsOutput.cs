using Redis.OM.Modeling;

namespace Docker.Redis.Sample.Features.UseCases.GetFunds.Models.v1
{
    public class GetFundsOutput
    {
        [RedisIdField]
        public int? id { get; set; }
        public string? cnpj { get; set; }
        public string? name { get; set; }
        public string? @class { get; set; }
    }
}
