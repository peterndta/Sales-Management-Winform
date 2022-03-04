using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<OrderObject> GetOrders();
        
        IEnumerable<OrderObject> GetOrderByMemberID(int memberID);
   
        void InsertOrder(OrderObject order);

        void UpdateOrder(OrderObject order);

        void DeleteOrder(int orderID);
    }
}
