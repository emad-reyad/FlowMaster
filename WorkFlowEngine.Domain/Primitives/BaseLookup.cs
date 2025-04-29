using System.ComponentModel.DataAnnotations;

namespace WorkFlowEngine.Domain.Primitives
{
    public abstract class BaseLookup : Entity
    {
        //protected Lookup(int id) : base(id)
        //{
        //}
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(150)]
        //public string Code { get; set; }
        //[StringLength(4000)]
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
