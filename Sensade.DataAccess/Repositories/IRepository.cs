namespace Sensade.DataAccess.Repositories;

public interface IRepository<T>
{
    public Task<bool> Create(T entityToCreate);

    public Task<IEnumerable<T>> Get();

    public Task<T?> Get(int id);

    public Task<bool> Update(T entityToUpdate);

    public Task<bool> Delete(int id);
}
