using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlowEngine.Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private init; }
        //protected Entity(int id)=>this.Id = id;

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            if (obj is not Entity entity) return false;
            return entity.Id == Id;
        }

        public override int GetHashCode() => Id.GetHashCode() + 71;

        public bool Equals(Entity? other)
        {
            if (other == null) return false;
            if (other.GetType() != GetType()) return false;
            return other.Id == Id;
        }

        public static bool operator ==(Entity? first, Entity? second) => first != null && second != null && first.Equals(second);
        public static bool operator !=(Entity? first, Entity? second) => !(first == second);


    }
}
