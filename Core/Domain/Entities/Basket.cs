using Domain.Entities.Common;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Order Order { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
