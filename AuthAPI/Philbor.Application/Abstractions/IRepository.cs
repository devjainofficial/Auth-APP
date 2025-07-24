namespace Philbor.Application.Abstractions
{
    public interface IRepository
    {
        Task<TEntity?> GetByIdAsync<TEntity>(int id)
            where TEntity : class;

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(CancellationToken cancellationToken = default)
            where TEntity : class;

        IQueryable<TEntity> GetQuery<TEntity>()
            where TEntity : class;

        Task AddAsync<TEntity>(TEntity entity,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        Task AddAsync<TEntity>(TEntity entity,
            bool saveChanges,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities,
            bool saveChanges,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        Task UpdateAsync<TEntity>(TEntity entity,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        Task UpdateAsync<TEntity>(TEntity entity,
            bool saveChanges,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        Task UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities,
            bool saveChanges,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        Task RemoveAsync<TEntity>(TEntity entity,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        Task RemoveAsync<TEntity>(TEntity entity,
            bool saveChanges,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities,
            bool saveChanges,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        IQueryable<T> GetSqlQuery<T>(string sql,
            params object[] parameters);

        Task<TResult> ExecuteInTransactionAsync<TResult>(Func<Task<TResult>> action,
            CancellationToken cancellationToken = default);

        Task ExecuteInTransactionAsync(Func<Task> action,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken ctx = default);
    }
}
