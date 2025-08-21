using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertySearch.Business.Models.RMs
{
    public class CreateSpaceRequest
    {
        public string Type { get; set; } = string.Empty;
        public double Size { get; set; }
        public string? Description { get; set; }
    }
}
