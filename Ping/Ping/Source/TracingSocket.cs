using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Ping.Source
{
    class TracingSocket
    {
        private bool ReadyLocal;
        private bool ReadyRemote;

        private ProtocolType SType;
            
        private MySocket socket;
        private List<Socket> ConnectedSockets = null;

        TracingSocket()
        {
            ReadyLocal = false;
            ReadyRemote = false;  
        }

        public void CreateSocket(ProtocolType type, IPAddress lockalIP, int lockalPort, IPAddress remoteIP, int remotePort)
        {
            if (type == ProtocolType.Tcp)
                socket = new TcpSocket();
            else if (type == ProtocolType.Udp)
                socket = new UdpSocket();
            else
                throw new ArgumentException("wrong type of protocol", "type");

            ConnectedSockets = new List<Socket>();
            socket.LocalEndPoint = new IPEndPoint(lockalIP, lockalPort);
            socket.RemoteEndPoint = new IPEndPoint(remoteIP, remotePort);

            socket.BindSocket();

            if(    type == ProtocolType.Tcp)
                while (!MainForm.StopAllSockets)
                {
                    Socket client = null;
                    socket.AcceptConnection(ref client);
                    ConnectedSockets.Add(client);
                }
        }
        
        private void operatingLoop(Socket socket)
        {
            while (!MainForm.StopAllSockets)
            {
                
            }
        }
    }
}
