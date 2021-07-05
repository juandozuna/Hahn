namespace Hahn.ApplicationProcess.February2021.Domain.Models
{
    using System;

    /// <summary>
    /// Represents a document from the API
    /// </summary>
    public sealed class Country
    {
        public string Name { get; set; }

        public string Alpha2Code { get; set; }

        public string Alpha3Code { get; set; }

        public string Capital { get; set; }

        public string Region { get; set; }

        public string Subregion { get; set; }

        public long? Population { get; set; }

        public string Demonym { get; set; }

        public long? Area { get; set; }

        public double? Gini { get; set; }

        public Uri Flag { get; set; }
    }
}