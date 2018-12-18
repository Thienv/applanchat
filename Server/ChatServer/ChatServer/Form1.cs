using System;
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

namespace ChatServer
{
    public partial class Form1 : Form
    {
        public static List<string> msg_Queue = new List<string>();
        public static List<Thread> cli_Cons = new List<Thread>();
        public static List<TCPComms> cli_List = new List<TCPComms>();

        private const int tcp_Port = 1337;
        public Form1()
        {
            InitializeComponent();
            InintServer();
        }

        private void InintServer()
        {
            Boolean done = false;
            Mutex mutex = new Mutex();

            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress iPAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, tcp_Port);

            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);


            lableCoonection.Text = "connec sussucecgull";
            QueryReader reader = new QueryReader(msg_Queue, cli_List, mutex);
            Thread t = new Thread(reader.Run);
            t.Start();
            while(done == false)
            {
                Socket handler = listener.Accept();
                MessageBox.Show("A client had connected!");
                mutex.WaitOne();
                cli_List.Add(new TCPComms(handler, msg_Queue, mutex));
                cli_Cons.Add(new Thread(cli_List[cli_List.Count - 1].Run));
                cli_Cons[cli_Cons.Count - 1].Start();
                mutex.ReleaseMutex();
            }
        }
    }
}
