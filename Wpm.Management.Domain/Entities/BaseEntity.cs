using System;
using System.Collections.Generic;
using System.Text;

namespace Wpm.Management.Domain.Entities
{
    public abstract class BaseEntity :IEquatable<BaseEntity>
    {
        public Guid Id { get; init; }

        public bool Equals(BaseEntity? other)
        {
            return other?.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity? left, BaseEntity? right)
        {
            return left?.Id == right?.Id;
        }

        public static bool operator !=(BaseEntity? left, BaseEntity? right)
        {
            return left?.Id != right?.Id;
        }
    }
}
