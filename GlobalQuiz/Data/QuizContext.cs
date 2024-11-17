using Microsoft.EntityFrameworkCore;
using GlobalQuiz.Domain;

namespace GlobalQuiz.Data
{
    public class QuizContext(DbContextOptions<QuizContext> options) : DbContext(options)
    {
        public required DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasKey(q => q.Id);
            modelBuilder.Entity<Question>().Property(q => q.Text).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Question>().Property(q => q.Answer).IsRequired();
        }
    }
}
