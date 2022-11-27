#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebProjectMVC.Data;

namespace WebProjectMVC.Models
{
    //[PrimaryKey(nameof(CommentId), nameof(ReactionId), nameof(UserId))]
    public class CommentReactions
    {
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public int ReactionId { get; set; }
        public Reaction Reaction { get; set; }

        [Column("ApplicationUserId", TypeName = "nvarchar(450)")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}


