namespace FormsCreator.Domain.Core.Base
{
    /// <summary>
    /// Operator filtra
    /// </summary>
    public enum FilterOperator
    {
        /// <summary>
        /// Mniejszy niż
        /// </summary>
        Lt = 1,
        
        /// <summary>
        /// Większy niż
        /// </summary>
        Gt,
        
        /// <summary>
        /// Równy
        /// </summary>
        Eq,
        
        /// <summary>
        /// Mniejszy niż lub równy
        /// </summary>
        Lte,
        
        /// <summary>
        /// Większy niż lub równy
        /// </summary>
        Gte,
        
        /// <summary>
        /// Zaczyna się na
        /// </summary>
        Sw,
        
        /// <summary>
        /// Kończy się na
        /// </summary>
        Ew,
        
        /// <summary>
        /// Zawiera
        /// </summary>
        Ct,
        
        /// <summary>
        /// Nie równy
        /// </summary>
        Neq,
        
        /// <summary>
        /// Nie zawiera
        /// </summary>
        Nct,
    }
}