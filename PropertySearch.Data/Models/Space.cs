using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertySearch.Data.Models
{
    [Table("Space")]
    public class Space
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public int PropertyId { get; set; }

        public virtual Property? Property { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = null!;

        public double Size { get; set; } 

        public string? Description { get; set; }
    }
}
