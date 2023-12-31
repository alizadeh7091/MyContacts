﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCobtacts
{
    public partial class Form1 : Form
    {
        IContactsRipository Ripository;
        public Form1()
        {
            InitializeComponent();
            Ripository = new ContactsRipository();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            dgContacts.AutoGenerateColumns = false;
            dgContacts.DataSource = Ripository.SelectAll();
        }

        private void dgContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            FrmAddOrEdit frm = new FrmAddOrEdit();
            frm.ShowDialog();
            if (frm.DialogResult==DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow!=null)
            {
                string name = dgContacts.CurrentRow.Cells[1].Value.ToString();
                string family = dgContacts.CurrentRow.Cells[2].Value.ToString();
                string fullname = name +" "+ family;
                if (MessageBox.Show($" آیا از حذف {fullname} مطمئن هستید ؟ ","توجه",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    int contactId = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                    Ripository.Delete(contactId);
                    BindGrid();
                }
            }
            else
            {
                MessageBox.Show("لطفا یک کاربر را از لیست انتخاب کنید");
            }
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow!=null)
            {
                int contactId = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                FrmAddOrEdit frm = new FrmAddOrEdit();
                frm.contactId = contactId;
                if (frm.ShowDialog() == DialogResult.OK) 
                {
                    BindGrid();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgContacts.DataSource = Ripository.Search(txtSearch.Text);
        }
    }
}
