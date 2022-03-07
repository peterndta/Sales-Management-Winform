using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmMember : Form
    {
        public frmMember()
        {
            InitializeComponent();
        }
        public IMemberRepository MemberRepository = new MemberRepository();
        BindingSource source;
        public MemberObject MemberInfo { get; set; }
        public bool MemberOrAdmin { get; set; } //False : Member, True : Admin
        private void ClearText()
        {
            txtMemberID.Text = "";
            txtCompanyName.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtCity.Text = "";
            txtCountry.Text = "";
        }
        //---------------------------------------------------------
        private void frmMember_Load(object sender, EventArgs e)
        {
            btnNew.Enabled = MemberOrAdmin;       
            btnDelete.Enabled = false;

        }
        //---------------------------------------------------------

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        //---------------------------------------------------------
        public void LoadMemberList()
        {
            List<MemberObject> listMember = null;
            if (MemberOrAdmin == true)
            {
                listMember = MemberRepository.GetMembers().ToList<MemberObject>();
            }
            else if (MemberOrAdmin == false)
            {

                listMember = new List<MemberObject>() { MemberInfo };
            }
            try
            {
                source = new BindingSource();
                source.DataSource = listMember;

                // Clear binding data
                txtMemberID.DataBindings.Clear();
                txtCompanyName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtCompanyName.DataBindings.Add("Text", source, "CompanyName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");

                dgvMemberList.DataSource = null;  // Clear grid data
                dgvMemberList.DataSource = source;


                if (listMember.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    if (MemberOrAdmin == false)
                    {
                        btnDelete.Enabled = false;
                    }else
                    {
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }
        //---------------------------------------------------------
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadMemberList();
        }
        //---------------------------------------------------------
        private void btnNew_Click(object sender, EventArgs e)
        {

            frmMemberDetail frmMemberDetail = new frmMemberDetail
            {
                Text = "Add new member",
                InsertOrUpdate = false,
                MemberRepository = MemberRepository,
            };
            if (frmMemberDetail.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
            }
            
        }
        //---------------------------------------------------------
        private MemberObject GetMemberObject()
        {
            MemberObject member = null;
            try
            {
                member = new MemberObject
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    CompanyName = txtCompanyName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member information");
            }
            return member;
        }
        //---------------------------------------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult alert = MessageBox.Show("Are you sure?", "Delete member", MessageBoxButtons.OKCancel);
                if (alert == DialogResult.OK)
                {
                    MemberObject member = GetMemberObject();
                    MemberRepository.DeleteMember(member.MemberID);
                    LoadMemberList();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete member");
            }
        }
        //---------------------------------------------------------
        private void dgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMemberDetail frmMemberDetails = new frmMemberDetail
            {
                Text = "Update member",
                InsertOrUpdate = true,
                MemberInfo = GetMemberObject(),
                MemberRepository = MemberRepository,
            };
            if (frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                //Set focus member updated
                source.Position = source.Count - 1;
            }
        }

        private void dgvMemberList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == 3 && e.Value != null)
            {
                e.Value = new string('*', e.Value.ToString().Length);
                   
            }
            
        }

        //---------------------------------------------------------
    }
}
