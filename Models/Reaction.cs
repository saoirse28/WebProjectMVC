#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace WebProjectMVC.Models
{
    public class Reaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Name", TypeName = "varchar(30)")]
        [MaxLength(50, ErrorMessage = "Reaction name must be 30 characters or less"), MinLength(3, ErrorMessage = "Reaction name must be 3 characters or more")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Font-Awsome Icon")]
        [Column("Icon", TypeName = "varchar(20)")]
        [MaxLength(50, ErrorMessage = "Reaction font-awsome must be 20 characters or less"), MinLength(3, ErrorMessage = "Reaction font-awsome icon must be 3 characters or more")]
        public string Icon { get; set; }

        [Required]
        [DisplayName("Bootstrap Text-Color")]
        [Column("Color", TypeName = "varchar(20)")]
        [MaxLength(50, ErrorMessage = "Reaction bootstrap text-color must be 30 characters or less"), MinLength(3, ErrorMessage = "Reaction bootstrap text-color must be 3 characters or more")]
        public string Color { get; set; }

        [Required]
        [DisplayName("Enabled")]
        [Column("Enabled", TypeName = "bit")]
        public bool Enabled { get; set; }
    }
}
