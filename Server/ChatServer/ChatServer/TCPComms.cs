using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    public class TCPComms
    {
        Socket handler;
        List<string> msgQuery;
        Mutex mutex;

        public Boolean Connected { get { return handler.Connected; } }

        public TCPComms(Socket socket, List<string> query, Mutex mut)
        {
            this.handler = socket;
            this.msgQuery = query;
            this.mutex = mut;
        }

        private String rec_Msg()
        {
            byte[] buf = new byte[1024];

            int bytesRecv = handler.Receive(buf);

            String str = Encoding.ASCII.GetString(buf, 0, bytesRecv);

            return str;
        }

        public void send_Msg(String data)
        {
            byte[] msg = Encoding.ASCII.GetBytes(DateTime.Now + "" + data);

            handler.Send(msg);
        }

        public void Run()
        {
            String message;
            while(handler.Connected)
            {
                try
                {
                    message = "";
                    if((message = rec_Msg()).Equals("") == false)
                    {
                        mutex.WaitOne();
                        msgQuery.Add(message);
                        mutex.ReleaseMutex();
                    }
                }
                catch(Exception ex)
                {
                    
                }
            }
        }
    }
}
