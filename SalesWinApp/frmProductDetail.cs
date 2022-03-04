using BusinessObject;
using DataAccess.Repository;
using System;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmProductDetail : Form
    {
        public frmProductDetail()
        {
            InitializeComponent();
        }
        public bool InsertOrUpdate { get; set; }
        public ProductObject ProductInfo { get; set; }
        public IProductRepository ProductRepository { get; set; }
        private void frmProductDetail_Load(object sender, EventArgs e)
        {
            txtProductID.Enabled = !InsertOrUpdate;
            if (InsertOrUpdate == true)
            {
                txtProductID.Text = ProductInfo.ProductID.ToString();
                txtProductName.Text = ProductInfo.ProductName.ToString();
                txtCategoryID.Text = ProductInfo.CategoryID.ToString();
                txtWeight.Text = ProductInfo.Weight.ToString();
                txtUnitPrice.Text = ProductInfo.UnitPrice.ToString();
                txtUnitInStock.Text = ProductInfo.UnitsInStock.ToString();
            }
        }
        //---------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var Product = new ProductObject
                {
                    ProductID = int.Parse(txtProductID.Text),
                    ProductName = txtProductName.Text,
                    CategoryID = int.Parse(txtCategoryID.Text),
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitsInStock = int.Parse(txtUnitInStock.Text),

                };
                if (InsertOrUpdate == false)
                {
                    ProductRepository.InsertProduct(Product);
                    MessageBox.Show("Add Success!");
                }
                else
                {
                    ProductRepository.UpdateProduct(Product);
                    MessageBox.Show("Update Success!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new Product" : "Update a Product");
            }
        }
        //---------------------------------------------------------
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //---------------------------------------------------------
    }
}
