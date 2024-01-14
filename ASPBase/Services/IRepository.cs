namespace ASPBase.Services
{
    public interface IRepository<T, TId, TSurName> where T : class
    {
        IList<T> GetAll();
        T GetById(TId id);
        int Create(T item);
        int Update(T item);
        int Delete(TId id);
        int DeleteAll();
        
    }
}
