using System;

namespace FormsCreator.Domain.Core.Base
{
    /// <summary>
    /// Element filtru
    /// </summary>
    public class QueryFilterItem
    {
        /// <summary>
        /// Wartość elementu
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Operator
        /// </summary>
        public FilterOperator Operator { get; set; }

        /// <summary>
        /// Metoda tworząca linq dynamic expression (dynamiczne zapytanie LINQ)
        /// </summary>
        /// <param name="fieldName">Nazwa pola</param>
        /// <param name="columnType">Typ kolumny</param>
        /// <returns>Linq dynamic expression w postaci ciągu znaków</returns>
        public string GetExpression(string fieldName, ColumnType columnType = ColumnType.Integer)
        {
            var falseExpression = "false";

            if (columnType == ColumnType.Text)
            {
                if (Operator == FilterOperator.Eq)
                {
                    if (Value.Contains("|")) //multiselect
                    {
                        var tempArray = Value.Split("|");
                        var result = string.Empty;
                        result = " ";
                        for (int i = 0; i < tempArray.Length; i++)
                        {
                            result += fieldName + " != null && " + fieldName + ".ToLower()  == \"" +
                                      tempArray[i].ToLower() + "\"";
                            if (i + 1 != tempArray.Length)
                            {
                                result += " || ";
                            }
                        }

                        return result;
                    }

                    return fieldName + " != null && " + fieldName + ".ToLower()  == \"" + Value.ToLower().Replace("\"","\"\"") + "\"";
                }

                if (Operator == FilterOperator.Neq)
                {
                    if (Value.Contains("|")) //multiselect
                    {
                        var tempArray = Value.Split("|");
                        var result = string.Empty;
                        result = " ";
                        for (int i = 0; i < tempArray.Length; i++)
                        {
                            result += fieldName + " != null && " + fieldName + ".ToLower()  != \"" +
                                      tempArray[i].ToLower() + "\"";
                            if (i + 1 != tempArray.Length)
                            {
                                result += " || ";
                            }
                        }

                        return result;
                    }

                    return fieldName + " != null && " + fieldName + ".ToLower()  != \"" + Value.ToLower().Replace("\"","\"\"") + "\"";
                }

                if (Operator == FilterOperator.Nct)
                {
                    return fieldName + " != null && " + "!" + fieldName +".ToLower().Contains(\"" + Value.ToLower().Replace("\"","\"\"") + "\")";
                }
                
                return fieldName + " != null && " + fieldName + GetOperatorCharacter(Operator) + Value.ToLower().Replace("\"","\"\"") + "\")";
            }

            if (columnType == ColumnType.Decimal)
            {
                if (!decimal.TryParse(Value, out _))
                {
                    return falseExpression;
                }

                return fieldName + GetOperatorCharacter(Operator) + Value.Replace(",", ".") + "M";
            }

            if (columnType == ColumnType.Date)
            {
                if (!DateTime.TryParse(Value, out _))
                {
                    return falseExpression;
                }

                return fieldName + GetOperatorCharacter(Operator) + "\"" + Value + "\"";
            }

            if (columnType == ColumnType.Bool)
            {
                if (!bool.TryParse(Value, out _))
                {
                    return falseExpression;
                }

                return fieldName + GetOperatorCharacter(Operator) + Value;
            }

            if (!int.TryParse(Value, out _))
            {
                return falseExpression;
            }

            if (Operator == FilterOperator.Eq)
            {
                if (Value.Contains("|")) //multiselect
                {
                    var tempArray = Value.Split("|");
                    var result = string.Empty;
                    result = " ";
                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        result += fieldName + GetOperatorCharacter(Operator) +
                                  tempArray[i];
                        if (i + 1 != tempArray.Length)
                        {
                            result += " || ";
                        }
                    }

                    return result;
                }
            }
            
            if (Operator == FilterOperator.Neq)
            {
                if (Value.Contains("|")) //multiselect
                {
                    var tempArray = Value.Split("|");
                    var result = string.Empty;
                    result = " ";
                    for (int i = 0; i < tempArray.Length; i++)
                    {
                        result += fieldName + GetOperatorCharacter(Operator) +
                                  tempArray[i];
                        if (i + 1 != tempArray.Length)
                        {
                            result += " || ";
                        }
                    }

                    return result;
                }
            }

            return fieldName + GetOperatorCharacter(Operator) + Value;
        }

        /// <summary>
        /// Metoda zwracająca operatora na podstawie typu operatora
        /// </summary>
        /// <param name="filterOperator">Typ operatora</param>
        /// <returns>Zwraca operator w postaci ciągu znaków</returns>
        /// <exception cref="ArgumentOutOfRangeException">Gdy nie znaleziono operatora</exception>
        private string GetOperatorCharacter(FilterOperator filterOperator)
        {
            switch (filterOperator)
            {
                case FilterOperator.Lt:
                    return " < ";
                case FilterOperator.Gt:
                    return " > ";
                case FilterOperator.Eq:
                    return " == ";
                case FilterOperator.Lte:
                    return " <= ";
                case FilterOperator.Gte:
                    return " >= ";
                case FilterOperator.Sw:
                    return ".ToLower().StartsWith(\"";
                case FilterOperator.Ew:
                    return ".ToLower().EndsWith(\"";
                case FilterOperator.Ct:
                    return ".ToLower().Contains(\"";
                case FilterOperator.Nct:
                    return "";
                case FilterOperator.Neq:
                    return " != ";
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterOperator), filterOperator, null);
            }
        }
    }
}