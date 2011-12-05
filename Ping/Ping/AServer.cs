//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Drawing;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Windows.Forms;

//namespace Ping
//{
//    public class StateObject
//    {
//         Client  socket.
//        public Socket workSocket = null;
//         Size of receive buffer.
//        public const int BufferSize = 1024;
//         Receive buffer.
//        public byte[] buffer = new byte[BufferSize];
//         Received data string.
//        public StringBuilder sb = new StringBuilder();
//    }

//    public class AServer
//    {
//        private List<Socket> serverSockets = new List<Socket> { };
//        private MainForm myForm;
//        private bool createClients;
//        private string myIp;
//        private List<AClient> clients = new List<AClient> { };

//        private class ConnectionInfo
//        {
//            public Socket Socket;
//            public byte[] Buffer;
//            public StringBuilder sb = new StringBuilder();
//        }

//        private List<ConnectionInfo> _connections = new List<ConnectionInfo>();

//        private void addRow2grid(string rip, string lip, int status, string ex, ProtocolType type)
//        {
//            try
//            {
               
//            }
//            catch (Exception)
//            {
//            }
//        }

//        public void Start(List<int> UDPports, List<int> TCPports, int type, MainForm form, bool _createClients, string ip, int optPort)
//        {
//            myForm = form;
//            createClients = _createClients;
//            myIp = ip;

//            Trace.WriteLine("SEVRER: Начинаем открывать сокеты: ");
//            if (optPort != 0)
//                AddNewSoc(optPort, ProtocolType.Tcp, ip);
//            if (type == 2 || type == 3)
//                foreach (int port in UDPports)
//                    AddNewSoc(port, ProtocolType.Udp, ip);
//            if (type == 1 || type == 3)
//                foreach (int port in TCPports)
//                    AddNewSoc(port, ProtocolType.Tcp, ip);
//            Trace.WriteLine("SEVRER: Сокеты открыты ");
//        }

//        public void Stop()
//        {
//            lock (clients) foreach (AClient client in clients)
//                    client.Stop();
//            lock (_connections) foreach (ConnectionInfo sock in _connections)
//                    CloseConnection(sock);
//            lock (serverSockets) foreach (Socket sock in serverSockets)
//                    sock.Close();
//            lock (clients) clients.Clear();
//            lock (_connections) _connections.Clear();
//            lock (serverSockets) serverSockets.Clear();
//            Trace.WriteLine("SEVRER: Сервер остановлен!");
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                Stop();
//            }
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        private Socket SetupServerSocket(int _port, ProtocolType type, string ip)
//        {
//            IPEndPoint myEndpoint = new IPEndPoint(IPAddress.Parse(ip), _port);
//            try
//            {
//                Socket _serverSocket = new Socket(myEndpoint.Address.AddressFamily, SocketType.Stream, type);
//                _serverSocket.Bind(myEndpoint);
//                _serverSocket.Listen((int)SocketOptionName.MaxConnections);
//                lock (serverSockets) serverSockets.Add(_serverSocket);
//                Trace.WriteLine("SEVRER: " + myEndpoint.ToString() + " открыт");
//                _serverSocket.SendTimeout = 1000;
//                _serverSocket.ReceiveTimeout = 1000;
//                return _serverSocket;
//            }
//            catch (Exception e)
//            {
//                Trace.WriteLine("SEVRER: " + myEndpoint.ToString() + " не открыт " + e.ToString());
//                addRow2grid(myEndpoint.ToString(), "", 2, e.ToString(), type);
//                return null;
//            }
//        }

//        private void AddNewSoc(int _port, ProtocolType type, string ip)
//        {
//            Socket s = SetupServerSocket(_port, type, ip);
//            s.BeginAccept(new AsyncCallback(AcceptCallback), s);
//        }

//        private void AcceptCallback(IAsyncResult result)
//        {
//            ConnectionInfo connection = new ConnectionInfo();
//            try
//            {
//                Socket s = (Socket)result.AsyncState;
//                connection.Socket = s.EndAccept(result);
//                connection.Buffer = new byte[255];
//                lock (_connections) _connections.Add(connection);

//                connection.Socket.BeginReceive(connection.Buffer, 0, connection.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), connection);

//                // s.BeginAccept(new AsyncCallback(AcceptCallback), result.AsyncState);
//            }
//            catch (Exception e)
//            {
//                CloseConnection(connection);
//                Trace.WriteLine("SEVRER: Ошибка: " + e);
//            }
//        }

//        private void ReceiveCallback(IAsyncResult result)
//        {
//            ConnectionInfo connection = (ConnectionInfo)result.AsyncState;
//            int bytesRead = connection.Socket.EndReceive(result);
//            try
//            {
//                if (bytesRead > 0)
//                {
//                    connection.sb.Append(Encoding.ASCII.GetString(connection.Buffer, 0, bytesRead));

//                    string content = connection.sb.ToString();

//                    if (content == "1234567876543210<EOF>")
//                    {
//                        Trace.WriteLine("SEVRER: Принято соединение от " + connection.Socket.RemoteEndPoint);
//                        addRow2grid(connection.Socket.RemoteEndPoint.ToString(), connection.Socket.LocalEndPoint.ToString(), 0, "", connection.Socket.ProtocolType);
//                        connection.Socket.Send(connection.Buffer, SocketFlags.None);
//                        if (createClients)
//                        {
//                            AClient client = new AClient();
//                            lock (clients) clients.Add(client);
//                            string lp = connection.Socket.RemoteEndPoint.ToString();
//                            string _ip = lp.Substring(0, lp.IndexOf(":"));
//                            lp = connection.Socket.LocalEndPoint.ToString();
//                            string _port = lp.Substring(lp.IndexOf(":") + 1);
//                            int portType = 0;
//                            if (connection.Socket.ProtocolType == ProtocolType.Tcp)
//                                portType = 1;
//                            else
//                                portType = 2;
//                            client.StartClient(new List<int> { int.Parse(_port) }, new List<int> { int.Parse(_port) }, portType, _ip, myForm, false, 0);
//                            lock (clients) clients.Add(client);
//                        }
//                        else
//                            CloseConnection(connection);
//                    }
//                    else if (content.StartsWith("AddPorts") && content.EndsWith("<EOF>"))
//                    {
//                        int i = 0;
//                        Trace.WriteLine("SEVRER: Принято соединение на открытие новых портов от " + connection.Socket.RemoteEndPoint + Environment.NewLine + content);
//                        string[] ports = content.Split(';');
//                        for (i = 1; ports[i] != "UDP"; i++)
//                        {
//                            int tPort = 0;
//                            if (int.TryParse(ports[i], out tPort) && !Findport(tPort) && tPort > 0)
//                            {
//                                AddNewSoc(tPort, ProtocolType.Udp, myIp);
//                            }
//                        }
//                        for (i++; ports[i] != "1234567876543210<EOF>"; i++)
//                        {
//                            int tPort = 0;
//                            if (int.TryParse(ports[i], out tPort) && !Findport(tPort) && tPort > 0)
//                            {
//                                AddNewSoc(tPort, ProtocolType.Tcp, myIp);
//                            }
//                        }
//                        connection.Socket.Send(Encoding.ASCII.GetBytes("PortsOpened;1234567876543210<EOF>"), SocketFlags.None);
//                    }

//                    else
//                    {
//                        connection.Socket.BeginReceive(connection.Buffer, 0, connection.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), connection);
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                Trace.WriteLine("SEVRER: Ошибка: " + e);
//                addRow2grid(connection.Socket.RemoteEndPoint.ToString(), connection.Socket.LocalEndPoint.ToString(), 0, "", connection.Socket.ProtocolType);
//                CloseConnection(connection);
//            }
//        }

//        private bool Findport(int port)
//        {
//            foreach (Socket s in serverSockets)
//                if (s.LocalEndPoint.ToString().EndsWith(port.ToString()))
//                    return true;
//            return false;
//        }

//        private void CloseConnection(ConnectionInfo ci)
//        {
//            if (ci.Socket != null)
//                ci.Socket.Close();
//            lock (_connections) _connections.Remove(ci);
//        }
//    }
//}