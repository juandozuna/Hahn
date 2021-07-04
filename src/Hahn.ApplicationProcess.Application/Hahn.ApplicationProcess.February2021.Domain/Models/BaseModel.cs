namespace Hahn.ApplicationProcess.February2021.Domain.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents the base model   
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Represents the primary key of entity
        /// </summary>
        public int Id { get; set; }
    }
}