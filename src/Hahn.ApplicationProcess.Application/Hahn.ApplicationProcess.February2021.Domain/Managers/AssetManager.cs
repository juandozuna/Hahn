namespace Hahn.ApplicationProcess.February2021.Domain.Managers
{
    using System.Threading.Tasks;
    using FluentValidation.Results;
    using Models;
    using Repositories;
    using Validators;

    /// <summary>
    /// Business Logic for the asset entity
    /// </summary>
    public sealed class AssetManager
    {
        private readonly IAssetRepository _assetRepository;

        /// <summary>
        /// Creates a new instance of <see cref="AssetManager"/>
        /// </summary>
        /// <param name="assetRepository">An implementation of <see cref="IAssetRepository"/></param>
        public AssetManager(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        /// <summary>
        /// Creates a new Asset
        /// </summary>
        /// <param name="asset">An instance of <see cref="Asset"/></param>
        /// <returns></returns>
        public async Task<IOperationResult<Asset>> AddNewAsset(Asset asset)
        {
            AssetValidator validator = new ();
            
            ValidationResult result = await validator.ValidateAsync(asset);

            if (!result.IsValid)
            {
                return OperationResult<Asset>.ValidationFailed(result);
            }

            await _assetRepository.Insert(asset);
            
            return OperationResult<Asset>.Ok(asset);
        }
    }
}