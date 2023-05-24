﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billing_System
{
    public partial class frm_editItem : Form
    {
        public frm_editItem()
        {
            InitializeComponent();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            connection_class.open_connection();

            string MysqlQry = "update item set itemName =('" + txt_itemName.Text + "'),itemBrandName =('" + txt_itemBrandName.Text + "'),itemPrice=(" + txt_itemPrice.Text + "),itemQuantity=(" + txt_itemQuantity.Text + ") where itemId = (" + txt_itemId.Text + ")";

            MySqlCommand cmd = new MySqlCommand(MysqlQry, connection_class.con);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Done!!");

            connection_class.close_connection();


            fndataLoad();
        }

       
        public void fndataLoad()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from item", connection_class.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "dgv_item");
            dgv_item.DataSource = ds.Tables["dgv_item"].DefaultView;

        }

        private void frm_editItem_Load(object sender, EventArgs e)
        {
            fndataLoad();
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            connection_class.open_connection();

            string MysqlQry = "delete from item where itemId =(" + txt_itemId.Text + ")";

            MySqlCommand cmd = new MySqlCommand(MysqlQry, connection_class.con);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Done!!");

            connection_class.close_connection();


            fndataLoad();
        }

      

        private void dgv_item_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_item_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvrow = dgv_item.Rows[e.RowIndex];
                txt_itemId.Text = dgvrow.Cells[0].Value.ToString();
                txt_itemName.Text = dgvrow.Cells[1].Value.ToString();
                txt_itemQuantity.Text = dgvrow.Cells[3].Value.ToString();
                txt_itemPrice.Text = dgvrow.Cells[2].Value.ToString();
                txt_itemBrandName.Text = dgvrow.Cells[4].Value.ToString();

            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            connection_class.open_connection();
            MySqlCommand cmd1 = new MySqlCommand("SELECT `itemId`, `itemName`, `itemPrice`, `itemQuantity`, `itemBrandName` FROM `item` WHERE itemName =@itemId", connection_class.con);
            cmd1.Parameters.AddWithValue("itemId", txt_searchItem.Text);

            MySqlDataReader reader1;
            reader1 = cmd1.ExecuteReader();

            if (reader1.Read())
            {
                txt_itemId.Text = reader1["itemId"].ToString();
                txt_itemName.Text = reader1["itemName"].ToString();
                txt_itemQuantity.Text = reader1["itemQuantity"].ToString();
                txt_itemPrice.Text = reader1["itemPrice"].ToString();
                txt_itemBrandName.Text = reader1["itemBrandName"].ToString();

            }
            else
            {
                MessageBox.Show("No data found");
            }
            connection_class.close_connection();
        }
    }
}
