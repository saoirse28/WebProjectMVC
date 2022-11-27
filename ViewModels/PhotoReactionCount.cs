#nullable disable
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using WebProjectMVC.Data;
using WebProjectMVC.Models;

namespace WebProjectMVC.ViewModels
{
    public class PhotoReactionCount: Reaction
    {
        public int PhotoId { get; set; }
        public int ReactionCount { get; set; }
    }
}


