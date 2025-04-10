using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public string Adress { get; set; }
        public virtual Basket Basket { get; set; }
        public string OrderNumber { get; set; }
        public virtual CompletedOrder CompletedOrder { get; set; }
    }
}
