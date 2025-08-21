namespace PropertySearch.Business.Models.DTOs.PageSort
{
    public class PagedResultDTO<T>
    {
        public PagedResultDTO() => Items = [];

        public int PageSize { get; set; }

        public int PageNo { get; set; }

        public long TotalCount { get; set; }

        public List<T> Items { get; set; }
        public List<AggregationResultDTO>? AggregationResults { get; set; }
    }
}
