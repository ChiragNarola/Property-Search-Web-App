namespace PropertySearch.Business.Models.RMs.PageSort
{
    public class PagedRM : IPageSortAggregation
    {
        public const int MAX_PAGE_SIZE = 5000;
        public const int DEFAULT_PAGE_SIZE = 10;
        public const int DEFAULT_PAGE_NO = 1;

        public PagedRM() { }

        public PagedRM(int pageSize, int pageNo)
        {
            PageSize = pageSize;
            PageNo = pageNo;
        }

        private int _pageSize = DEFAULT_PAGE_SIZE;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > MAX_PAGE_SIZE) _pageSize = MAX_PAGE_SIZE;
                if (value <= 0) _pageSize = DEFAULT_PAGE_SIZE;
                else _pageSize = value;
            }
        }
        public int PageNo
        {
            get
            {
                return _pageNo;
            }
            set
            {
                if (value <= 0) _pageNo = DEFAULT_PAGE_NO;
                else _pageNo = value;
            }
        }
        private int _pageNo = DEFAULT_PAGE_NO;

        public IEnumerable<SortRM>? Sorts { get; set; }
        public IEnumerable<AggregationRM>? Aggregations { get; set; }
    }

    public class PagedRM<T> : IPageSortAggregation
    {
        public PagedRM() { }

        public PagedRM(int pageSize, int pageNo, T requestData)
        {
            PageSize = pageSize;
            PageNo = pageNo;
            RequestData = requestData;
        }

        private int _pageSize = PagedRM.DEFAULT_PAGE_SIZE;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > PagedRM.MAX_PAGE_SIZE) _pageSize = PagedRM.MAX_PAGE_SIZE;
                else if (value <= 0) _pageSize = PagedRM.DEFAULT_PAGE_SIZE;
                else _pageSize = value;
            }
        }
        public int PageNo
        {
            get
            {
                return _pageNo;
            }
            set
            {
                if (value <= 0) _pageNo = PagedRM.DEFAULT_PAGE_NO;
                else _pageNo = value;
            }
        }
        private int _pageNo = PagedRM.DEFAULT_PAGE_NO;

        public IEnumerable<SortRM>? Sorts { get; set; }

        public IEnumerable<AggregationRM>? Aggregations { get; set; }

        public T? RequestData { get; set; }
    }

    public interface IPageSortAggregation
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public IEnumerable<SortRM>? Sorts { get; set; }
        public IEnumerable<AggregationRM>? Aggregations { get; set; }
    }
}
