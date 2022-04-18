using System;

namespace FormsCreator.Domain.Core.Exceptions
{
    /// <summary>
    /// Wyjątek "nie znaleziono"
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public NotFoundException()
        {
        }

        /// <summary>
        /// Konstruktor 
        /// </summary>
        /// <param name="message">Wiadomość</param>
        public NotFoundException(string message) : base(message)
        {
        }
    }
}