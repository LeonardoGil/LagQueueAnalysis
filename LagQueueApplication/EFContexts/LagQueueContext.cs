using LagQueueApplication.EFContexts.EntityConfigurations;
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.SetMessageEntityConfigurations();
        }
    }
}
