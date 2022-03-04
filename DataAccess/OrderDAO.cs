using BusinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    //---------------------------------------------------
    public class OrderDAO : BaseDAL
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();

        private OrderDAO() { }

        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------------------
        public List<OrderObject> GetOrderList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select OrderId, MemberId, OrderDate, RequiredDate, ShippedDate, Freight from [Order] ";
            var orders = new List<OrderObject>();
            try
            {
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    orders.Add(new OrderObject
                    {
                        OrderID = dataReader.GetInt32(0),
                        MemberID = dataReader.GetInt32(1),
                        OrderDate = dataReader.GetDateTime(2),
                        RequiredDate = dataReader.GetDateTime(3),
                        ShippedDate = dataReader.GetDateTime(4),
                        Freight = dataReader.GetDecimal(5),
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
            return orders;
        }
        //-------------------------------------------------
        public OrderObject GetOrderByID(int orderID)
        {
            OrderObject order = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select OrderId, MemberId, OrderDate, RequiredDate, ShippedDate, Freight from [Order] where OrderId = @OrderId";
            try
            {
                var param = DataProvider.CreateParameter("@OrderId", 4, orderID, DbType.Int32);
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    order = new OrderObject
                    {
                        OrderID = dataReader.GetInt32(0),
                        MemberID = dataReader.GetInt32(1),
                        OrderDate = dataReader.GetDateTime(2),
                        RequiredDate = dataReader.GetDateTime(3),
                        ShippedDate = dataReader.GetDateTime(4),
                        Freight = dataReader.GetDecimal(5),
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
            return order;
        }
        //---------------------------------------------------------
        public List<OrderObject> GetOrderByMemberID(int memberID)
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select OrderId, MemberId, OrderDate, RequiredDate, ShippedDate, Freight from [Order] where MemberId = @MemberId";
            var orders = new List<OrderObject>();
            try
            {
                var param = DataProvider.CreateParameter("@MemberId", 4, memberID, DbType.Int32);
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    orders.Add(new OrderObject
                    {
                        OrderID = dataReader.GetInt32(0),
                        MemberID = dataReader.GetInt32(1),
                        OrderDate = dataReader.GetDateTime(2),
                        RequiredDate = dataReader.GetDateTime(3),
                        ShippedDate = dataReader.GetDateTime(4),
                        Freight = dataReader.GetDecimal(5),
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
            return orders;
        }
        //-------------------------------------------------
        public void AddNew(OrderObject order)
        {
            try
            {
                OrderObject isEmpty = GetOrderByID(order.OrderID);
                if (isEmpty == null)
                {
                    string SQLInsert = "Insert [Order] values(@OrderId, @MemberId, @OrderDate, @RequiredDate, @ShippedDate, @Freight)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@OrderId", 4, order.OrderID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@MemberId", 4, order.MemberID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@OrderDate", 50, order.OrderDate, DbType.DateTime));
                    parameters.Add(DataProvider.CreateParameter("@RequiredDate", 50, order.RequiredDate, DbType.DateTime));
                    parameters.Add(DataProvider.CreateParameter("@ShippedDate", 50, order.ShippedDate, DbType.DateTime));
                    parameters.Add(DataProvider.CreateParameter("@Freight", 30, order.Freight, DbType.Decimal));
                    DataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The orderID is already exist");
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
        //-------------------------------------------------
        public void Update(OrderObject order)
        {
            try
            {
                OrderObject isEmpty = GetOrderByID(order.OrderID);
                if (isEmpty != null)
                {
                    string SQLInsert = "Update [Order] set OrderId = @OrderId, MemberId = @MemberId, OrderDate = @OrderDate, RequiredDate = @RequiredDate, ShippedDate = @ShippedDate, Freight = @Freight" +
                        " where OrderId = @OrderId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@OrderId", 4, order.OrderID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@MemberId", 4, order.MemberID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@OrderDate", 50, order.OrderDate, DbType.DateTime));
                    parameters.Add(DataProvider.CreateParameter("@RequiredDate", 50, order.RequiredDate, DbType.DateTime));
                    parameters.Add(DataProvider.CreateParameter("@ShippedDate", 50, order.ShippedDate, DbType.DateTime));
                    parameters.Add(DataProvider.CreateParameter("@Freight", 30, order.Freight, DbType.Decimal));
                    DataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The orderID is already exist");
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
        //-------------------------------------------------
        public void Remove(int orderID)
        {
            try
            {
                OrderObject isEmpty = GetOrderByID(orderID);
                if (isEmpty != null)
                {
                    string SQLDelete = "Delete [Order] where OrderId = @OrderId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@OrderId", 4, orderID, DbType.Int32));
                    DataProvider.Update(SQLDelete, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The orderID is not exist");
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
        //---------------------------END--------------------
    }
}
