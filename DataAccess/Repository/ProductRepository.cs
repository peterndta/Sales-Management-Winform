using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        void IProductRepository.DeleteProduct(int productID) => ProductDAO.Instance.Remove(productID);

        ProductObject IProductRepository.GetProductByID(int productID) => ProductDAO.Instance.GetProductByID(productID);

        ProductObject IProductRepository.GetProductByName(string productName) => ProductDAO.Instance.GetProductByName(productName);

        ProductObject IProductRepository.GetProductByUnitPrice(int unitPrice) => ProductDAO.Instance.GetProductByUnitPrice(unitPrice);

        ProductObject IProductRepository.GetProductByUnitInStock(int unitInStock) => ProductDAO.Instance.GetProductByUnitInStock(unitInStock);

        IEnumerable<ProductObject> IProductRepository.GetProducts() => ProductDAO.Instance.GetProductList();

        void IProductRepository.InsertProduct(ProductObject product) => ProductDAO.Instance.AddNew(product);

        void IProductRepository.UpdateProduct(ProductObject product) => ProductDAO.Instance.Update(product);
    }
}
