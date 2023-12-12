using Microsoft.EntityFrameworkCore;
using VSA.Api.Entities;

namespace VSA.Api.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }

    }
}
