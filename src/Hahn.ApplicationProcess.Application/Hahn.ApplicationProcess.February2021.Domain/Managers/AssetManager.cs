namespace Hahn.ApplicationProcess.February2021.Domain.Managers
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using FluentValidation.Results;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Models;
    using Repositories;
    using Validators;

    /// <summary>
    /// Business Logic for the asset entity
    /// </summary>
    public sealed class AssetManager
    {
        private readonly IAssetRepository _assetRepository;
        private readonly ICountryRepository _countryRepository;

        /// <summary>
        /// Creates a new instance of <see cref="AssetManager"/>
        /// </summary>
        /// <param name="assetRepository">An implementation of <see cref="IAssetRepository"/></param>
        /// <param name="countryRepository">An implementation of <see cref="ICountryRepository"/></param>
        public AssetManager(IAssetRepository assetRepository, ICountryRepository countryRepository)
        {
            _assetRepository = assetRepository;
            _countryRepository = countryRepository;
        }

        /// <summary>
        /// Gets an <see cref="Asset"/> by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of the <see cref="Asset"/></param>
        /// <returns></returns>
        public async Task<IOperationResult<Asset>> GetById(int id)
        {
            var asset = await _getAssetById(id);

            return asset != null
                ? OperationResult<Asset>.Ok(asset)
                : OperationResult<Asset>.Fail("Not Found");
        }

        /// <summary>
        /// Creates a new Asset
        /// </summary>
        /// <param name="asset">An instance of <see cref="Asset"/></param>
        /// <returns></returns>
        public async Task<IOperationResult<Asset>> AddNewAsset(Asset asset)
        {
            AssetValidator validator = new (_countryRepository);
            
            ValidationResult result = await validator.ValidateAsync(asset);

            if (!result.IsValid)
            {
                return OperationResult<Asset>.ValidationFailed(result);
            }

            await _assetRepository.Insert(asset);
            
            return OperationResult<Asset>.Ok(asset);
        }
        
        /// <summary>
        /// Updates an asset
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public async Task<IOperationResult<Asset>> UpdateAsset(Asset asset)
        {
            AssetValidator validator = new(_countryRepository);

            ValidationResult result = await validator.ValidateAsync(asset);
            
            if (!result.IsValid) return OperationResult<Asset>.ValidationFailed(result);

            Asset oldAsset = await _getAssetById(asset.Id);

            if (oldAsset == null) return OperationResult<Asset>.Fail("No Asset Found");

            _updatePropertiesOfTrackedEntity(asset, oldAsset);

            await _assetRepository.Update(oldAsset);
            
            return OperationResult<Asset>.Ok();
        }

        /// <summary>
        /// Removes an asset from store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IOperationResult<Asset>> RemoveAsset(int id)
        {
            var asset = await _getAssetById(id);

            if (asset == null) OperationResult<Asset>.Fail("Not Found");

            await _assetRepository.Delete(asset);
            
            return OperationResult<Asset>.Ok(asset);
        }
        
        private static void _updatePropertiesOfTrackedEntity(Asset newAsset, Asset oldAsset)
        {
            Type type = newAsset.GetType();
            
            PropertyInfo[] properties = type.GetProperties();
            
            foreach (var info in properties)
            {
                info.SetValue(oldAsset, info.GetValue(newAsset));
            }
        }

        private Task<Asset> _getAssetById(int id) => _assetRepository.Get(id);
    }
}