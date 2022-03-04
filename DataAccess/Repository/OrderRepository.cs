using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        void IOrderRepository.DeleteOrder(int orderID) => OrderDAO.Instance.Remove(orderID);

        IEnumerable<OrderObject> IOrderRepository.GetOrders() => OrderDAO.Instance.GetOrderList();

        IEnumerable<OrderObject> IOrderRepository.GetOrderByMemberID(int memberID) => OrderDAO.Instance.GetOrderByMemberID(memberID);

        void IOrderRepository.InsertOrder(OrderObject order) => OrderDAO.Instance.AddNew(order);

        void IOrderRepository.UpdateOrder(OrderObject order) => OrderDAO.Instance.Update(order);

    }
}
