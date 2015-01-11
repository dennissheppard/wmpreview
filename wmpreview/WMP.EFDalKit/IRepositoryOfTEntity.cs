namespace WMP.EFDalKit
{
    /// <summary>
    /// A repository pattern Interface for Data Access
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
            where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>        
        /// <param name="entity">The entity to refresh</param>        
        void RefreshOriginalValues(TEntity entity);

        /// <summary>
        /// Detects changes and then refreshes the given entity
        /// Default refresh mode of store wins
        /// </summary>
        /// <param name="entity">The entity to refresh</param>
        void Refresh(TEntity entity);

        /// <summary>
        /// Attaches an entity to the context
        /// </summary>
        /// <param name="entity">The entity to attach</param>
        void Attach(TEntity entity);

        /// <summary>
        /// Create a new instance of the entity
        /// </summary>
        /// <returns>A new instance of the entity, attached to the Context</returns>
        TEntity Create();

        /// <summary>
        /// Create attach option
        /// </summary>
        /// <param name="shouldAttach">Should this entity be attached to the context</param>
        /// <returns>A new instance of the entity</returns>
        TEntity Create(bool shouldAttach);

        /// <summary>
        /// Adds an item to the repository for 
        /// potential saving in the future.
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The Entity that was added</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds an item to the repository for 
        /// potential saving in the future.
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The Entity that was added</returns>
        TEntity Delete(TEntity entity);
    }
}