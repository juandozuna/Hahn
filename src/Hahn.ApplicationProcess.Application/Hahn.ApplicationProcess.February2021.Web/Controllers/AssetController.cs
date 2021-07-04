using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicationProcess.February2021.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Models;
    using Domain.Services;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Assets controller
    /// </summary>
    [ApiController]
    [Route("assets")]
    public class AssetController : ControllerBase
    {
        private readonly AssetService _assetService;

        /// <summary>
        /// Creates a new instance of <see cref="AssetController"/>
        /// </summary>
        /// <param name="assetService">An instance of <see cref="AssetService"/></param>
        public AssetController(AssetService assetService)
        {
            _assetService = assetService;
        }

        /// <summary>
        /// Gets an asset by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ISet<Asset>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IOperationResult<Asset>), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _assetService.GetAll();

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result);
        }


        /// <summary>
        /// Gets an asset by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IOperationResult<Asset>), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetById([FromRoute] int id)
        {
            var result = await _assetService.GetById(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Creates a new asset
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CreateAssetResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IOperationResult<Asset>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] Asset asset)
        {
            if (asset.Id > 0) return BadRequest(new {message = "Id can't be greater then 0"});

            var result = await _assetService.AddNewAsset(asset);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var data = result.Data;

            CreateAssetResponse response = new()
            {
                Asset = data,
                Url = $"/assets/{data.Id}"
            };

            return Created("http://localhost:4001/assets/1", response);
        }

        /// <summary>
        /// Replaces an asset, JSON must have ID property set
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CreateAssetResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(IOperationResult<Asset>), StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> UpdateAsset([FromBody] Asset asset)
        {
            if (asset.Id == 0) return BadRequest(new {message = "Id must be greater than 0"});

            var result = await _assetService.UpdateAsset(asset);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var data = result.Data;

            return Accepted();
        }

        /// <summary>
        /// Deletes a complete asset
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(CreateAssetResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(IOperationResult<Asset>), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset([FromRoute] int id)
        {
            if (id == 0) return BadRequest(new {message = "Id must be greater than 0"});

            IOperationResult<Asset> result = await _assetService.RemoveAsset(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            Asset data = result.Data;

            return Accepted(data);
        }
    }
}