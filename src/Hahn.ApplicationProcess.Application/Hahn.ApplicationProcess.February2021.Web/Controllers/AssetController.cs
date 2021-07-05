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
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ISet<Asset> result = await _assetService.GetAll();

            return Ok(result);
        }


        /// <summary>
        /// Gets an asset by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Asset), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetById([FromRoute] int id)
        {
            Asset result = await _assetService.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// Creates a new asset
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CreateAssetResponse), StatusCodes.Status201Created)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] Asset asset)
        {
            if (asset.Id > 0) return BadRequest(new {message = "Id can't be greater then 0"});

            Asset result = await _assetService.AddNewAsset(asset);

            CreateAssetResponse response = new()
            {
                Asset = result,
                Url = $"/assets/{result.Id}"
            };

            return Created("http://localhost:4001/assets/1", response);
        }

        /// <summary>
        /// Replaces an asset, JSON must have ID property set
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(CreateAssetResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> UpdateAsset([FromBody] Asset asset)
        {
            if (asset.Id == 0) return BadRequest(new {message = "Id must be greater than 0"});

            Asset result = await _assetService.UpdateAsset(asset);

            return Accepted();
        }

        /// <summary>
        /// Deletes a complete asset
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(CreateAssetResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset([FromRoute] int id)
        {
            if (id == 0) return BadRequest(new {message = "Id must be greater than 0"});

            Asset result = await _assetService.RemoveAsset(id);

            return Accepted(result);
        }
    }
}