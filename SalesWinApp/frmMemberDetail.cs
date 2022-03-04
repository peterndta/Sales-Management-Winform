using BusinessObject;
using DataAccess.Repository;
using System;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmMemberDetail : Form
    {
        public frmMemberDetail()
        {
            InitializeComponent();
        }
        public IMemberRepository MemberRepository { get; set; }
        public bool InsertOrUpdate { get; set; } //False : Insert, True : Update
        public MemberObject MemberInfo { get; set; }
        private void frmMemberDetail_Load(object sender, EventArgs e)
        {
            txtMemberID.Enabled = !InsertOrUpdate;
            if (InsertOrUpdate == true)
            {
                txtMemberID.Text = MemberInfo.MemberID.ToString();
                txtCompanyName.Text = MemberInfo.CompanyName.ToString();
                txtEmail.Text = MemberInfo.Email.ToString();
                txtPassword.Text = MemberInfo.Password.ToString();
                txtCity.Text = MemberInfo.City.ToString();
                txtCountry.Text = MemberInfo.Country.ToString();              
            }
        }
        //---------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var member = new MemberObject
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    CompanyName = txtCompanyName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,

                };
                if (InsertOrUpdate == false)
                {
                    MemberRepository.InsertMember(member);
                    MessageBox.Show("Add Success!");
                }
                else
                {
                    MemberRepository.UpdateMember(member);
                    MessageBox.Show("Update Success!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new member" : "Update a member");
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
