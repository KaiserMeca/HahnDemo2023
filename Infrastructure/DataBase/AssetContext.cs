using Domain.Assets.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase
{
    public class AssetContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public AssetContext(DbContextOptions<AssetContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet of assets in the context.
        /// </summary>
        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(o => o.HasKey(x => x.Id));
            ; base.OnModelCreating(modelBuilder);
        }
    }
}
