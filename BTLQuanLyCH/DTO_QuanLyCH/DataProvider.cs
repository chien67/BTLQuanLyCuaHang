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
        string connectionString = @"Data Source =DESKTOP-HIB2CEE\QUYNH;Initial Catalog = BTLT3;User ID = sa; Password = 123456";

        public object ExcuteQuery(string query)
        {
            throw new NotImplementedException();
        }
    }
    public DataTable ExcuteQuery(string query)
    {
        DataTable data = new DataTable();
        string connectionString = null;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            connection.Close();
        }

        return data;
    }
}
