using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizManager.Core.Enitites;
using QuizManager.Core.User;
using QuizManager.Infrastructure.Data.Configuration;

namespace QuizManager.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<QuizManagerUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new OrganisationConfiguration());
        }

        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuizManagerUser> Users { get; set; }
    }
}
