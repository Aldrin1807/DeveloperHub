namespace DeveloperHub.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }


        public string DateTime { get;set; }



    }
}
