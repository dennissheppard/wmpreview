using System.Collections.Generic;

namespace WMP.Core
{
    /// <summary>
    /// A utility class that allows operations within the application to return result codes rather than exceptions
    /// </summary>
    public class OperationResult
    {
        #region Constructors
        public OperationResult()
        {
            Initialize();
        }

        public OperationResult(string message)
            : this()
        {

            AddMessageToList(string.Empty, message);

        }
        public OperationResult(ResultCode code)
            : this()
        {
            Code = code;
        }
        public OperationResult(string message, ResultCode code)
            : this(message)
        {
            Code = code;
        }
        
        #endregion

        /// <summary>
        /// The status of this operation
        /// </summary>
        public ResultCode Code { get; set; }

        /// <summary>
        /// Is this result OK (e.g. Code is Success and not failure or unknown)
        /// </summary>
        public bool Ok
        {
            get { return Code == ResultCode.Success; }
        }

        /// <summary>
        /// Is this result a failure (e.g. not Success and Not Unknown)
        /// </summary>
        public bool Failed
        {
            get { return Code == ResultCode.Failure; }
        }

        /// <summary>
        /// Error messages
        /// </summary>
        public IDictionary<string, string> Messages { get; set; }

        /// <summary>
        /// Returns a concatenated string of messages seperated by commas
        /// </summary>
        public string Message
        {
            get { return string.Join(", ", Messages.Values); }
        }

        #region Set as Failed
        public void SetAsFailed(string key, string message)
        {
            SetFailure();
            AddMessageToList(key, message);
        }

        public void SetAsFailed(IEnumerable<KeyValuePair<string, string>> messages)
        {
            SetFailure();
            foreach (var item in messages)
            {                
                Messages.Add(item.Key, item.Value);
            }
        }
        #endregion

        #region Set as successful
        public void SetAsSuccessful()
        {
            Code = ResultCode.Success;
        } 
        #endregion

        #region Protected methods
        protected void SetFailure()
        {
            Code = ResultCode.Failure;
        }

        protected void AddMessageToList(string key, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            if (!Messages.ContainsKey(key))
            {
                Messages.Add(key, message);
            }
        } 
        #endregion
        
        #region Private methods

        private void Initialize()
        {
            Code = ResultCode.Unknown;
            Messages = new Dictionary<string, string>();
        } 
        #endregion
    }
}
