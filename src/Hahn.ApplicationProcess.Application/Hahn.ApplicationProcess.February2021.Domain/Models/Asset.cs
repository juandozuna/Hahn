using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hahn.ApplicationProcess.February2021.Domain.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;

    /// <summary>
    /// Represents the Asset class
    /// </summary>
    public sealed class Asset : BaseModel
    {

        /// <summary>
        /// Represents the name of the asset
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        /// Represents the department associated to the asset
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// Represents the country of the department
        /// </summary>
        public string CountryOfDepartment { get; set; }

        /// <summary>
        /// Represents the email address of the department
        /// </summary>
        public string EmailAddressOfDepartment { get; set; }

        /// <summary>
        /// Represents the date in which the item was bought
        /// </summary>
        public DateTimeOffset PurchaseDate { get; set; }

        /// <summary>
        /// Represents whether the asset is broken
        /// </summary>
        public bool Broken { get; set; } = false;
    }
}
