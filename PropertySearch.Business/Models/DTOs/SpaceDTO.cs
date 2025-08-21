using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertySearch.Business.Models.DTOs
{
    public class SpaceDTO
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Type { get; set; } = null!;
        public double Size { get; set; }
        public string? Description { get; set; }
    }
}
