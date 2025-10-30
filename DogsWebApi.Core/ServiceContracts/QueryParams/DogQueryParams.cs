namespace DogsWebApi.Core.ServiceContracts.QueryParams
{
    public class DogQueryParams
    {
        public string? Attribute { get; set; }
        public string? Order { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}