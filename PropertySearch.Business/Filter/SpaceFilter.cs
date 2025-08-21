using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertySearch.Business.Filter
{
    public class SpaceFilter
    {
        public int? PropertyId { get; set; }
        public string? Type { get; set; }
        public int? MinSize { get; set; }
    }
}
