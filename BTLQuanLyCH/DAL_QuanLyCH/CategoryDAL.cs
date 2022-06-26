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
        public Category GetCategoryByID(int id)
        {
            Category category = null;
            string query = "select * from FoodCategory where id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }
            return category;
        }
        public bool InsertCategory(string name)
        {
            string query = string.Format("insert dbo.FoodCategory ( name )Values ( N'{0}')", name);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteCategory(string name)
        {
            string query = string.Format("delete FoodCategory where name = N'{0}'", name);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
