using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<ProductObject> GetProducts();
        ProductObject GetProductByID(int productID);

        ProductObject GetProductByName(string productName);

        ProductObject GetProductByUnitPrice(int unitPrice);

        ProductObject GetProductByUnitInStock(int unitInStock);

        void InsertProduct(ProductObject product);

        void UpdateProduct(ProductObject product);

        void DeleteProduct(int productID);
    }
}
