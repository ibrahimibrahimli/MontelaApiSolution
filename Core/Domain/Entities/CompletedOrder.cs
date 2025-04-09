using Domain.Entities.Common;

namespace Domain.Entities
{
    public class CompletedOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
