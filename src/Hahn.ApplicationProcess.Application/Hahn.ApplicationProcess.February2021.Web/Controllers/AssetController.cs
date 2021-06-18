using Microsoft.AspNetCore.Mvc;

namespace Hahn.ApplicationProcess.February2021.Web.Controllers
{
    using System.Threading.Tasks;
    using Domain;
    using Domain.Managers;
    using Domain.Models;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Assets controller
    /// </summary>
    [ApiController]
    [Route("assets")]
    public class AssetController : ControllerBase
    {
        private readonly AssetManager _assetManager;

        /// <summary>
        /// Creates a new instance of <see cref="AssetController"/>
        /// </summary>
        /// <param name="assetManager">An instance of <see cref="AssetManager"/></param>
        public AssetController(AssetManager assetManager)
        {
            _assetManager = assetManager;
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
            var result = await _assetManager.GetById(id);

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
        [ProducesResponseType(typeof(CreateAssetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IOperationResult<Asset>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] Asset asset)
        {
            var result = await _assetManager.AddNewAsset(asset);

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
        
        
    }
}