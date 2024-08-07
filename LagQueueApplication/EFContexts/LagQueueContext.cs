using LagQueueDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LagQueueApplication.EFContexts
{
    public class LagQueueContext : DbContext
    {
        public LagQueueContext(DbContextOptions<LagQueueContext> options) : base(options) 
        {
        }

        public DbSet<Queue> Queues { get; set; }

        public DbSet<Message> Messages { get; set; }
        
        public DbSet<ProcessingEvent> ProcessingEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
