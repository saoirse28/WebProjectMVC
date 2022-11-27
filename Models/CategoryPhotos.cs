#nullable disable
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjectMVC.Models
{
    public class CategoryPhotos
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int PhotosId { get; set; }
        public Photo Photos { get; set; }
        
    }
}
