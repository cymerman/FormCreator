using System;

namespace FormsCreator.Domain.Core.Base
{
    /// <summary>
    /// Atrybut służący określeniu typu kolumny filtra
    /// </summary>
    public class ColumnTypeAttribute : Attribute
    {
        /// <summary>
        /// Rodzaj kolumny
        /// </summary>
        public ColumnType ColumnType { get; }

        /// <summary>
        /// Konstruktor, inicjalizuje instancję obiektu klasy ColumnTypeAttribute
        /// </summary>
        /// <param name="columnType">Rodzaj kolumny</param>
        public ColumnTypeAttribute(ColumnType columnType)
        {
            ColumnType = columnType;
        }
    }

    /// <summary>
    /// Atrybut służący określeniu nazwy pola
    /// </summary>
    public class FieldNameAttribute : Attribute
    {
        /// <summary>
        /// Nazwa pola
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// Konstruktor, inicjalizuje instancję obiektu klasy FieldNameAttribute
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldNameAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}