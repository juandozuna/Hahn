namespace Hahn.ApplicationProcess.February2021.Data
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents the applications DbContext
    /// </summary>
    public sealed class HahnDbContext : DbContext
    {
        /// <summary>
        /// Represents a list of assets
        /// </summary>
        public DbSet<Asset> Assets { get; set; }

        /// <summary>
        /// Builds a new instance of the dbContext
        /// </summary>
        /// <param name="options"></param>
        public HahnDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}