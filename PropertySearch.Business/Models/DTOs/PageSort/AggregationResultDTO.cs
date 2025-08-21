using PropertySearch.Business.Models.RMs.PageSort;

namespace PropertySearch.Business.Models.DTOs.PageSort
{
    public class AggregationResultDTO
    {
        public AggregationResultDTO(string property, AggregateType type, object? result)
        {
            Property = property;
            Type = type;
            Result = result;
        }

        public string Property { get; set; }

        public AggregateType Type { get; set; }

        public object? Result { get; set; }
    }
}
