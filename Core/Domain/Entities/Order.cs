using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public string Adress { get; set; }
        public virtual Basket Basket { get; set; }


        //public virtual Customer Customer { get; set; }

        //public Guid CustomerId { get; set; }

        //public ICollection<Product> Products { get; set; }
    }
}
