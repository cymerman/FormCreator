using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormsCreator.Domain.Core.Forms
{
    /// <summary>
    /// Formularz
    /// </summary>
    public class Form 
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
        /// Id definicji formularza na podstawie której został stworzony formularz
        /// </summary>
        public long FormDefinitionId { get; set; }

        /// <summary>
        /// Definicja formularza
        /// </summary>
        public FormDefinition FormDefinition { get; set; }
        
        /// <summary>
        /// Json data
        /// </summary>
        public string Data { get; set; }
        
        /// <summary>
        /// Historia formularza
        /// </summary>
        public IList<FormHistory> FormHistories { get; set; }
    }
}