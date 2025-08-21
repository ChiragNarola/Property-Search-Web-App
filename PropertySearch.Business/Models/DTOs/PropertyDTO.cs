using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertySearch.Business.Models.DTOs
{
    public class PropertyDTO
    {
        public int Id { get; set; }
        public string Address { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public List<SpaceDTO> spaces { get; set; } = null!;

    }
}
