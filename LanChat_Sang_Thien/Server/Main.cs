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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Main : Form
    {
        private readonly Listener listener;

        private PrivateChat pChat;

        public List<Socket> clients = new List<Socket>();

        public void BroadcastData(string data)
        {
            foreach (var socket in clients)
            {
                try
                {
                    socket.Send(Encoding.ASCII.GetBytes(data));
                }
                catch (Exception)
                {

                }
            }
        }
        public Main()
        {
            pChat = new PrivateChat(this);
            InitializeComponent();
            listener = new Listener(2014);
            listener.SocketAccepted += listener_SocketAccepted;
            Server.receivedPath = "";
        }

        private void listener_SocketAccepted(Socket e)
        {
            var client = new Client(e);
            client.Received += client_Received;
            client.Disconnected += client_Disconnected;
            this.Invoke(() =>
            {
                string ip = client.Ip.ToString().Split(':')[0];
                var item = new ListViewItem(ip); // ip
                item.SubItems.Add(" "); // nickname
                item.SubItems.Add(" "); // status
                item.Tag = client;
                clientList.Items.Add(item);
                clients.Add(e);
            });
        }

        private void client_Disconnected(Client sender)
        {
            this.Invoke(() =>
            {
                for (int i = 0; i < clientList.Items.Count; i++)
                {
                    var client = clientList.Items[i].Tag as Client;
                    if (client.Ip == sender.Ip)
                    {
                        txtReceive.Text += "<< " + clientList.Items[i].SubItems[1].Text + " has left the room >>\r\n";
                        BroadcastData("RefreshChat|" + txtReceive.Text);
                        clientList.Items.RemoveAt(i);
                    }
                }
            });
        }


        private void client_Received(Client sender, byte[] data)
        {
            this.Invoke(() =>
            {
                for (int i = 0; i < clientList.Items.Count; i++)
                {
                    var client = clientList.Items[i].Tag as Client;
                    if (client == null || client.Ip != sender.Ip) continue;
                    var command = Encoding.ASCII.GetString(data).Split('|');
                    switch (command[0])
                    {
                        case "Connect":
                            txtReceive.Text += "<< " + command[1] + " joined the room >>\r\n";
                            clientList.Items[i].SubItems[1].Text = command[1]; // nickname
                            clientList.Items[i].SubItems[2].Text = command[2]; // status
                            string users = string.Empty;
                            for (int j = 0; j < clientList.Items.Count; j++)
                            {
                                users += clientList.Items[j].SubItems[1].Text + "|";
                            }
                            BroadcastData("Users|" + users.TrimEnd('|'));
                            BroadcastData("RefreshChat|" + txtReceive.Text);
                            break;
                        case "Message":
                            txtReceive.Text += command[1] + " says: " + command[2] + "\r\n";
                            BroadcastData("RefreshChat|" + txtReceive.Text);
                            break;
                        case "pMessage":
                            this.Invoke(() =>
                            {
                                pChat.txtReceive.Text += command[1] + " says: " + command[2] + "\r\n";
                            });
                            break;
                        case "pChat":

                            break;
                    }
                }
            });
        }

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {
            txtReceive.SelectionStart = txtReceive.TextLength;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            listener.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            listener.Stop();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtInput.Text != string.Empty)
            {
                BroadcastData("Message|" + txtInput.Text);
                txtReceive.Text += txtInput.Text + "\r\n";
                txtInput.Text = "Admin says: ";
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var client in from ListViewItem item in clientList.SelectedItems select (Client)item.Tag)
            {
                client.Send("Disconnect|");
            }
        }

        private void chatWithClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var client in from ListViewItem item in clientList.SelectedItems select (Client)item.Tag)
            {
                client.Send("Chat|");
                pChat = new PrivateChat(this);
                pChat.Show();
            }
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
            }
        }
        private void menu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void clientList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Server.receivedPath = fd.SelectedPath;
            }
        }
        Server obj = new Server();
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            obj.StartServer();
        }
        class Server
        {
            IPEndPoint ipEnd;
            Socket sock;
            public Server()
            {
                ipEnd = new IPEndPoint(IPAddress.Any, 5656);
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                sock.Bind(ipEnd);
            }
            public static string receivedPath;
            public static string curMsg = "Stopped";
            public void StartServer()
            {
                try
                {
                    curMsg = "Starting...";
                    sock.Listen(100);

                    curMsg = "Running and waiting to receive file.";
                    Socket clientSock = sock.Accept();

                    byte[] clientData = new byte[1024 * 5000];

                    int receivedBytesLen = clientSock.Receive(clientData);
                    curMsg = "Receiving data...";

                    int fileNameLen = BitConverter.ToInt32(clientData, 0);
                    string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);

                    BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + "/" + fileName, FileMode.Append)); ;
                    bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);

                    curMsg = "Saving file...";

                    bWrite.Close();
                    clientSock.Close();
                    curMsg = "Reeived & Saved file; Server Stopped.";
                }
                catch (Exception ex)
                {
                    curMsg = "File Receving error.";
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
