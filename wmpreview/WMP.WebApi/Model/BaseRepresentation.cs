using System;
using WMP.Core.Model;

namespace WMP.WebApi.Model
{
    public abstract class BaseRepresentation<TKey> : Representation, IIdentified<TKey>
            where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}