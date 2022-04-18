using System;

namespace FormsCreator.Domain.Core
{
    /// <summary>
    /// Interfejs dla encji audytowanych
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// Utworzone o
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Utworzone przez Id
        /// </summary>
        long? CreatedBy { get; set; }

        /// <summary>
        /// Zmodyfikowane o
        /// </summary>
        DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Utworzone przez Id
        /// </summary>
        long? ModifiedBy { get; set; }
    }
}