#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectMVC.Models
{
    public class PhotoDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public byte[] Bytes { get; set; }

        [Column("Description", TypeName = "varchar(150)")]
        [ConcurrencyCheck, MaxLength(150, ErrorMessage = "Photo description must be 150 characters or less")]
        public string? Description { get; set; }

        [Column("FileExtension", TypeName = "varchar(5)")]
        public string FileExtension { get; set; }

        [Column("Size", TypeName = "decimal(8,2)")]
        public decimal Size { get; set; }
        public int PhotoId { get; set; }
        public Photo Photo { get; set; }
        public List<PhotoDetailComment> PhotoDetailComments { get; set; }
        public List<PhotoDetailReactions> PhotoDetailReactions { get; set;}
    }
}

