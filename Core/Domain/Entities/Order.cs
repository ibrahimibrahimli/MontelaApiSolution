using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Basket Basket { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
