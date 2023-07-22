using DeveloperHub.Data.DTOs;
using DeveloperHub.Models;

namespace DeveloperHub.Data.Services
{
    public class TopicService
    {
        private readonly AppDbContext _context;

        public TopicService(AppDbContext context)
        {
                _context = context;
        }

        public async Task Create(TopicDTO topic)
        {
            var ntopic = new Topic()
            {
                Title = topic.Title,
                Description = topic.Description,
                CategoryID = topic.CategoryID,
                UserID = topic.UserID,
                DateTime = DateTime.Now.ToString("dd MMM yyyy HH:mm")
            };

            await _context.Topics.AddAsync(ntopic);
            await _context.SaveChangesAsync();
        }
    }
}
