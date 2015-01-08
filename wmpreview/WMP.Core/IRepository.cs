namespace WMP.Core
{
    /// <summary>
    /// A repository pattern interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<in TKey, T>
     where T : class
    {
        /// <summary>
        /// Finds an item by the identifier        
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The item that is found or null if not found</returns>
        T FindById(TKey id);

        /// <summary>
        /// Adds an item to the repository for 
        /// potential saving in the future.
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The Entity that was added</returns>
        OperationResult<T> Add(T entity);

        /// <summary>
        /// Adds an item to the repository for 
        /// potential saving in the future.
        /// </summary>
        /// <param name="entity">The entity to add</param>
        void Delete(TKey key);

        /// <summary>
        /// Updates an entity in the data store
        /// </summary>
        /// <param name="entity"></param>
        void Save(T entity);
    }
}
