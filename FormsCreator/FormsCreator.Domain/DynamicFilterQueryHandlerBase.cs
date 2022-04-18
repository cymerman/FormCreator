using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using FormsCreator.Domain.Core.Base;
using FormsCreator.Domain.Core.Base.Queries;

namespace FormsCreator.Domain
{
    /// <summary>
    /// Klasa bazowa dla metody szablonowej obsługującej zapytania, używająca refleksji do budowania filtorwania i sortowania
    /// </summary>
    /// <typeparam name="TQuery">Typ zapytania</typeparam>
    /// <typeparam name="TResult">Typ wynikowy</typeparam>
    public abstract class DynamicFilterQueryHandlerBase<TQuery, TResult> : FilterQueryHandlerBase<TQuery, TResult> where TQuery : IFilterQuery<TResult>
    {
        /// <summary>
        /// Metoda nakładająca filtry na queryable za pomocą reflekcji
        /// </summary>
        /// <param name="result">Iqueryable z typem wynikowym</param>
        /// <param name="query">Obiekt query ktory zawiera potrzebne parametry do filtrowania</param>
        /// <returns>Przefiltrowane Iqueryable z typem wynikowym</returns>
        protected override IQueryable<TResult> ApplyFilter(IQueryable<TResult> result, TQuery query)
        {
            var properties = query.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.PropertyType != typeof(QueryFilterItem))
                {
                    continue;
                }

                var propertyInstance = propertyInfo.GetValue(query);
                if (propertyInstance == null)
                {
                    continue;
                }

                var propertyType = propertyInfo.PropertyType;
                string fieldName;
                if (!(propertyInfo.GetCustomAttribute(typeof(FieldNameAttribute), true) is FieldNameAttribute fieldNameAttribute))
                {
                    fieldName = propertyInfo.Name;
                }
                else
                {
                    fieldName = fieldNameAttribute.FieldName;
                }

                object[] parameters;
                if (!(propertyInfo.GetCustomAttribute(typeof(ColumnTypeAttribute), true) is ColumnTypeAttribute attrbiute))
                {
                    parameters = new object[] {fieldName};
                }
                else
                {
                    var name = fieldName;
                    if (attrbiute.ColumnType == ColumnType.Date)
                    {
                        var prop = typeof(TResult).GetProperty(fieldName);
                        if (prop != null)
                        {
                            var type = prop.PropertyType;

                            if (type == typeof(DateTime?))
                            {
                                name = $"{name}.HasValue && {name}.Value.Date";
                            }
                            else
                            {
                                name += ".Date";
                            }
                        }
                    }

                    parameters = new object[] {name, attrbiute.ColumnType};
                }

                result = result.Where(propertyType
                    .InvokeMember("GetExpression", BindingFlags.InvokeMethod, null, propertyInstance, parameters)
                    .ToString());
            }

            return result;
        }

        /// <summary>
        /// Metoda sortująca Iqueryable z typem wynikowym
        /// </summary>
        /// <param name="query">Obiekt query który</param>
        /// <param name="sorting">Obiekt zawierający informacje o sortowaniu</param>
        /// <param name="groupFiledName">Kolumna po której mają być grupowane dane</param>
        /// <returns>Posortowane query z typem wynikowym</returns>
        protected override IQueryable<TResult> ApplySorting(IQueryable<TResult> query, Sorting sorting, string groupFiledName = null)
        {
            if (string.IsNullOrEmpty(sorting?.FieldName))
            {
                return query.OrderBy("Id ascending");
            }

            return query.OrderBy($"{sorting.FieldName} {(sorting.Direction == SortDirection.Asc ? "ascending" : "descending")}");
        }
    }
}