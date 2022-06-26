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

        public static int TableWidth = 100;
        public static int TableHeight = 100;
        private DAL_Table() { }
        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("USP_SwitchTable @idTable1 , @idTable2", new object[] { id1, id2 });
        }

        public List<Table> GetListTable()
        {
            List<Table> list = new List<Table>();
            string query = "SELECT * FROM TableFood";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                    list.Add(table);
            }
            return list;
        }

        //public List<Table> GetDataTable()
        //{
        //    Dictionary<string, int> dict = new Dictionary<string, int>();

        //    List<Table> list = new List<Table>();
        //    string query = "SELECT * FROM TableFood";

        //    DataTable data = DataProvider.Instance.ExecuteQuery(query);

        //    foreach (DataRow item in data.Rows)
        //    {
        //        Table table = new Table(item);

        //        //Lấy những item mà trường table.Status không bị lặp lại
        //        if (!dict.TryGetValue(table.Status, out int id))
        //        {
        //            list.Add(table);
        //            dict.Add(table.Status, table.ID);
        //        }
        //    }
        //    return list;
        //}

        public bool InsertTable(string name)
        {
            string query = string.Format("insert dbo.TableFood ( name) Values ( N'{0}')", name);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteTable(int idTable)
        {
            string query = string.Format("delete TableFood where id = {0}", idTable);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            string query = String.Format("select * from dbo.TableFood ");
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }
    }
}
