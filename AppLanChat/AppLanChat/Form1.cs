﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppLanChat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.InitLogin();
            panelChatClient.Hide();
            CheckForIllegalCrossThreadCalls = false;
            threadReceive = new Thread(new ThreadStart(ReceivedByClient));
            threadReceive.Start();


        }
       
        private void InitLogin()
        {
            Panel panelLogin = new Panel();            
            TextBox txtUserName = new TextBox();
            TextBox txtPassword = new TextBox();
            Label labelUsername = new Label();
            Label labelPassword = new Label();
            Button btnLogin = new Button();
            panelLogin.Controls.Add(txtUserName);
            panelLogin.Controls.Add(txtPassword);
            panelLogin.Controls.Add(labelUsername);
            panelLogin.Controls.Add(labelPassword);
            panelLogin.Controls.Add(btnLogin);

            panelLogin.Size = new Size(200, 200);
            panelLogin.Location = new Point(50, 50);
           
            panelLogin.BorderStyle = BorderStyle.FixedSingle;

            labelUsername.Text = "UserName";
            labelUsername.Location = new Point(2, 20);
            txtUserName.Location = new Point(70, 20);





            labelPassword.Text = "Password";
            labelPassword.Location = new Point(2, 60);
            txtPassword.Location = new Point(70,60);
            txtPassword.PasswordChar = '*';

            btnLogin.Text = "Login";
            btnLogin.Location = new Point(70, 100);
            btnLogin.Click += new EventHandler(btnLogin_Click);
            this.Controls.Add(panelLogin);

        }
        Thread threadReceive;
        public void ReceivedByClient()
        {
            Socket socketReceive = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int portReceive = 40001;
            IPEndPoint iPEndPointReceive = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portReceive);
            socketReceive.Bind(iPEndPointReceive);
            socketReceive.Listen(10);
            while (true)
            {
                Socket temp = null;
                try
                {
                    temp = socketReceive.Accept();
                    byte[] messageReceivedByServer = new byte[100];
                    int sizeOfReceivedMessage = temp.Receive(messageReceivedByServer, SocketFlags.None);
                    string str = Encoding.ASCII.GetString(messageReceivedByServer);
                    txtShowMessage.Text += "\r\nServer: " + str;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.HelpLink + "\n" + ex.InnerException
                            + "\n" + ex.Source + "\n" + ex.TargetSite);
                }
                finally
                {
                    temp.Close();
                }
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            panelChatClient.Show();
        }

        private void btnSent_Click(object sender, EventArgs e)
        {
            int portSend = 40000;
            IPEndPoint iPEndPointSend = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portSend);
            Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string messageTextBox = txtMessage.Text;
            byte[] messageSentFromClient;
            try
            {
                socketSend.Connect(iPEndPointSend);
                messageSentFromClient = Encoding.ASCII.GetBytes(messageTextBox);
                socketSend.Send(messageSentFromClient, SocketFlags.None);
                txtShowMessage.Text += "\r\nClient: " + messageTextBox;
                txtMessage.Text = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.HelpLink + "\n" + ex.InnerException
                        + "\n" + ex.Source + "\n" + ex.TargetSite);
            }
            finally
            {
                socketSend.Close();
            }
        }
    }
}
