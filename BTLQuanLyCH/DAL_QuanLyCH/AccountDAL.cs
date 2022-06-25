﻿using DTO_QuanLyCH;
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
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @password , @newPassword", new object[] {userName, displayName, pass, newPass });
            return result >0;
        }
        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("select Username , DisplayName , Type from dbo.Account");
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
        public bool InsertAccount(string name, string displayName, int type)
        {
            string query = string.Format("insert dbo.Account ( UserName, DisplayName, Type )Values ( N'{0}', N'{1}', {2})", name, displayName, type);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateAccount(string name, string displayName, int type)
        {
            string query = string.Format("update dbo.Account set DisplayName = N'{1}', Type = {2} where Username = N'{0}'", name, displayName, type);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteAccount(string name)
        {
 
            string query = string.Format("delete Account where UserName = N'{0}'", name);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool ResetPassword(string name)
        {
            string query = string.Format("update account set password = N'0' where UserName = N'{0}'", name);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
