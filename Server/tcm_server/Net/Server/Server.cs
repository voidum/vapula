using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCM.Net
{
    public partial class Server
    {
        private TcpListener _Ltn = null;

        private int _Port = 80;
        private string _RootPath = null;
        private int _MaxConnection = 16;

        public int Port
        {
            get { return _Port; }
        }

        public string RootPath
        {
            get { return _RootPath; }
        }

        public bool Start()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, _Port);
            _Ltn = new TcpListener(ipep);
            _Ltn.Start(_MaxConnection);
            return true;
        }

        public bool Stop()
        {
            _Ltn.Stop();
            return true;
        }

        public bool Restart()
        {
            Stop();
            Start();
            return false;
        }

        public Server()
        {
        }

        public void SendResponse(byte[] data, Socket socket)
        {
            int total = 0;
            try
            {
                if (socket.Connected)
                {
                    total = socket.Send(data, data.Length, 0);
                    if (total == -1) Console.WriteLine("Fail to send data");
                    else Console.WriteLine("Send bytes total : {0}", total);
                }
                else Console.WriteLine("Fail to connect");
            }
            catch (Exception e)
            {
                Console.WriteLine("发生错误 : {0} ", e);
            }
        }

        public void ThreadProc_Listen()
        {
            byte[] buffer = new byte[4096];
            while (true)
            {
                Socket socket = _Ltn.AcceptSocket();
                Console.WriteLine("Socket Type " + socket.SocketType);
                if (socket.Connected)
                {
                    Console.WriteLine("\n\nClient IP {0}\n", socket.RemoteEndPoint);
                    int i = socket.Receive(buffer, buffer.Length, 0);
                    string req_str = Encoding.ASCII.GetString(buffer);
                    Request req = Request.Parse(req_str);

                    socket.Close();
                }
            }
        }
    }
}
