using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        IEnumerable<ProductObject> GetProducts();
        IEnumerable<ProductObject> GetProductByID(int productID);

        IEnumerable<ProductObject> GetProductByUnitPrice(int unitPrice);

        IEnumerable<ProductObject> GetProductByUnitInStock(int unitInStock);

        void InsertProduct(ProductObject product);

        void UpdateProduct(ProductObject product);

        void DeleteProduct(int productID);
    }
}
