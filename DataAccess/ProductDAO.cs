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
    public class ProductDAO : BaseDAL
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();

        private ProductDAO() { }

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        //---------------------------------------------------------
        public List<ProductObject> GetProductList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select ProductId, CategoryId, ProductName, Weight, UnitPrice, UnitslnStock from Product";
            var products = new List<ProductObject>();
            try
            {
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    products.Add(new ProductObject
                    {
                        ProductID = dataReader.GetInt32(0),
                        CategoryID = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitsInStock = dataReader.GetInt32(5),
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
            return products;
        }
        //---------------------------------------------------------
        public ProductObject GetProductByID(int productID)
        {
            ProductObject product = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select ProductId, CategoryId, ProductName, Weight, UnitPrice, UnitslnStock from Product where ProductId = @ProductId";
            try
            {
                var param = DataProvider.CreateParameter("@ProductId", 4, productID, DbType.Int32);
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    product = new ProductObject
                    {
                        ProductID = dataReader.GetInt32(0),
                        CategoryID = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitsInStock = dataReader.GetInt32(5),
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
            return product;
        }
        public ProductObject GetProductByName(string productName)
        {
            ProductObject product = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select ProductId, CategoryId, ProductName, Weight, UnitPrice, UnitslnStock from Product where ProductName = @ProductName";
            try
            {
                var param = DataProvider.CreateParameter("@ProductName", 50, productName, DbType.String);
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    product = new ProductObject
                    {
                        ProductID = dataReader.GetInt32(0),
                        CategoryID = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitsInStock = dataReader.GetInt32(5),
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
            return product;
        }
        public ProductObject GetProductByUnitPrice(int unitPrice)
        {
            ProductObject product = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select ProductId, CategoryId, ProductName, Weight, UnitPrice, UnitslnStock from Product where UnitPrice = @UnitPrice";
            try
            {
                var param = DataProvider.CreateParameter("@UnitPrice", 50, unitPrice, DbType.Decimal);
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    product = new ProductObject
                    {
                        ProductID = dataReader.GetInt32(0),
                        CategoryID = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitsInStock = dataReader.GetInt32(5),
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
            return product;
        }
        public ProductObject GetProductByUnitInStock(int unitInStock)
        {
            ProductObject product = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select ProductId, CategoryId, ProductName, Weight, UnitPrice, UnitslnStock from Product where UnitslnStock = @UnitslnStock";
            try
            {
                var param = DataProvider.CreateParameter("@UnitslnStock", 4, unitInStock, DbType.Int32);
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    product = new ProductObject
                    {
                        ProductID = dataReader.GetInt32(0),
                        CategoryID = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitsInStock = dataReader.GetInt32(5),
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
            return product;
        }
        //---------------------------------------------------------
        public void AddNew(ProductObject product)
        {
            try
            {
                ProductObject isEmpty = GetProductByID(product.ProductID);
                if (isEmpty == null)
                {
                    string SQLInsert = "Insert Product values(@ProductId, @CategoryId, @ProductName, @Weight, @UnitPrice, @UnitslnStock)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@ProductId", 4, product.ProductID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@CategoryId", 4, product.CategoryID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@ProductName", 50, product.ProductName, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@Weight", 50, product.Weight, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@UnitPrice", 50, product.UnitPrice, DbType.Decimal));
                    parameters.Add(DataProvider.CreateParameter("@UnitslnStock", 4, product.UnitsInStock, DbType.Int32));
                    DataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The product is already exist");
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
        //---------------------------------------------------------
        public void Update(ProductObject product)
        {
            try
            {
                ProductObject isEmpty = GetProductByID(product.ProductID);
                if (isEmpty != null)
                {
                    string SQLInsert = "Update Product set ProductId = @ProductId, CategoryId = @CategoryId, ProductName = @ProductName, Weight = @Weight, UnitPrice = @UnitPrice, UnitslnStock = @UnitslnStock" +
                        " where ProductId = @ProductId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@ProductId", 4, product.ProductID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@CategoryId", 4, product.CategoryID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@ProductName", 40, product.ProductName, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@Weight", 20, product.Weight, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@UnitPrice", 50, product.UnitPrice, DbType.Decimal));
                    parameters.Add(DataProvider.CreateParameter("@UnitslnStock", 4, product.UnitsInStock, DbType.Int32));
                    DataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("Error when updating");
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
        //---------------------------------------------------------
        public void Remove(int productID)
        {
            try
            {
                ProductObject isEmpty = GetProductByID(productID);
                if (isEmpty != null)
                {
                    string SQLDelete = "Delete Product where productID = @productID";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@productID", 4, productID, DbType.Int32));
                    DataProvider.Update(SQLDelete, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The product is not exist");
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
        //-----------------------------END--------------------------

    }
}
