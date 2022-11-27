using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Xml.Linq;
using WebProjectMVC.Models;

namespace WebProjectMVC.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column("FirstName", TypeName = "varchar(30)")]
        public string? FirstName { get; set; }

        [PersonalData]
        [Column("LastName", TypeName = "varchar(30)")]
        public string? LastName { get; set; }

        public List<Photo>? Photos { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<CommentReactions>? CommentReactions { get; set; }
        public List<PhotoReactions>? PhotoReactions { get; set; }
        public List<PhotoDetailReactions>? PhotoDetailReactions { get; set; }
        public List<PhotoDetailComment>? PhotoDetailComment { get; set; }

    }
}
