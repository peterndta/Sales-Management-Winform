using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmProducts : Form
    {
        public IProductRepository ProductRepository = new ProductRepository();
        BindingSource source;
        private IEnumerable<ProductObject> ProductList;
        public frmProducts()
        {
            InitializeComponent();
        }
        private void ClearText()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            txtCategoryID.Text = "";
            txtWeight.Text = "";
            txtUnitPrice.Text = "";
            txtUnitInStock.Text = "";
        }
        //---------------------------------------------------------
        public void LoadProductList()
        {
            List<ProductObject> listProduct = null;

            listProduct = ProductRepository.GetProducts().ToList<ProductObject>();

            try
            {
                source = new BindingSource();
                source.DataSource = listProduct;

                // Clear binding data
                txtProductID.DataBindings.Clear();
                txtProductName.DataBindings.Clear();
                txtCategoryID.DataBindings.Clear();
                txtWeight.DataBindings.Clear();
                txtUnitPrice.DataBindings.Clear();
                txtUnitInStock.DataBindings.Clear();

                txtProductID.DataBindings.Add("Text", source, "ProductID");
                txtProductName.DataBindings.Add("Text", source, "ProductName");
                txtCategoryID.DataBindings.Add("Text", source, "CategoryID");
                txtWeight.DataBindings.Add("Text", source, "Weight");
                txtUnitPrice.DataBindings.Add("Text", source, "UnitPrice");
                txtUnitInStock.DataBindings.Add("Text", source, "UnitsInStock");

                dgvProductList.DataSource = null;  // Clear grid data
                dgvProductList.DataSource = source;


                if (listProduct.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                { 
                    btnDelete.Enabled = true;                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load product list");
            }
        }
        //---------------------------------------------------------
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadProductList();
        }
        //---------------------------------------------------------
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmProductDetail frmProductDetail = new frmProductDetail
            {
                Text = "Add new product",
                InsertOrUpdate = false,
                ProductRepository = ProductRepository,
            };
            if (frmProductDetail.ShowDialog() == DialogResult.OK)
            {
                LoadProductList();
            }

        }
        //---------------------------------------------------------
        private ProductObject GetProductObject()
        {
            ProductObject product = null;
            try
            {
                product = new ProductObject
                {
                    ProductID = int.Parse(txtProductID.Text),
                    ProductName = txtProductName.Text,
                    CategoryID = int.Parse(txtCategoryID.Text),
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitsInStock = int.Parse(txtUnitInStock.Text),
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Product information");
            }
            return product;
        }
        //---------------------------------------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult alert = MessageBox.Show("Are you sure?", "Delete product", MessageBoxButtons.OKCancel);
                if(alert == DialogResult.OK)
                {
                    ProductObject product = GetProductObject();
                    ProductRepository.DeleteProduct(product.ProductID);
                    LoadProductList();
                }             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Product");
            }
        }
        //---------------------------------------------------------
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        //---------------------------------------------------------

        private void frmProducts_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
        }
        //---------------------------------------------------------
        private void dgvProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmProductDetail frmProductDetails = new frmProductDetail
            {
                Text = "Update Product",
                InsertOrUpdate = true,
                ProductInfo = GetProductObject(),
                ProductRepository = ProductRepository,
            };
            if (frmProductDetails.ShowDialog() == DialogResult.OK)
            {
                LoadProductList();
                //Set focus Product updated
                source.Position = source.Count - 1;
            }
        }
        //---------------------------------------------------------
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (comboType.Text == "ID")
                {
                    int productID = int.Parse(txtSearch.Text);
                    ProductList = ProductRepository.GetProductByID(productID);
                }
                else if(comboType.Text == "Name")
                {
                    string productName = txtSearch.Text;
                    ProductList = ProductRepository.GetProducts().Where(s=>s.ProductName.Contains(productName));
                }
                else if (comboType.Text == "UnitPrice")
                {
                    string unitPrice = txtSearch.Text;
                    ProductList = ProductRepository.GetProductByUnitPrice(int.Parse(unitPrice));
                }
                else if (comboType.Text == "UnitInStock")
                {
                    string unitInStock = txtSearch.Text;
                    ProductList = ProductRepository.GetProductByUnitInStock(int.Parse(unitInStock));
                }

                if (ProductList != null)
                {
                    source = new BindingSource();
                    source.DataSource = ProductList;

                    txtProductID.DataBindings.Clear();
                    txtProductName.DataBindings.Clear();
                    txtCategoryID.DataBindings.Clear();
                    txtWeight.DataBindings.Clear();
                    txtUnitPrice.DataBindings.Clear();
                    txtUnitInStock.DataBindings.Clear();

                    txtProductID.DataBindings.Add("Text", source, "ProductID");
                    txtProductName.DataBindings.Add("Text", source, "ProductName");
                    txtCategoryID.DataBindings.Add("Text", source, "CategoryID");
                    txtWeight.DataBindings.Add("Text", source, "Weight");
                    txtUnitPrice.DataBindings.Add("Text", source, "UnitPrice");
                    txtUnitInStock.DataBindings.Add("Text", source, "UnitsInStock");

                    dgvProductList.DataSource = null;
                    dgvProductList.DataSource = source;

                    btnDelete.Enabled = true;

                }
                else
                {
                    ClearText();
                    btnDelete.Enabled = false;
                    MessageBox.Show("This product does not exist!", "Search product");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This product does not exist!", "Search product");
            }
        }
        //---------------------------------------------------------
    }
}
