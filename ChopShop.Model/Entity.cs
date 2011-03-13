using System;
using System.Collections.Generic;
using ChopShop.Admin.Web.Models;

namespace ChopShop.Model
{
    public abstract class Entity<TId>
    {
        public virtual TId Id { get; set; }
        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TId>);
        }
        private static bool IsTransient(Entity<TId> obj)
        {
            return obj != null && Equals(obj.Id, default(TId));
        }
        private Type GetUnProxiedType()
        {
            return GetType();
        }
        public virtual bool Equals(Entity<TId> other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (!IsTransient(this) && !IsTransient(other) && Equals(Id, other.Id))
            {
                var otherType = other.GetUnProxiedType();
                var thisType = GetUnProxiedType();
                return thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(Id, default(TId)))
            {
                return base.GetHashCode();
            }
            return Id.GetHashCode();
        }

        private List<ErrorInfo> errors;
        public virtual List<ErrorInfo> Errors
        {
            get
            {
                return errors ?? new List<ErrorInfo>();
            }
        }

        public virtual void AddError(ErrorInfo errorInfo)
        {
            if (errors == null)
            {
                errors = new List<ErrorInfo>();
            }

            errors.Add(errorInfo);
        }
    }

    public abstract class Entity: Entity<Guid>
    {}
}
