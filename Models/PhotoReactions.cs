#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using WebProjectMVC.Data;

namespace WebProjectMVC.Models
{
    //[PrimaryKey(nameof(ReactionId), nameof(PhotoId), nameof(UserId))]
    public class PhotoReactions
    {
        public int ReactionId { get; set; }
        public Reaction Reactions { get; set; }
        public int PhotoId { get; set; }
        public Photo Photos { get; set; }

        [Column("ApplicationUserId", TypeName = "nvarchar(450)")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}


