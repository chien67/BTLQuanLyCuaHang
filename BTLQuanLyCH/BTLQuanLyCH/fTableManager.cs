﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL_QuanLyCH;
using DTO_QuanLyCH;
namespace GUI_QuanLyCH
{
    public partial class fTableManager : Form
    {
        private Account loginAccount;

        public Account LoginAccount 
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        public fTableManager(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            LoadTable();
            LoadCategory();
            LoadComboBoxTable(cbSwitchTable);
        }
        #region Method
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += "(" +LoginAccount.DisplayName + ")";
        }
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAL.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAL.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = DAL_Table.Instance.LoadTableList();
            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = DAL_Table.TableWidth, Height = DAL_Table.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += Btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.LightYellow;
                        break;
                    default:
                        btn.BackColor = Color.Pink;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<DTO_QuanLyCH.Menu> listBillInfo = MenuDAL.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (DTO_QuanLyCH.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txbTotalPrice.Text = totalPrice.ToString("c",culture);
        }
        void LoadComboBoxTable(ComboBox cb)
        {
            cb.DataSource = DAL_Table.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }
        #endregion

        #region Event
        private void Btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadFoodListByCategoryID(id);
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAL.Instance.GetUncheckBillIDByTableID(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;

            if (idBill == -1)
            {
                BillDAL.Instance.InsertBill(table.ID);
                BillInfoDAL.Instance.InsertBillInfo(BillDAL.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAL.Instance.InsertBillInfo(idBill, foodID, count);
            }
            ShowBill(table.ID);
            LoadTable();
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table ;

            int idBill = BillDAL.Instance.GetUncheckBillIDByTableID (table.ID);
            int discount = (int)nmDiscount.Value;

            double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(',')[0]);
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán bàn {0}\n Tổng tiền - (Tổng tiền / 100) x Giảm giá\n => {1} - ({1} / 100) x {2} = {3}", table.Name, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAL.Instance.CheckOut(idBill, discount, (float)finalTotalPrice);
                    ShowBill(table.ID);
                    LoadTable();
                }
            }
        }
        private void btnSwitchTable_Click(object sender, EventArgs e)
        {

            int id1 = (lsvBill.Tag as Table).ID;
            int id2 = (cbSwitchTable.SelectedItem as Table).ID;
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}", (lsvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DAL_Table.Instance.SwitchTable(id1, id2);
                LoadTable();
            }
        }
        #endregion

    }
}
