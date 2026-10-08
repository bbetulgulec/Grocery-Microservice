using manav.order.domain.Entities;

namespace manav.order.applicaiton.Interfaces;

public interface IOrderingRepository
{
    public List<Ordering> GetOrderingsByUserId(string userId);
    
}