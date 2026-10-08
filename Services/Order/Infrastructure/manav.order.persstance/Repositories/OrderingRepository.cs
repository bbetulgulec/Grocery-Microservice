using manav.order.applicaiton.Interfaces;
using manav.order.domain.Entities;
using manav.order.persstance.Context;

namespace manav.order.persstance.Repositories;

public class OrderingRepository:IOrderingRepository
{
    private readonly OrderContext _context;
    public OrderingRepository(OrderContext context)
    {
        _context = context;
    }
    public List<Ordering> GetOrderingsByUserId(string userId)
    {
        var values = _context.Orderings.Where(x => x.UserID == userId).ToList<Ordering>();
        return values;
    }
}