using System;

namespace FormsCreator.Domain.Core.Forms.Dto
{
    /// <summary>
    /// Dto dla listy formularzy
    /// </summary>
    public class FormDefinitionDto
    {
        /// <summary>
        /// guid formularza
        /// </summary>
        public string FormUid { get; set; }
        
        /// <summary>
        /// Klucz encji
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Tytuł
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Opis
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Nazwa
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Status 
        /// </summary>
        public FormDefinitionStatus Status { get; set; }
        
        /// <summary>
        /// Czy ograniczona liczba plików
        /// </summary>
        public bool IsFileCountLimited { get; set; }

        /// <summary>
        /// Liczba plików ograniczona do
        /// </summary>
        public int? FileCount { get; set; }
        
        /// <summary>
        /// Minimalna liczba plików
        /// </summary>
        public int? FileCountMinimum { get; set; }
        
        /// <summary>
        /// Maksymalna liczba plików 
        /// </summary>
        public int? FileCountMaximum { get; set; }

        /// <summary>
        /// Utworzone przez
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Utworzone o
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Zmodyfikowane przez
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Zmodyfikowane o
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        
        /// <summary>
        /// Formularz w postacji json
        /// </summary>
        public string Form { get; set; }
        
        /// <summary>
        /// metoda do mapowania z encji na dto
        /// </summary>
        /// <param name="entity">Encja definicji formularza</param>
        /// <returns>Dto definicji formularza</returns>
        public static FormDefinitionDto MapFrom(FormDefinition entity)
        {
            return new FormDefinitionDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Status = entity.Status,
                Title = entity.Title,
                Form = entity.Form,
                FileCount = entity.FileCount,
                FileCountMaximum = entity.FileCountMaximum,
                FileCountMinimum = entity.FileCountMinimum,
                IsFileCountLimited = entity.IsFileCountLimited,
                Description = entity.Description,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedBy.ToString(),
                ModifiedOn = entity.ModifiedOn,
                ModifiedBy = entity.ModifiedBy.ToString(),
                FormUid = entity.FormUid
            };
        }
    }
}