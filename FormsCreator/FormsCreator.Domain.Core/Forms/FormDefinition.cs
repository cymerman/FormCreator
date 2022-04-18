
using System;
using System.Collections.Generic;
using FormsCreator.Domain.Core.Exceptions;
using FormsCreator.Domain.Core.Security;

namespace FormsCreator.Domain.Core.Forms
{
    public class FormDefinition : Entity
    {
        /// <summary>
        /// guid formularza
        /// </summary>
        public string FormUid { get; set; }
        
        /// <summary>
        /// Twórca
        /// </summary>
        public User User { get; set; }
        
        /// <summary>
        /// Id twórcy definicji formularza
        /// </summary>
        public long UserId { get; set; }
        
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
        /// Formularz w postacji json
        /// </summary>
        public string Form { get; set; }

        /// <summary>
        /// Hasło do formularza
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Status definicji
        /// </summary>
        public FormDefinitionStatus Status { get; set; }
        
        /// <summary>
        /// Lista wniosków formularza
        /// </summary>
        public List<FormDefinition> Children { get; set; }
        
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
        /// Metoda służy do zmiany statusu definicji formularza
        /// </summary>
        /// <param name="newStatus">Nowy status</param>
        public void SetStatus(FormDefinitionStatus newStatus)
        {
            if ((Status == FormDefinitionStatus.WorkCopy && newStatus != FormDefinitionStatus.Confirmed
                 || (Status == FormDefinitionStatus.Confirmed && (newStatus != FormDefinitionStatus.Published && newStatus != FormDefinitionStatus.WorkCopy))
                 || (Status == FormDefinitionStatus.Published && newStatus != FormDefinitionStatus.NotActive)
                 || (Status == FormDefinitionStatus.NotActive && newStatus != FormDefinitionStatus.Published)) && Status != newStatus)
            {
                throw new DomainException(DomainException.AppExceptionCodes.StatusChangeError, "Wrong status");
            }

            Status = newStatus;
        }
        
        public FormDefinition Clone()
        {
            var clone = (FormDefinition) MemberwiseClone();
            clone.Id = 0;
            clone.FormUid = Guid.NewGuid().ToString();
            clone.Status = FormDefinitionStatus.WorkCopy;
            clone.Title = clone.Title + "_" + "kopia";

            return clone;
        }
    }
}