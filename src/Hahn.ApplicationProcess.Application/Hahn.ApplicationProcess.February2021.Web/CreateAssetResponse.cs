namespace Hahn.ApplicationProcess.February2021.Web
{
    using Domain.Models;

    /// <summary>
    /// Response after creating an asset
    /// </summary>
    public sealed class CreateAssetResponse
    {
        /// <summary>
        /// Represents the asset in question
        /// </summary>
        public Asset Asset { get; set; }

        /// <summary>
        /// Represents the URL where the asset can be found
        /// </summary>
        /// <returns></returns>
        public string Url { get; set; }
    }
}