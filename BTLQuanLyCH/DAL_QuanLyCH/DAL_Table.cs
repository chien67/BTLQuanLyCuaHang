using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyCH;
namespace DAL_QuanLyCH
{
    public class DAL_Table
    {
        private static DAL_Table instance;

        public static DAL_Table Instance
        {
            get { if (instance == null) instance = new DAL_Table(); return DAL_Table.instance; }
            private set { DAL_Table.instance = value; }
        }

        public static double TableWidth = 50;
        public static double TableHeight = 50;
        private DAL_Table() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");

            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);   
            }
            return tableList;
        }
    }
}
