using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormsCreator.Domain.Core
{
    /// <summary>
    /// Klasa bazowa encji
    /// </summary>
    public abstract class Entity : IAuditableEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Utworzone o
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Utworzone przez Id
        /// </summary>
        public long? CreatedBy { get; set; }

        /// <summary>
        /// Zmodyfikowane o
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Utworzone przez Id
        /// </summary>
        public long? ModifiedBy { get; set; }
    }
}