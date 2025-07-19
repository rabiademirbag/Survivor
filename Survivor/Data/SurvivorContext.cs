using Microsoft.EntityFrameworkCore;
using Survivor.Models;

namespace Survivor.Data
{
    public class SurvivorContext:DbContext
    {
        public SurvivorContext(DbContextOptions<SurvivorContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Competitor> Competitors { get; set; }
    }
}
