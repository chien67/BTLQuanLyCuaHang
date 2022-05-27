using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLyCH
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get  {if (instance == null) instance = new DataProvider();return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        string connectionString = @"Data Source =DESKTOP-E3B836B\SQLEXPRESS;Initial Catalog = BTLT3;User ID = sa; Password = 123456";

        private DataProvider() { }
        public DataTable ExcuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if(parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            string para = item.Replace(",", "");
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }
            return data;
        }
    }
}
