using BusinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccess
{
    public class OrderDetailDAO : BaseDAL
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();

        private OrderDetailDAO() { }

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }
        //----------------------------------------------------------
        public List<OrderDetailObject> GetDetailListByOrderID(int orderID)
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select OrderId, ProductId, UnitPrice, Quantity, Discount from OrderDetail where OrderId = @OrderId";
            var details = new List<OrderDetailObject>();
            try
            {
                var param = DataProvider.CreateParameter("@OrderId", 4, orderID, DbType.Int32);
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    details.Add(new OrderDetailObject
                    {
                        OrderID = dataReader.GetInt32(0),
                        ProductID = dataReader.GetInt32(1),
                        UnitPrice = dataReader.GetDecimal(2),
                        Quantity = dataReader.GetInt32(3),
                        Discount = dataReader.GetDouble(4),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return details;
        }
        //----------------------------------------------------------
        public OrderDetailObject GetOrderDetailByIDs(int orderID, int productID)
        {
            OrderDetailObject detail = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select OrderId, ProductId, UnitPrice, Quantity, Discount from OrderDetail where OrderId = @OrderId AND ProductId=@ProductId";
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(DataProvider.CreateParameter("@OrderId", 4, orderID, DbType.Int32));
                parameters.Add(DataProvider.CreateParameter("@ProductId", 4, productID, DbType.Int32));
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, parameters.ToArray());
                if (dataReader.Read())
                {
                    detail = new OrderDetailObject
                    {
                        OrderID = dataReader.GetInt32(0),
                        ProductID = dataReader.GetInt32(1),
                        UnitPrice = dataReader.GetDecimal(2),
                        Quantity = dataReader.GetInt32(3),
                        Discount = dataReader.GetDouble(4),
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return detail;
        }
        //----------------------------------------------------------
        public void AddNew(OrderDetailObject orderDetail)
        {
            try
            {
                OrderDetailObject isEmpty = GetOrderDetailByIDs(orderDetail.OrderID, orderDetail.ProductID);
                if (isEmpty == null)
                {
                    string SQLInsert = "Insert OrderDetail values(@OrderId, @ProductId, @UnitPrice, @Quantity, @Discount)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@OrderId", 4, orderDetail.OrderID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@ProductId", 4, orderDetail.ProductID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@UnitPrice", 50, orderDetail.UnitPrice, DbType.Decimal));
                    parameters.Add(DataProvider.CreateParameter("@Quantity", 4, orderDetail.Quantity, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@Discount", 50, orderDetail.Discount, DbType.Double));
                    DataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The orderDetail is already exist");
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        //----------------------------------------------------------
        public void Update(OrderDetailObject orderDetail)
        {
            try
            {
                OrderDetailObject isEmpty = GetOrderDetailByIDs(orderDetail.OrderID, orderDetail.ProductID);
                if (isEmpty != null)
                {
                    string SQLInsert = "Update Member set OrderId = @OrderId, ProductId = @ProductId, UnitPrice = @UnitPrice, Quantity = @Quantity, Discount = @Discount" +
                        " where OrderId = @OrderId AND ProductId=@ProductId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@OrderId", 4, orderDetail.OrderID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@ProductId", 4, orderDetail.ProductID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@UnitPrice", 50, orderDetail.UnitPrice, DbType.Decimal));
                    parameters.Add(DataProvider.CreateParameter("@Quantity", 4, orderDetail.Quantity, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@Discount", 50, orderDetail.Discount, DbType.Double));
                    DataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The orderDetail is already exist");
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        //----------------------------------------------------------
        public void Remove(int orderID, int productID)
        {
            try
            {
                OrderDetailObject isEmpty = GetOrderDetailByIDs(orderID, productID);
                if (isEmpty != null)
                {
                    string SQLDelete = "Delete OrderDetail where OrderId = @OrderId AND ProductId=@ProductId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@OrderId", 4, orderID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@ProductId", 4, productID, DbType.Int32));
                    DataProvider.Update(SQLDelete, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The orderDetail is not exist");
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

        }
        
        //------------------------------END--------------------------
    }
}
