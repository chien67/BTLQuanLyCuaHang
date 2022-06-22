using DTO_QuanLyCH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLyCH
{
    public class AccountDAL
    {
        private  static AccountDAL instance;

        public static AccountDAL Instance 
        { 
            get { if (instance == null) instance = new AccountDAL(); return instance; }
            private set { instance = value; }
        }
        private AccountDAL() { }

        public bool Login(string userName, string passWord)
        {
            string query = "USP_login @username , @password";

            DataTable result = DataProvider.Instance.ExecuteQuery(query,new object[] { userName, passWord });
            return result.Rows.Count >0;
        }

        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName @displayName @password @newPassword", new object[] {userName,displayName,pass,newPass });
            return result >0;
        }
        public Account GetAccountByUserName (string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("Select * from account where userName = '" + userName + "'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }
    }
}
