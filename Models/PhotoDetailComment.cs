using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using WebProjectMVC.Data;

namespace WebProjectMVC.Models
{
    [Index(nameof(Id), nameof(ApplicationUserId), nameof(PhotoDetailId), IsUnique = true, Name = "idxPhotoDetailComment_1")]
    public class PhotoDetailComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Detail", TypeName = "varchar(255)")]
        [ConcurrencyCheck, MaxLength(255, ErrorMessage = "Comment detials must be 255 characters or less"), MinLength(3, ErrorMessage = "Comment details must be 3 characters or more")]
        public string? Detail { get; set; }

        [Column("ApplicationUserId", TypeName = "nvarchar(450)")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int PhotoDetailId { get; set; }
        public PhotoDetail? PhotoDetail { get; set; }
    }
}
