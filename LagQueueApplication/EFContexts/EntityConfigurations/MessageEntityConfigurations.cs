using LagQueueDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LagQueueApplication.EFContexts.EntityConfigurations
{
    internal static class MessageEntityConfigurations
    {
        public static void SetMessageEntityConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                        .HasOne(m => m.ReplyTo)
                        .WithMany()
                        .HasForeignKey(m => m.ReplyToId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                        .HasOne(m => m.Queue)
                        .WithMany(q => q.Messages)
                        .HasForeignKey(m => m.QueueId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
