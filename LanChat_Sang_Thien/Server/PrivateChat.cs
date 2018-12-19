﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class PrivateChat : Form
    {
        private readonly Main Main;
        public PrivateChat(Main main)
        {
            InitializeComponent();
            this.Main = main;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtInput.Text != string.Empty)
            {
                foreach (var client in from ListViewItem item in Main.clientList.SelectedItems select (Client)item.Tag)
                {
                    client.Send("pMessage|" + txtInput.Text);
                }
                txtReceive.Text += "Server says: " + txtInput.Text + "\r\n";
                txtInput.Text = string.Empty;
            }
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            txtReceive.SelectionStart = txtReceive.TextLength;
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
            }
        }
    }
}