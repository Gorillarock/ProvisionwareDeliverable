using System;
using System.Net.Sockets;
using System.Net;

namespace SocketConnector
{
    public class socketListen
    {
        string connectionString;
        private byte[] _ip = new byte[] { 127, 0, 0, 1 };
        private int _port = 6000;
        //Listens for a connection from TCP clients
        TcpListener listen = null;
        //provides Client Connection
        TcpClient client = null;
        //Stream of Network Data
        NetworkStream networkStreamData = null;

        public socketListen()
        {
            IPAddress ip = new IPAddress(_ip);
            listen = new TcpListener(ip, _port);
        }

        public socketListen(int port)
        {
            IPAddress ip = new IPAddress(_ip);
            _port = port;
            listen = new TcpListener(ip, _port);
        }

        //return a byteBuffer
        public byte[] OpenPortRX()
        {
            byte[] buffer;
            try
            {
                //AddText(logtxtbx, "Server Listening on: " + ip[0] + "." + ip[1] + "." + ip[2] + "." + ip[3] + "/" + port);
                listen.Start();
                //buffer
                const int bytesize = 100000;
                buffer = new byte[bytesize];
                //

                //accepts a pending connection request
                var senderRequest = listen.AcceptTcpClient();
                //

                //read from client request stream
                senderRequest.GetStream().Read(buffer, 0, bytesize);


                senderRequest.Close();
                listen.Stop();
                return buffer;

            }
            catch (Exception e)
            {
                //AddText(logtxtbx, "Listening was interupted: " + e.ToString());
                return new byte[1];
            }


        }

        public void stopListening()
        {
            listen.Stop();
        }
    }

    public class socketTransmit
    {
        string ipAddress = "localhost";
        private byte[] _ip = new byte[] { 127, 0, 0, 1 };
        private int _port = 6000;
        //Listens for a connection from TCP clients
        TcpListener listen = null;
        //provides Client Connection
        TcpClient client = null;
        //Stream of Network Data
        NetworkStream networkStreamData = null;


        public socketTransmit()
        {
            IPAddress ip = new IPAddress(_ip);
            listen = new TcpListener(ip, _port);
        }


        public socketTransmit(int port)
        {
            _port = port;
            IPAddress ip = new IPAddress(_ip);
            listen = new TcpListener(ip, _port);
        }


        public void OpenPortTX(byte[] sendData)
        {
            try
            {
                using (client = new TcpClient(ipAddress, _port))
                {
                    //
                    //int byteCount = Encoding.ASCII.GetByteCount(message);
                    //
                    //sendData = new byte[byteCount];
                    //
                    //sendData = Encoding.ASCII.GetBytes(message);
                    //
                    networkStreamData = client.GetStream();
                    //
                    networkStreamData.Write(sendData, 0, sendData.Length);
                    //
                    networkStreamData.Close();
                    client.Close();

                }
            }
            catch (Exception exception)
            {
                //AddText(logtxtbx, exception.ToString());
                Console.WriteLine(exception.ToString());
            }
        }
    }
}
