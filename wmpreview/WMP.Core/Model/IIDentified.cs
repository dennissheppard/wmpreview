using System;

namespace WMP.Core.Model
{
    public interface IIdentified<TKey> where TKey : IEquatable<TKey>
    {
        
        /// <summary>
        /// The Unique identifying key for this item
        /// </summary>

        TKey Id { get; set; }
    }
}