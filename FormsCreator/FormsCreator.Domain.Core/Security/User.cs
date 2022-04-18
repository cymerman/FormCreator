
namespace FormsCreator.Domain.Core.Security
{
    public class User : Entity
    {
        /// <summary>
        /// Czy jest adminem
        /// </summary>
        public bool IsAdmin { get; set; } = false;
        
        /// <summary>
        /// Login użytkownika
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Hasło użytkownika
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}