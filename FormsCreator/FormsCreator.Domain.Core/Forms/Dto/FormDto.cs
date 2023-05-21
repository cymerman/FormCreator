using System;

namespace FormsCreator.Domain.Core.Forms.Dto
{
    public class FormDto
    {
        /// <summary>
        /// guid formularza
        /// </summary>
        public string FormUid { get; set; }
        
        public string FormData { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}