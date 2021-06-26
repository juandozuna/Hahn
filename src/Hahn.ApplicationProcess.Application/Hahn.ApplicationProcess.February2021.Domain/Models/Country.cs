namespace Hahn.ApplicationProcess.February2021.Domain.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a document from the API
    /// </summary>
    public sealed class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alpha2Code")]
        public string Alpha2Code { get; set; }

        [JsonProperty("alpha3Code")]
        public string Alpha3Code { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("subregion")]
        public string Subregion { get; set; }

        [JsonProperty("population")]
        public long? Population { get; set; }

        [JsonProperty("demonym")]
        public string Demonym { get; set; }

        [JsonProperty("area")]
        public long? Area { get; set; }

        [JsonProperty("gini")]
        public double? Gini { get; set; }
        
        
        [JsonProperty("flag")]
        public Uri Flag { get; set; }

    }
}