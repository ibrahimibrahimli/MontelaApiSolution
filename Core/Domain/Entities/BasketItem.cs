using Domain.Entities.Common;

namespace Domain.Entities
{
    public class BasketItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid BasketId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Basket Basket { get; set; }
    }
}
