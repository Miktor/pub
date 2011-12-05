using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Ping
{
    public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }
    public class AServer
    {
        private List<Socket> serverSockets = new List<Socket> { };
        private bool stopAccept = false;
        private Form1 myForm;
        private class ConnectionInfo
        {
            public Socket Socket;
            public byte[] Buffer;
            public StringBuilder sb = new StringBuilder();
        }

        private List<ConnectionInfo> _connections =  new List<ConnectionInfo>();

        public void Start(List <int> ports,ProtocolType type,Form1 form)
        {
            myForm = form;
            myForm.resultView.Text += "Начинаем открывать сокеты: " + Environment.NewLine;
            foreach (int port in ports)
                SetupServerSocket(port, type);
            myForm.resultView.Text += "==========================" + Environment.NewLine;
            //while(!stopAccept)
                foreach (Socket serverSocket in serverSockets)
                    serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);
        }

        private void SetupServerSocket(int _port,ProtocolType type)
        {
            IPHostEntry localMachineInfo = Dns.Resolve(Dns.GetHostName());
            IPEndPoint myEndpoint = new IPEndPoint(localMachineInfo.AddressList[0], _port);
            try
            {    
                Socket _serverSocket = new Socket(myEndpoint.Address.AddressFamily, SocketType.Stream, type);
                _serverSocket.Bind(myEndpoint);
                _serverSocket.Listen((int)SocketOptionName.MaxConnections);
                serverSockets.Add(_serverSocket);
                myForm.resultView.Text += "    " + myEndpoint.ToString() + " открыт" + Environment.NewLine;
                _serverSocket.SendTimeout = 1000;
                _serverSocket.ReceiveTimeout = 1000;
            }
            catch (Exception e) 
            {
                myForm.resultView.Text += "    " + myEndpoint.ToString() + " не открыт " + e.ToString() + Environment.NewLine;
            }
        }

        private void AcceptCallback(IAsyncResult result)
        {
            ConnectionInfo connection = new ConnectionInfo();
            try
            {
                Socket s = (Socket)result.AsyncState;
                connection.Socket = s.EndAccept(result);
                connection.Buffer = new byte[255];
                lock (_connections) _connections.Add(connection);

                connection.Socket.BeginReceive(connection.Buffer, 0, connection.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), connection);
                
                s.BeginAccept(new AsyncCallback(AcceptCallback), result.AsyncState);
            }
            catch (Exception e)
            {
                CloseConnection(connection);
                myForm.resultView.Text += "Ошибка: " + e + Environment.NewLine;
                myForm.resultView.Text += "==========================" + Environment.NewLine;
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            ConnectionInfo connection = (ConnectionInfo)result.AsyncState;
            try
            {
                int bytesRead = connection.Socket.EndReceive(result);

                if (bytesRead > 0)
                {

                    connection.sb.Append(Encoding.ASCII.GetString(connection.Buffer, 0, bytesRead));

                    string content = connection.sb.ToString();
                    if (content.IndexOf("<EOF>") > -1)
                    {
                        if (content == "1234567876543210<EOF>")
                        {
                            myForm.resultView.Text += "Принято соединение от " + connection.Socket.RemoteEndPoint + Environment.NewLine;

                            myForm.resultView.Text += "==========================" + Environment.NewLine;
                            connection.Socket.Send(connection.Buffer, SocketFlags.None);
                        }
                        else
                            CloseConnection(connection);
                    }
                    else
                    {
                        connection.Socket.BeginReceive(connection.Buffer, 0, connection.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), connection);
                    }
                }                
            }
            catch (Exception e)
            {
                CloseConnection(connection);
                myForm.resultView.Text += "Ошибка: " + e + Environment.NewLine;
                myForm.resultView.Text += "==========================" + Environment.NewLine;
            }
        }

        private void CloseConnection(ConnectionInfo ci)
        {
            ci.Socket.Close(); lock (_connections) _connections.Remove(ci);
        }
    }
}
