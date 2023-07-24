using System.ComponentModel.DataAnnotations.Schema;

namespace DeveloperHub.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public int topicCount { get; set; }


        public List<Topic>  Topics { get; set; }


    }
}
