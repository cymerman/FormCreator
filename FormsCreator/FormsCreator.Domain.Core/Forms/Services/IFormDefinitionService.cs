using System.Threading.Tasks;

namespace FormsCreator.Domain.Core.Forms.Services
{
    public interface IFormDefinitionService
    {
        /// <summary>
        /// Dodanie definicji formularza
        /// </summary>
        /// <returns></returns>
        Task AddForm();
    }
}