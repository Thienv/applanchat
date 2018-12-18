using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    public class QueryReader
    {
        List<String> messageQuery;
        List<TCPComms> listenerList;
        Boolean done;
        Mutex mutex;
        public QueryReader(List<String> query, List<TCPComms> listeners, Mutex mut)
        {
            this.messageQuery = query;
            this.listenerList = listeners;
            this.mutex = mut;
            done = false;
        }

        public void Run()
        {
            while(done == false)
            {
                mutex.WaitOne();
                for(int i = 0; i < listenerList.Count(); i++)
                {
                    if(listenerList[i].Connected == false)
                    {
                        listenerList.RemoveAt(i);
                    }
                }

                if(messageQuery.Count > 0)
                {
                    foreach(TCPComms listener in listenerList)
                    {
                        listener.send_Msg(messageQuery[0]);
                    }
                    messageQuery.RemoveAt(0);
                    Thread.Sleep(10);
                }
                mutex.ReleaseMutex();
            }
        }
    }
}
