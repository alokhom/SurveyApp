using Microsoft.EntityFrameworkCore;
using SurveyApp.Models.Entities;

namespace SurveyApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
