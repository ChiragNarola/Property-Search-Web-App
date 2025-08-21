using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertySearch.Business.Models.RMs
{
    public class CreatePropertyRequest
    {
        public string Address { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public List<CreateSpaceRequest>? Spaces { get; set; }
    }
}
