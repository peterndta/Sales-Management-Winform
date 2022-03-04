using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmOrders : Form
    {
        public frmOrders()
        {
            InitializeComponent();
        }
        public IOrderRepository OrderRepository = new OrderRepository();
        BindingSource source;
        public MemberObject MemberInfo { get; set; }
        public bool MemberOrAdmin { get; set; } //False : Member, True : Admin
        private void frmOrders_Load(object sender, EventArgs e)
        {
            btnNew.Enabled = MemberOrAdmin;
            btnDelete.Enabled = false;
        }
        //---------------------------------------------------------
        private void ClearText()
        {
            txtMemberID.Text = "";
            txtFreight.Text = "";
            txtOrderID.Text = "";
            dtpOrderDate.Value = DateTime.Now;
            dtpRequiredDate.Value = DateTime.Now;
            dtpShippedDate.Value = DateTime.Now;
        }
        public void LoadOrderList()
        {
            List<OrderObject> listOrder= null;
            if (MemberOrAdmin == true)
            {
                listOrder = OrderRepository.GetOrders().ToList<OrderObject>();
            }
            else if (MemberOrAdmin == false)
            {
                txtMemberID.Enabled = false;
                txtFreight.Enabled = false;
                txtOrderID.Enabled = false;
                dtpOrderDate.Enabled = false;
                dtpRequiredDate.Enabled = false;
                dtpShippedDate.Enabled = false;
                listOrder = OrderRepository.GetOrderByMemberID(MemberInfo.MemberID).ToList<OrderObject>();
            }

            try
            {
                source = new BindingSource();
                source.DataSource = listOrder;

                // Clear binding data
                txtOrderID.DataBindings.Clear();
                txtMemberID.DataBindings.Clear();
                txtFreight.DataBindings.Clear();
                dtpOrderDate.DataBindings.Clear();
                dtpRequiredDate.DataBindings.Clear();
                dtpShippedDate.DataBindings.Clear();

                txtOrderID.DataBindings.Add("Text", source, "OrderID");
                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtFreight.DataBindings.Add("Text", source, "Freight");
                dtpOrderDate.DataBindings.Add("Text", source, "OrderDate");
                dtpRequiredDate.DataBindings.Add("Text", source, "RequiredDate");
                dtpShippedDate.DataBindings.Add("Text", source, "ShippedDate");

                dgvOrderList.DataSource = null;  // Clear grid data
                dgvOrderList.DataSource = source;


                if (listOrder.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    if (MemberOrAdmin == false)
                    {
                        btnDelete.Enabled = false;
                    }
                    else
                    {
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load order list");
            }
        }
        //---------------------------------------------------------

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadOrderList();
        }
        //---------------------------------------------------------

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmOrderDetail frmOrderDetail = new frmOrderDetail()
            {
                Text = "Add new order",
                InsertOrUpdate = false,
                OrderRepository = OrderRepository,
            };
            if (frmOrderDetail.ShowDialog() == DialogResult.OK)
            {
                LoadOrderList();
            }
        }
        //---------------------------------------------------------

        private void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                OrderObject order = GetOrderObject();
                OrderRepository.DeleteOrder(order.OrderID);
                LoadOrderList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete order");
            }

        }
        //---------------------------------------------------------

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        //---------------------------------------------------------

        private OrderObject GetOrderObject()
        {
            OrderObject order = null;
            try
            {
                order = new OrderObject
                {
                    OrderID = int.Parse(txtOrderID.Text),
                    MemberID = int.Parse(txtMemberID.Text),
                    OrderDate = dtpOrderDate.Value,
                    RequiredDate = dtpRequiredDate.Value,
                    ShippedDate = dtpShippedDate.Value,
                    Freight = Decimal.Parse(txtFreight.Text),
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get order information");
            }
            return order;
        }
        //---------------------------------------------------------

        private void dgvOrderList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MemberOrAdmin == true)
            {
                frmOrderDetail frmMemberDetails = new frmOrderDetail
                {
                    Text = "Update order",
                    InsertOrUpdate = true,
                    OrderInfo = GetOrderObject(),
                    OrderRepository = OrderRepository,
                };
                if (frmMemberDetails.ShowDialog() == DialogResult.OK)
                {
                    LoadOrderList();
                    source.Position = source.Count - 1;
                }
            }
            else if (MemberOrAdmin == false)
            {

                MessageBox.Show("You do not have permission to update!", "Alert");
            }
            
        }

        //---------------------------------------------------------
    }
}
