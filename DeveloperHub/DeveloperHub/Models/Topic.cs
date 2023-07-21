namespace DeveloperHub.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
