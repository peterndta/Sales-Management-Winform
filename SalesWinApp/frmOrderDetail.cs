using BusinessObject;
using DataAccess.Repository;
using System;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmOrderDetail : Form
    {
        public frmOrderDetail()
        {
            InitializeComponent();
        }
        public bool InsertOrUpdate { get; set; }
        public OrderObject OrderInfo { get; set; }
        
        public IOrderRepository OrderRepository { get; set; }
        //---------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var Order = new OrderObject
                {
                    OrderID = int.Parse(txtOrderID.Text),
                    MemberID = int.Parse(txtMemberID.Text),
                    OrderDate = dtpOrderDate.Value,
                    RequiredDate = dtpRequiredDate.Value,
                    ShippedDate = dtpShippedDate.Value,
                    Freight = Decimal.Parse(txtFreight.Text),

                };
                if (txtOrderID.Text.Trim() == string.Empty || txtMemberID.Text.Trim() == string.Empty ||
                    dtpOrderDate.Text.Trim() == string.Empty || txtFreight.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Your field is Empty", InsertOrUpdate == false ? "Add a new Order" : "Update a Order");
                    DialogResult = DialogResult.None;
                }
                else
                {
                    if (InsertOrUpdate == false)
                    {
                        OrderRepository.InsertOrder(Order);
                        MessageBox.Show("Add Success!");
                    }
                    else
                    {
                        OrderRepository.UpdateOrder(Order);
                        MessageBox.Show("Update Success!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new Order" : "Update a Order");
            }
        }
        //---------------------------------------------------------
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        //---------------------------------------------------------
        private void frmOrderDetail_Load(object sender, EventArgs e)
        {
            txtOrderID.Enabled = !InsertOrUpdate;
            if (InsertOrUpdate == true)
            {
                txtOrderID.Text = OrderInfo.OrderID.ToString();
                txtMemberID.Text = OrderInfo.MemberID.ToString();
                dtpOrderDate.Value = OrderInfo.OrderDate;
                dtpRequiredDate.Value = OrderInfo.RequiredDate;
                dtpShippedDate.Value = OrderInfo.ShippedDate;
                txtFreight.Text = OrderInfo.Freight.ToString();
            }
        }
        //---------------------------------------------------------
        private void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                frmDetailsInOrder frmDetailsInOrder = new frmDetailsInOrder
                {
                    OrderInfo = OrderInfo,
                };
                frmDetailsInOrder.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }


        }
        //---------------------------------------------------------
    }
}
