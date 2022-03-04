using BusinessObject;
using DataAccess.Repository;
using System;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmDetailsInOrderInfo : Form
    {
        public frmDetailsInOrderInfo()
        {
            InitializeComponent();
        }
        public bool InsertOrUpdate { get; set; }
        public OrderDetailObject OrderDetailInfo { get; set; }
        public IOrderDetailRepository OrderDetailRepository { get; set; }
        public OrderObject OrderInfo { get; set; }
        //---------------------------------------------------------
        private void frmDetailsInOrderInfo_Load(object sender, EventArgs e)
        {
            txtOrderID.Enabled = false;
            txtOrderID.Text = OrderInfo.OrderID.ToString();
        }
        //---------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var Detail = new OrderDetailObject
                {
                    OrderID = int.Parse(txtOrderID.Text),
                    ProductID = int.Parse(txtProductID.Text),
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),                
                    Quantity = int.Parse(txtQuantity.Text),
                    Discount = float.Parse(txtDiscount.Text),


                };
                if (InsertOrUpdate == false)
                {
                    OrderDetailRepository.InsertOrderDetail(Detail);
                    MessageBox.Show("Add Success!");
                }
                else
                {
                    OrderDetailRepository.UpdateOrderDetail(Detail);
                    MessageBox.Show("Update Success!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new Detail" : "Update a Detail");
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
