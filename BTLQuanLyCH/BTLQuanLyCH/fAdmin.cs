﻿using DAL_QuanLyCH;
using DTO_QuanLyCH;
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

        BindingSource foodList = new BindingSource();

        BindingSource accountList = new BindingSource();

        BindingSource tableList = new BindingSource();

        BindingSource categoryList = new BindingSource();

        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            Load();
        }
        #region methods

        void Load()
        {
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;
            dtgvTable.DataSource = tableList;
            dtgvCategory.DataSource = categoryList;
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadAccount(); 
            LoadListCategory();
            LoadListTable();
            LoadCategoryIntoCombobox(cbfFoodCategory);

            AddFoodBinding();
            AddAccountBinding();
            AddCategoryBinding();
            AddTableBinding();

        }
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAL.Instance.SearchFoodByName(name);

            return listFood;
        }
        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Username",true,DataSourceUpdateMode.Never));//txbox ko chuyen du lieu nguoc ve
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            numericUpDown1.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDAL.Instance.GetListAccount();
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate (DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAL.Instance.GetBillListByDate(checkIn, checkOut);
        }
        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void AddCategoryBinding()
        {
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cbfFoodCategory.DataSource = CategoryDAL.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAL.Instance.GetListFood();
        }
        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAL.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }
                LoadAccount();
        }
        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAL.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }
            LoadAccount();
        }
        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Không thể tự xoá chính tài khoản");
            }    
            if (AccountDAL.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }
            LoadAccount();
        }
        void ResetPass(string userName)
        {
            if (AccountDAL.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại tài khoản thất bại");
            }
        }
        void LoadListTable()
        {
            tableList.DataSource = DAL_Table.Instance.GetListTable();
            this.dtgvTable.Columns["ID"].Visible = false;
        }

        void AddTableBinding()
        {
            txbTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            cbTableStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never));
        }
        void LoadListCategory()
        {
            categoryList.DataSource = CategoryDAL.Instance.GetListCategory();
        }
        #endregion

        #region events
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;
            AddAccount(userName,displayName,type);
        }
        private void btnDeleteccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            DeleteAccount(userName);
        }
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            EditAccount(userName, displayName, type);
        }
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            ResetPass(userName);
            LoadAccount();
        }
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }
        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbfFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            if (FoodDAL.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món ăn thành công");
                LoadListFood();
                if(insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn!");
            }
        }
        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbfFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAL.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món ăn thành công");
                LoadListFood();
                if(updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn!");
            }
        }
        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAL.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xoá món ăn thành công");
                LoadListFood();
                if(deleteFood !=null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xoá thức ăn!");
            }
        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;

                if (DAL_Table.Instance.InsertTable(name))
                {
                    MessageBox.Show("Thêm bàn ăn thành công");
                    LoadListTable();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm bàn!");
                }
        }
        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableID.Text);

            if (DAL_Table.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xoá bàn thành công");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xoá bàn!");
            }
        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            if (CategoryDAL.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công");
                LoadListCategory();
                LoadCategoryIntoCombobox(cbfFoodCategory);
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục!");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            if (CategoryDAL.Instance.DeleteCategory(name))
            {
                MessageBox.Show("Xoá danh mục thành công");
                LoadListCategory();
                LoadCategoryIntoCombobox(cbfFoodCategory);
            }
            else
            {
                MessageBox.Show("Có lỗi khi xoá danh mục!");
            }
        }

        private void txbFoodID_TextChage(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int id = dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value == null ?
                    0 :

                int.Parse(dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value.ToString());

                Category category = CategoryDAL.Instance.GetCategoryByID(id);
                if (category != null)
                {

                    cbfFoodCategory.SelectedItem = category;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbfFoodCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbfFoodCategory.SelectedIndex = index;
                }
            }

        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
        #endregion


    }
}
