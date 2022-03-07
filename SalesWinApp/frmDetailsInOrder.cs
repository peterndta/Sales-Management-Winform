using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmDetailsInOrder : Form
    {
        public frmDetailsInOrder()
        {
            InitializeComponent();
        }
        public IOrderDetailRepository OrderDetailRepository = new OrderDetailRepository();
        BindingSource source;
        public OrderObject OrderInfo { get; set; }
        private void frmDetailsInOrder_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            txtOrderID.Enabled = false;
            txtProductID.Enabled = false;
        }
        //---------------------------------------------------------
        private void ClearText()
        {
            txtOrderID.Text = "";
            txtProductID.Text = "";
            txtUnitPrice.Text = "";
            txtQuantity.Text = "";
            txtDiscount.Text = "";
        }
        //---------------------------------------------------------
        public void LoadOrderDetailList()
        {     
            List<OrderDetailObject> listProduct = null;

            listProduct = OrderDetailRepository.GetDetailListByOrderID(OrderInfo.OrderID).ToList<OrderDetailObject>();
            try
                {

                    source = new BindingSource();
                    source.DataSource = listProduct;

                    // Clear binding data
                    txtOrderID.DataBindings.Clear();
                    txtProductID.DataBindings.Clear();
                    txtUnitPrice.DataBindings.Clear();
                    txtQuantity.DataBindings.Clear();
                    txtDiscount.DataBindings.Clear();

                    txtOrderID.DataBindings.Add("Text", source, "OrderID");
                    txtProductID.DataBindings.Add("Text", source, "ProductID");
                    txtUnitPrice.DataBindings.Add("Text", source, "UnitPrice");
                    txtQuantity.DataBindings.Add("Text", source, "Quantity");
                    txtDiscount.DataBindings.Add("Text", source, "Discount");

                    dgvDetailList.DataSource = null;  // Clear grid data
                    dgvDetailList.DataSource = source;


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
                    MessageBox.Show(ex.Message, "Load detail list");
                }
            
            
        }
        //---------------------------------------------------------
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadOrderDetailList();
        }
        //---------------------------------------------------------
        private OrderDetailObject GetDetailObject()
        {
            OrderDetailObject detail = null;
            try
            {
                detail = new OrderDetailObject
                {
                    OrderID = int.Parse(txtOrderID.Text),
                    ProductID = int.Parse(txtProductID.Text),
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    Quantity = int.Parse(txtQuantity.Text),
                    Discount = float.Parse(txtDiscount.Text),
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get detail information");
            }
            return detail;
        }
        //---------------------------------------------------------
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmDetailsInOrderInfo frmOrderDetailsInfo = new frmDetailsInOrderInfo()
            {
                Text = "Add new detail",
                InsertOrUpdate = false,
                OrderDetailRepository = OrderDetailRepository,
                OrderInfo = OrderInfo,
            };
            if (frmOrderDetailsInfo.ShowDialog() == DialogResult.OK)
            {
                LoadOrderDetailList();
            }
        }
        //---------------------------------------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult alert = MessageBox.Show("Are you sure?", "Delete detail", MessageBoxButtons.OKCancel);
                if (alert == DialogResult.OK)
                {
                    OrderDetailObject order = GetDetailObject();
                    OrderDetailRepository.DeleteOrderDetail(OrderInfo.OrderID, order.ProductID);
                    LoadOrderDetailList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete detail");
            }
        }
        //---------------------------------------------------------
        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }
        //---------------------------------------------------------
    }
}
