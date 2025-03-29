namespace DomainModel.Entities;

public interface IRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken  = default);
    Task Save(CancellationToken cancellationToken = default);
}