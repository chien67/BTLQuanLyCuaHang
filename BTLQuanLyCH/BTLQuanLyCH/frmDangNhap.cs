using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS_QuanLyCH;

namespace GUI_QuanLyCH
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        private bool isSuccess = false;
        public string getEmail
        {
            get
            {
 //               return txtEmail.Text;
            }
        }
        public bool getSuccess
        {
            get { return isSuccess; }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }
    }
}
