namespace Hahn.ApplicationProcess.February2021.Data.Repositories
{
    using Domain.Models;
    using Domain.Repositories;

    /// <summary>
    /// Implementation for asset repository
    /// </summary>
    public sealed class AssetRepository : GenericRepository<Asset>, IAssetRepository
    {
        /// <summary>
        /// Creates a new instance of asset repository
        /// </summary>
        /// <param name="context"></param>
        public AssetRepository(HahnDbContext context) : base(context)
        { }
    }
}