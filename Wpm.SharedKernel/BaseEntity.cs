
namespace Wpm.SharedKernel
{
    public abstract class BaseEntity :IEquatable<BaseEntity>
    {
        public Guid Id { get; protected set; }

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
