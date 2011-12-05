using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Ping
{

    abstract class MySocket
    {
        public int Port { get { return _port; } set { _port = value; } }
        public bool OpenedToListen { get { return _openedToListen; } set { _openedToListen = value; } }
        public bool OpenedToPing { get { return _openedToPing; } set { _openedToPing = value; } }
        public ProtocolType Type { get { return _type; } set { _type = value; } }
        public bool PingedTo { get { return _pingedTo; } set { _pingedTo = value; } }
        public bool PingedFrom { get { return _pingedFrom; } set { _pingedFrom = value; } }
        public EndPoint LocalEndPoint { get { return localEndPoint; } set { localEndPoint = value; } }
        public EndPoint RemoteEndPoint { get { return remoteEndPoint; } set { remoteEndPoint = value; } }

        protected int _port;
        protected bool _openedToListen;
        protected bool _openedToPing;
        protected bool _pingedTo;
        protected bool _pingedFrom;
        protected ProtocolType _type;

        protected EndPoint localEndPoint = null;
        protected EndPoint remoteEndPoint = null;
        protected EndPoint lastConnected = null;

        protected byte[] buffer = new byte[1024];

        public Socket socket;

        //public MySocket(int port, ProtocolType type, bool openedToListen)
        //{
        //    _port = port;
        //    _openedToListen = openedToListen;
        //    _type = type;

        //    _openedToPing = false;

        //    _pingedTo = false;
        //    _pingedFrom = false;
        //}

        abstract public void CreateSocket(EndPoint localIP);
        abstract public void ConnectTo(EndPoint remoteEP, EndPoint localIP);

        abstract public bool AcceptConnection(ref Socket clientsocket);

        abstract public void SendString(string str);
        abstract public int ReciveString(ref string outValue);

        abstract public void Close();
    }

    class TcpSocket : MySocket
    {
        public TcpSocket()
        {
            _openedToListen = false;
            _type = ProtocolType.Tcp;

            _openedToPing = false;

            _pingedTo = false;
            _pingedFrom = false;
        }

        public TcpSocket(int port, bool openedToListen)
        {
            _port = port;
            _openedToListen = openedToListen;
            _type = ProtocolType.Tcp;

            _openedToPing = false;

            _pingedTo = false;
            _pingedFrom = false;
        }


        override public void CreateSocket(EndPoint localIP)
        {
            localEndPoint = localIP;

            socket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Blocking = false;
            socket.SendTimeout = 20000;
            socket.ReceiveTimeout = 20000;
            socket.Bind(localEndPoint);
            socket.Listen(10);

            _openedToListen = true;
        }

        override public void ConnectTo(EndPoint remoteEP, EndPoint localIP)
        {
            remoteEndPoint = remoteEP;
            localEndPoint = localIP;

            socket = new Socket(remoteEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);              
            socket.SendTimeout = 20000;
            socket.ReceiveTimeout = 20000;

            while (!MainForm.StopAllSockets && !socket.Connected)
            {
                Thread.Sleep(500);
                try
                {                                            
                    socket.Connect(remoteEP);
                    socket.Blocking = false;
                    return;
                }
                catch (SocketException e)
                {
                    if (e.ErrorCode != 10035 && e.ErrorCode != 10037)
                        throw;                       
                }
            }
        }

        public override bool AcceptConnection(ref Socket clientsocket)
        {           
            while (!MainForm.StopAllSockets)
            {
                Thread.Sleep(500);
                try
                {
                    clientsocket = socket.Accept();
                    return true;
                }
                catch (SocketException e)
                {
                    if (e.ErrorCode != 10035)
                        throw;
                }
            }
            return false;
        }

        override public void SendString(string str)
        {             
            while (!MainForm.StopAllSockets)
            {
                Thread.Sleep(500);
                try
                {                       
                    socket.Send(Encoding.ASCII.GetBytes(str), SocketFlags.None);
                    return;
                }
                catch (SocketException e)
                {
                    if (e.ErrorCode != 10035)
                    {
                        throw;
                    }
                    else continue;
                }
            }
        }

        override public int ReciveString(ref string outValue)
        {
            int bytesRead = 0;
            while (!MainForm.StopAllSockets)
            {
                Thread.Sleep(500);
                try
                {
                    bytesRead = socket.Receive(buffer);
                    outValue = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    return bytesRead;
                }
                catch (SocketException e)
                {
                    if (e.ErrorCode != 10035)
                    {
                        throw;
                    }
                    else continue;
                }
            }
            
            return -1;
        }

        override public void Close()
        {

            if (socket != null)
            {
                socket.Close();
                socket = null;
            }
        }
    }

    class UdpSocket : MySocket
    {       
        public UdpSocket()
        {
            _openedToListen = false;
            _type = ProtocolType.Udp;

            _openedToPing = false;

            _pingedTo = false;
            _pingedFrom = false;
        }

        public UdpSocket(int port, bool openedToListen)
        {
            _port = port;
            _openedToListen = openedToListen;
            _type = ProtocolType.Udp;

            _openedToPing = false;

            _pingedTo = false;
            _pingedFrom = false;
        }

        override public void CreateSocket(EndPoint localIP)
        {
            localEndPoint = localIP;

            socket = new Socket(localEndPoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            socket.Blocking = false;
            socket.SendTimeout = 20000;
            socket.ReceiveTimeout = 20000;
            socket.Bind(localEndPoint);             

            _openedToListen = true;
        }

        override public void ConnectTo(EndPoint remoteEP, EndPoint localIP)
        {
            remoteEndPoint = remoteEP;
            localEndPoint = localIP;
            lastConnected = remoteEP;

            socket = new Socket(remoteEndPoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            socket.Blocking = false;
            socket.SendTimeout = 20000;
            socket.ReceiveTimeout = 20000;

            _openedToPing = true;
        }

        public override bool AcceptConnection(ref Socket clientsocket)
        {
            throw new NotImplementedException();
        }

        override public void SendString(string str)
        {     
            while (!MainForm.StopAllSockets)
            {
                Thread.Sleep(500);
                try
                {                   
                    socket.SendTo(Encoding.ASCII.GetBytes(str), lastConnected);  
                    return;
                }
                catch (SocketException e)
                {
                    if (e.ErrorCode != 10035)
                    {
                        throw;
                    }
                    else continue;
                }
            }
            return;
        }

        override public int ReciveString(ref string outValue)
        {
            int bytesRead = 0;
            lastConnected = localEndPoint;
            while (!MainForm.StopAllSockets)
            {
                Thread.Sleep(500);
                try
                {                     
                    bytesRead = socket.ReceiveFrom(buffer, ref lastConnected);   
                    outValue = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    return bytesRead;
                }
                catch (SocketException e)
                {
                    if (e.ErrorCode != 10035)
                    {
                        throw;
                    }
                    else continue;
                }
            }
            return -1;
        }

        override public void Close()
        {
            if (socket != null)
            {
                if (socket.Connected)
                    socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
            }
        }
    }
}
