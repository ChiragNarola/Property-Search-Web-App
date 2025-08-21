namespace PropertySearch.Business.Models.RMs.PageSort
{
    public class SortRM
    {
        public SortRM(string property, SortType type)
        {
            Property = property;
            Type = type;
        }

        public string Property { get; set; }

        public SortType Type { get; set; }
    }

    public enum SortType
    {
        OrderBy = 1,
        OrderByDescending = 2,
        ThenBy = 3,
        ThenByDescending = 4
    }
}
