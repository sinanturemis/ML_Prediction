namespace PredictionApp.Repository
{
    /// <summary>
    /// This interface exists for standardize repositories
    /// </summary>
    /// <typeparam name="TEntity">An entity class derived from IEntity interface</typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
      
    }
}
