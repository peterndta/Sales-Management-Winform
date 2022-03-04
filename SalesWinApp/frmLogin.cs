using BusinessObject;
using DataAccess.Repository;
using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace SalesWinApp
{
    public partial class frmLogin : Form
    {
        IMemberRepository memeberRepository = new MemberRepository();
        public MemberObject AdminAccountInfo { get; set; }
        public frmLogin()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmMain frmMain;
            var Email = txtEmail.Text;
            var Password = txtPassword.Text;

            //Get Admin account from json
            var adminDefaultSettings = Program.Configuration.GetSection("AdminAccount").Get<MemberObject>();
            var email = adminDefaultSettings.Email; //admin email
            var password = adminDefaultSettings.Password; //admin password
            MemberObject loginInfo = memeberRepository.Login(Email, Password);

            if (loginInfo != null) //check if member
            {
                frmMain = new frmMain
                {
                    MemberInfo = loginInfo,
                    MemberOrAdmin = false
                };
                this.Hide();
                frmMain.ShowDialog();
                this.Show();
            }
            else if (Email == email && Password == password) //check if admin
            {
                frmMain = new frmMain
                {
                    MemberOrAdmin = true
                };
                this.Hide();
                frmMain.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Your account does not exist!");
            }

        }//end btnLogin_Click
         //-----------------------------------------------------

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }//end btnClose_Click

        //------------------------END---------------------------
    }
}
