namespace PropertySearch.Business.Models.RMs.PageSort
{
    public class AggregationRM
    {
        public AggregationRM(string property, AggregateType type)
        {
            Property = property;
            Type = type;
        }

        public string Property { get; set; }

        public AggregateType Type { get; set; }
    }

    public enum AggregateType
    {
        Sum,
        Average,
        Min,
        Max
    }
}
