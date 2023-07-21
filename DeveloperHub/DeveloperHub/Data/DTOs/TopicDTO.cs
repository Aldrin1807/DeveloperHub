using DeveloperHub.Models;
using System.ComponentModel.DataAnnotations;

namespace DeveloperHub.Data.DTOs
{
    public class TopicDTO
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string UserID { get; set; }
        public int CategoryID { get; set; }
    }
}
