using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace Ping
{
    class PingingSocket : IDisposable
    {
        #region def
        public Int32 Port { get { return _port; } }
        //public IPAddress Ip { get { return _ip; } }
        public ProtocolType Type { get { return _type; } }
        public string Result { get { return _result; } }

        public EndPoint LocalEndPoint { get { return resievingSocket.LocalEndPoint; } 
            set { resievingSocket.LocalEndPoint = value; } }
        public EndPoint RemoteEndPoint { get { return resievingSocket.RemoteEndPoint; } set { resievingSocket.RemoteEndPoint = value; } }

        private MySocket resievingSocket = null;
        private MySocket sendingSocket = null;
        private Int32 _port;
        private string _result = "";

        private EndPoint _localEP = null;
        private EndPoint _remoteEP = null;

        ProtocolType _type;

        private ManualResetEvent _doneEvent;
        #endregion

        public PingingSocket(EndPoint remoteEP, EndPoint localEP, ProtocolType type, ManualResetEvent doneEvent)
        {
            _localEP = localEP;
            _remoteEP = remoteEP;
            _type = type;
            _doneEvent = doneEvent;

            _port = SupportiveFunctions.getPort(remoteEP);

            if (_type == ProtocolType.Tcp)
            {
                resievingSocket = new TcpSocket();
                //socket.ConnectTo(_ip, _port);
            }
            else if (_type == ProtocolType.Udp)
            {
                resievingSocket = new UdpSocket();
                sendingSocket = new UdpSocket();
                //socket.CreateSocket(_ip, _port);
            }
            else
            {
                _result = "error";
                return;
            }

            resievingSocket.LocalEndPoint = localEP;
        }

        public void ThreadPoolCallback(Object threadContext)
        {
            //int threadIndex = (int)threadContext;
            _result = TryPing();
            _doneEvent.Set();
        }

        public string TryPing()
        {              
            string content = string.Empty;             

            try
            {
                if (_type == ProtocolType.Tcp)
                {
                    //socket = new TcpSocket();
                    resievingSocket.ConnectTo(_remoteEP, _localEP);
                }
                else if (_type == ProtocolType.Udp)
                {
                    //socket = new UdpSocket();
                    resievingSocket.ConnectTo(_remoteEP, _localEP);
                }
                else
                    return "error";       

                while (!MainForm.StopAllSockets)
                { 
                    resievingSocket.SendString("TryPing;<EOF>");                  
                    resievingSocket.ReciveString(ref content);

                    if (content == "PingOK;<EOF>")
                    {                          
                        if (resievingSocket != null)
                        {                              
                            resievingSocket.Close();
                            resievingSocket = null;
                        }                        
                        return "true";
                    }                                      
                }
                return "false";
            }
            catch (Exception e)
            { return e.ToString(); }
            finally
            {
                if (resievingSocket != null)
                {                      
                    resievingSocket.Close();
                    resievingSocket = null;
                }
            }
        }

        #region dispose
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (resievingSocket != null)
                {                        
                    resievingSocket.Close();
                    resievingSocket = null;
                }
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    class ListeningSocket
    {
        #region def
        public Int32 Port { get { return _port; } }
        //public IPAddress Ip { get { return _ip; } }
        public ProtocolType Type { get { return _type; } }
        public string Result { get { return _result; } }

        private Int32 _port;
        ProtocolType _type;
        private string _result = "";

        private EndPoint _localEP = null;

        private ManualResetEvent _doneEvent;
        private MySocket socket = null;   
        #endregion

        public ListeningSocket(EndPoint localEP, ProtocolType type, ManualResetEvent doneEvent)
        {
            _localEP = localEP;
            _type = type;
            _doneEvent = doneEvent;

            _port = SupportiveFunctions.getPort(localEP);
        }

        public void ThreadPoolCallback(Object threadContext)
        {
            //int threadIndex = (int)threadContext;
            ListenPing();
            _doneEvent.Set();
        }

        public void ListenPing()
        {              
            string content = string.Empty;                
            MySocket ConnectedSocket = null;              

            try
            {

                if (_type == ProtocolType.Tcp)
                {
                    socket = new TcpSocket();
                    ConnectedSocket = new TcpSocket();            
                }
                else if (_type == ProtocolType.Udp)
                {
                    socket = new UdpSocket();
                    ConnectedSocket = new UdpSocket();                    
                }
                else
                {
                    _result = "error: ";
                    return;
                }

                socket.CreateSocket(_localEP);
                ConnectedSocket.Port = _port;

                _result = "ready";

                while (!MainForm.StopAllSockets)
                {
                    Thread.Sleep(500);
                    try
                    {                          
                        if (_type == ProtocolType.Tcp)
                            if (!socket.AcceptConnection(ref ConnectedSocket.socket))
                                continue;
                            else
                            {
                                ConnectedSocket.ReciveString(ref content);

                                if (content == "TryPing;<EOF>")
                                {
                                    ConnectedSocket.SendString("PingOK;<EOF>");
                                    _result = "true";
                                }
                                else if (content == "Exit;<EOF>")
                                {
                                    _result = "exited";
                                    break;
                                }
                            }
                        else
                        {
                            socket.ReciveString(ref content);
                            
                            if (content == "TryPing;<EOF>")
                            {
                                socket.SendString("PingOK;<EOF>");
                                _result = "true";
                            }
                            else if (content == "Exit;<EOF>")
                            {
                                _result = "exited";
                                break;
                            }
                        }

                       
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e.ToString());
                        _result = "error: " + e.ToString();
                    }
                    finally
                    {
                        if (ConnectedSocket.socket != null)
                        {                               
                            ConnectedSocket.Close();                              
                        }
                        _result = "exited";
                    }                     
                }
            }
            catch (Exception e)
            {
                _result = "error: " + e.ToString();
            }
            finally
            {
                if (ConnectedSocket != null)
                {                      
                    ConnectedSocket.Close();
                    ConnectedSocket = null;
                }      

                if (socket != null)
                {                       
                    socket.Close();
                    socket = null;
                }
            }
        }

        #region dispose
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (socket != null)
                {                     
                    socket.Close();
                    socket = null;
                }
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
