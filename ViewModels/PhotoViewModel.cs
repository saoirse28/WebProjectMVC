using WebProjectMVC.Data;
using WebProjectMVC.Models;

namespace WebProjectMVC.ViewModels
{
    public class PhotoViewModel
    {
        public Photo Photo { get; set; }
        public List<Photo> PhotoList { get; set; }
        public PhotoDetail PhotoDetail { get; set; }
        public List<Reaction> ReactionRef { get; set; }
        public PhotoDetailComment PhotoDetailComment { get; set; }
    }
}
