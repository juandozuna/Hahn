namespace Hahn.ApplicationProcess.February2021.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using FluentValidation.Results;
    using Models;
    using Repositories;
    using Validators;

    /// <summary>
    /// Business Logic for the asset entity
    /// </summary>
    public sealed class AssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly ICountryRepository _countryRepository;

        /// <summary>
        /// Creates a new instance of <see cref="AssetService"/>
        /// </summary>
        /// <param name="assetRepository">An implementation of <see cref="IAssetRepository"/></param>
        /// <param name="countryRepository">An implementation of <see cref="ICountryRepository"/></param>
        public AssetService(IAssetRepository assetRepository, ICountryRepository countryRepository)
        {
            _assetRepository = assetRepository;
            _countryRepository = countryRepository;
        }

        /// <summary>
        /// Gets all assets in DB
        /// </summary>
        /// <returns></returns>
        public async Task<ISet<Asset>> GetAll()
        {
            var assets = await _assetRepository.GetAll();

            return assets.ToHashSet();
        }

        /// <summary>
        /// Gets an <see cref="Asset"/> by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Id of the <see cref="Asset"/></param>
        /// <returns></returns>
        public async Task<Asset> GetById(int id)
        {
            var asset = await _getAssetById(id);

            if (asset == null) throw new KeyNotFoundException("Id was not found");

            return asset;
        }

        /// <summary>
        /// Creates a new Asset
        /// </summary>
        /// <param name="asset">An instance of <see cref="Asset"/></param>
        /// <returns></returns>
        public async Task<Asset> AddNewAsset(Asset asset)
        {
            AssetValidator validator = new(_countryRepository);

            ValidationResult result = await validator.ValidateAsync(asset);

            if (!result.IsValid)
            {
                throw new ArgumentException(result.Errors.First().ErrorMessage);
            }

            await _assetRepository.Insert(asset);

            return asset;
        }

        /// <summary>
        /// Updates an asset
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public async Task<Asset> UpdateAsset(Asset asset)
        {
            AssetValidator validator = new(_countryRepository);

            ValidationResult result = await validator.ValidateAsync(asset);

            if (!result.IsValid) throw new ArgumentException(result.Errors.First().ErrorMessage);

            Asset oldAsset = await _getAssetById(asset.Id);

            if (oldAsset == null) throw new KeyNotFoundException("Asset was not found");

            _updatePropertiesOfTrackedEntity(asset, oldAsset);

            await _assetRepository.Update(oldAsset);

            return asset;
        }

        /// <summary>
        /// Removes an asset from store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Asset> RemoveAsset(int id)
        {
            var asset = await _getAssetById(id);

            if (asset == null) throw new KeyNotFoundException("Id was not found");

            await _assetRepository.Delete(asset);

            return asset;
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