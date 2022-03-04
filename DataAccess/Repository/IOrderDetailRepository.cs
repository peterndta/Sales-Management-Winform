using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetailObject> GetDetailListByOrderID(int orderID);

        void InsertOrderDetail(OrderDetailObject orderDetail);

        void UpdateOrderDetail(OrderDetailObject orderDetail);

        void DeleteOrderDetail(int orderID, int productID);

    }
}
