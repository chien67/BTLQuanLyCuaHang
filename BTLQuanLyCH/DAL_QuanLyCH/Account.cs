using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLyCH
{
    public class Account
    {
        private  static Account instance;

        public static Account Instance 
        { 
            get { if (instance == null) instance = new Account(); return instance; }
            private set { instance = value; }
        }
        private Account() { }

        public bool Login(string userName, string passWord)
        {
            string query = "select *from dbo.Account where UserName = N'" + userName + "' AND PassWord = N'" + passWord + "' ";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count >0;
        }
    }
}
