namespace FormsCreator.Domain.Core.Base.Queries
{
    /// <summary>
    /// Interfejs, który muszą implementować wszystkie klasy przetwarzane przez QueryProcessor
    /// </summary>
    /// <typeparam name="TResult">Typ wyniku zwracanego przez kwerendę</typeparam>
    public interface IQuery<TResult>
    {
    }
}