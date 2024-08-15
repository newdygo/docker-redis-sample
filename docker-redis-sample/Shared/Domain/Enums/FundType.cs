namespace Docker.Redis.Sample.Shared.Domain.Enums
{
    public class FundType : Enumeration
    {
        public static readonly FundType Opened = new ("Aberto", "1");
        public static readonly FundType Closed = new ("Aberto", "2");

        public FundType(string id, string name) : base(id, name)
        {
        }
    }
}
