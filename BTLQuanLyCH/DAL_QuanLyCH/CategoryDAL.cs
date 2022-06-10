using DTO_QuanLyCH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLyCH
{
    public class CategoryDAL
    {
        private static CategoryDAL instance;

        public static CategoryDAL Instance 
        {
            get { if (instance == null) instance = new CategoryDAL(); return CategoryDAL.instance; } 
            private set => instance = value; 
        }
        private CategoryDAL(){ }
        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();
            string query = "select * from FoodCategory";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }
            return list;
        }
    }
}
