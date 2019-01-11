using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Client
{
    public partial class PublicChatForm : Form
    {
        public PublicChatForm()
        {
            pChat = new PrivateChatForm(this);
            InitializeComponent();
        }

        private void PublicChatForm_Load(object sender, EventArgs e)
        {

        }
        public readonly LoginForm formLogin = new LoginForm();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            formLogin.Client.Received += _client_Received;
            formLogin.Client.Disconnected += Client_Disconnected;
            Text = "TCP Chat - " + formLogin.txtIP.Text + " - (Connected as: " + formLogin.txtNickname.Text + ")";
            formLogin.ShowDialog();
        }
        private readonly PrivateChatForm pChat;
        public void _client_Received(ClientSettings cs, string received)
        {
            var cmd = received.Split('|');
            switch (cmd[0])
            {
                case "Users":
                    this.Invoke(() =>
                    {
                        userList.Items.Clear();
                        for (int i = 1; i < cmd.Length; i++)
                        {
                            if (cmd[i] != "Connected" | cmd[i] != "RefreshChat")
                            {
                                userList.Items.Add(cmd[i]);
                            }
                        }
                    });
                    break;
                case "Message":
                    this.Invoke(() =>
                    {
                        txtReceive.Text += cmd[1] + "\r\n";
                    });
                    break;
                case "RefreshChat":
                    this.Invoke(() =>
                    {
                        txtReceive.Text = cmd[1];
                    });
                    break;
                case "Chat":
                    this.Invoke(() =>
                    {
                        pChat.Text = pChat.Text.Replace("user", formLogin.txtNickname.Text);
                        pChat.Show();
                    });
                    break;
                case "pMessage":
                    this.Invoke(() =>
                    {
                        pChat.txtReceive.Text += "Server says: " + cmd[1] + "\r\n";
                    });
                    break;
                case "Disconnect":
                    Application.Exit();
                    break;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtInput.Text != string.Empty)
            {
                formLogin.Client.Send("Message|" + formLogin.txtNickname.Text + "|" + txtInput.Text);
                txtReceive.Text += formLogin.txtNickname.Text + " says: " + txtInput.Text + "\r\n";
                txtInput.Text = string.Empty;
            }
        }
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
            }
        }

        private void userList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {
            txtReceive.SelectionStart = txtReceive.TextLength;
            
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {

        }
        private void privateChat_Click(object sender, EventArgs e)
        {
            formLogin.Client.Send("pChat|" + formLogin.txtNickname.Text);
        }
        private static void Client_Disconnected(ClientSettings cs)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            FileDialog fDg = new OpenFileDialog();
            if (fDg.ShowDialog() == DialogResult.OK)
            {
                Client.SendFile(fDg.FileName);
            }
        }
        class Client
        {
            public static string curMsg = "Idle";
            public static void SendFile(string fileName)
            {
                try
                {
                    IPAddress[] ipAddress = Dns.GetHostAddresses("localhost");
                    IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 5656);
                    Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);


                    string filePath = "";

                    fileName = fileName.Replace("\\", "/");
                    while (fileName.IndexOf("/") > -1)
                    {
                        filePath += fileName.Substring(0, fileName.IndexOf("/") + 1);
                        fileName = fileName.Substring(fileName.IndexOf("/") + 1);
                    }


                    byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
                    if (fileNameByte.Length > 850 * 1024)
                    {
                        curMsg = "File size is more than 850kb, please try with small file.";
                        return;
                    }

                    curMsg = "Buffering ...";
                    byte[] fileData = File.ReadAllBytes(filePath + fileName);
                    byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                    byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                    fileNameLen.CopyTo(clientData, 0);
                    fileNameByte.CopyTo(clientData, 4);
                    fileData.CopyTo(clientData, 4 + fileNameByte.Length);

                    curMsg = "Connection to server ...";
                    clientSock.Connect(ipEnd);

                    curMsg = "File sending...";
                    clientSock.Send(clientData);

                    curMsg = "Disconnecting...";
                    clientSock.Close();
                    curMsg = "File transferred.";

                }
                catch (Exception ex)
                {
                    if (ex.Message == "No connection could be made because the target machine actively refused it")
                        curMsg = "File Sending fail. Because server not running.";
                    else
                        curMsg = "File Sending fail." + ex.Message;
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtReceive.Text = Client.curMsg;
        }
    }
}
