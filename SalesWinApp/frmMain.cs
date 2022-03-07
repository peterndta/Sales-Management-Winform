using BusinessObject;
using System;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }      
        public bool MemberOrAdmin{ get; set; } //False : Member, True : Admin
        public MemberObject MemberInfo { get; set; }
        private void frmMain_Load(object sender, EventArgs e)
        {
            btnProducts.Enabled = MemberOrAdmin;
        }
        //---------------------------------------------------------
        private void btnMembers_Click(object sender, EventArgs e)
        {
            try
            {
                frmMember frmMember = new frmMember
                {
                    MemberInfo = MemberInfo,
                    MemberOrAdmin = MemberOrAdmin
                };
                frmMember.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Member is empty","Error");
            }
            

        }
        //---------------------------------------------------------
        private void btnProducts_Click(object sender, EventArgs e)
        {
            try
            {
                frmProducts frmProduct = new frmProducts { };
                frmProduct.ShowDialog();
            }catch(Exception ex)
            {
                MessageBox.Show("Product is empty", "Error");
            }
                
        }
        //---------------------------------------------------------
        private void btnOrders_Click(object sender, EventArgs e)
        {
            try
            {
                frmOrders frmOrders = new frmOrders
                {
                    MemberInfo = MemberInfo,
                    MemberOrAdmin = MemberOrAdmin
                };
                frmOrders.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Order is empty", "Error");
            }

        }
        //---------------------------------------------------------

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        //--------------------------END----------------------------
    }
}
