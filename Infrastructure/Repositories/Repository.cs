using DomainModel.Entities;
using DomainModel.Entities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class Repository<TEntity>(AppDbContext context) : IRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> Entities = context.Set<TEntity>();

    public void Add(TEntity entity)
    {
        Entities.Add(entity);
    }

    public async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await Entities.FindAsync(id, cancellationToken) ??
               throw new EntityNotFoundException($"Entity with ID {id} not found.");
    }

    public async Task Save(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}