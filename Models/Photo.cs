#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebProjectMVC.Data;

namespace WebProjectMVC.Models
{
    [Table("Photos")]
    [Index(nameof(Title), IsUnique = false, Name = "idxPhotoTitle")]
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("PhotoTitle", TypeName = "varchar(150)")]
        [MaxLength(150, ErrorMessage = "Photo title must be 150 characters or less"), MinLength(3, ErrorMessage = "Photo title must be 3 characters or more")]
        public string Title { get; set; }

        [Column("PhotoDescription", TypeName = "varchar(150)")]
        [MaxLength(150, ErrorMessage = "Photo description must be 150 characters or less")]
        public string Description { get; set; }
        public List<CategoryPhotos> CategoryPhotos { get; set; }
        public List<Comment> Comments { get; set; }
        public List<PhotoReactions> PhotoReactions { get; set; }

        [Column("ApplicationUserId", TypeName = "nvarchar(450)")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<PhotoDetail> PhotoDetails { get; set; }

        [FromForm]
        [NotMapped]
        public IFormFileCollection Files { get; set; }


    }
}
