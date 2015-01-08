namespace WMP.Core
{
    public class OperationResult<T> : OperationResult where T : class
    {
        #region Constructors
        public OperationResult()
        {
            Initialize();
        }
        public OperationResult(T result)
            : this()
        {
            SetAsSuccessful(item: result);
        }
        #endregion

        public T Result { get; set; }

        public bool OkWithItem
        {
            get
            {
                return (Ok && Result != null);
            }
        }
        public bool OkWithNoItem
        {
            get { return (Ok && Result == null); }
        }

        public void SetAsSuccessful(string message = default(string), T item = default(T))
        {

            base.SetAsSuccessful();

            Result = item;

            //If we have no item and no message, then set a basic item no found message
            if (item == default(T) && message == default(string))
            {
                AddMessageToList(string.Empty, "Item not found");
            }
            if (message != default(string))
            {
                AddMessageToList(string.Empty, message);
            }
        }

        public void SetAsFailed(string message, T item = default(T))
        {
            SetFailure(item);
            AddMessageToList(item == default(T) ? string.Empty : item.ToString(), message);
        }

        /// <summary>
        /// Returns the Item if successfull, otherwise null
        /// </summary>
        /// <returns>Item</returns>
        public T GetResultIfSuccessful()
        {
            return OkWithItem
                ? Result
                : null;
        }

        #region Privates

        private void SetFailure(T result)
        {
            SetFailure();
            Result = result;
        }

        private void Initialize()
        {
            Result = null;
        }
        #endregion

    }
}