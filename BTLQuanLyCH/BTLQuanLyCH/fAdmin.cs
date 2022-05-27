using DTO_QuanLyCH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QuanLyCH
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            LoadAccountList();
        }
        void LoadFoodList()
        {
            string query = "select * from dbo.Food";

            dtgvAccount.DataSource = DataProvider.Instance.ExcuteQuery(query);
        }
        void LoadAccountList()
        {
            string query = "exec dbo.getInfo @userName";

            dtgvAccount.DataSource = DataProvider.Instance.ExcuteQuery(query, new object[] {"Chien01"});
        }
    }
}
