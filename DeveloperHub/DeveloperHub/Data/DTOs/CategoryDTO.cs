using System.ComponentModel.DataAnnotations;

namespace DeveloperHub.Data.DTOs
{
    public class CategoryDTO
    {

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
