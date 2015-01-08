namespace WMP.Core
{
    /// <summary>
    ///     A unit of work interface for an EntityFramework DbContext class
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        ///     Commits the unit of work.
        /// </summary>
        /// <returns></returns>
        void Commit();
    }
}