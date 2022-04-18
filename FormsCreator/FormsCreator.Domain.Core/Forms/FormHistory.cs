using FormsCreator.Domain.Core.Security;

namespace FormsCreator.Domain.Core.Forms
{
    public class FormHistory : Entity
    {
        /// <summary>
        /// Tekst
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Id użytkownika
        /// </summary>
        public long UserId { get; set; }
        
        /// <summary>
        /// Użytkownik wykonujący akcję
        /// </summary>
        public User User { get; set; }
        
        /// <summary>
        /// Id formularza
        /// </summary>
        public long FormId { get; set; }
        
        /// <summary>
        /// Formularz
        /// </summary>
        public Form Form { get; set; }
    }
}