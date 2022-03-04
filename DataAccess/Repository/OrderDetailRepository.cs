using BusinessObject;
using System.Collections.Generic;


namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {

        void IOrderDetailRepository.DeleteOrderDetail(int orderID, int productID) => OrderDetailDAO.Instance.Remove(orderID, productID);

        IEnumerable<OrderDetailObject> IOrderDetailRepository.GetDetailListByOrderID(int orderID) => OrderDetailDAO.Instance.GetDetailListByOrderID(orderID);

        void IOrderDetailRepository.InsertOrderDetail(OrderDetailObject orderDetail) => OrderDetailDAO.Instance.AddNew(orderDetail);

        void IOrderDetailRepository.UpdateOrderDetail(OrderDetailObject orderDetail) => OrderDetailDAO.Instance.Update(orderDetail);

    }
}
