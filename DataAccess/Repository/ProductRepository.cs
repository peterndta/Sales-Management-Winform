using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        void IProductRepository.DeleteProduct(int productID) => ProductDAO.Instance.Remove(productID);

        IEnumerable<ProductObject> IProductRepository.GetProductByID(int productID) => ProductDAO.Instance.GetProductsByID(productID);

        IEnumerable<ProductObject> IProductRepository.GetProductByUnitPrice(int unitPrice) => ProductDAO.Instance.GetProductByUnitPrice(unitPrice);

        IEnumerable<ProductObject> IProductRepository.GetProductByUnitInStock(int unitInStock) => ProductDAO.Instance.GetProductByUnitInStock(unitInStock);

        IEnumerable<ProductObject> IProductRepository.GetProducts() => ProductDAO.Instance.GetProductList();

        void IProductRepository.InsertProduct(ProductObject product) => ProductDAO.Instance.AddNew(product);

        void IProductRepository.UpdateProduct(ProductObject product) => ProductDAO.Instance.Update(product);
    }
}
