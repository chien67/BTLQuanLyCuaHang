﻿using DTO_QuanLyCH;
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
        void LoadAccountList()
        {
            string query = "select * from dbo.Account";
            DataProvider provider = new DataProvider();
            dtgvAccount.DataSource = provider.ExcuteQuery(query);
        }
    }
}
