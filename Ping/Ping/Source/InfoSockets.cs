using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ping
{
    class ConnectionInfo
    {
        public MySocket Socket;
        public Thread Thread;        
    }

    class InfoSockets
    {
        private List<ConnectionInfo> AcceptedClients = new List<ConnectionInfo> { };
        private TcpSocket optionPort = null;
        private IPAddress ServerIP;
        private List<Int32> TCPPorts;
        private List<Int32> UDPPorts;

        private IPAddress localIP = null;

        private List<ListeningSocket> ListeningPorts = new List<ListeningSocket> { };
        private List<ManualResetEvent> ListeningDoneEvents = new List<ManualResetEvent> { };

        MainForm myForm;           

        public void Start(Int32 port, IPAddress ip, MainForm form)
        {             
            TCPPorts = new List<Int32> { };
            UDPPorts = new List<Int32> { };
            ServerIP = ip;

            SupportiveFunctions.output = form.Output;

            try
            {
                optionPort = new TcpSocket();
                optionPort.CreateSocket(new IPEndPoint(ServerIP, port));
                optionPort.socket.Blocking = false;

                SupportiveFunctions.Splitter();
                SupportiveFunctions.PrintText("Сервер запущен, IP " + ServerIP.ToString());

                //byte[] outValue = BitConverter.GetBytes(1);                  
                //optionPort.IOControl(IOControlCode.NonBlockingIO, BitConverter.GetBytes(1), outValue);
                //uint bytesAvailable = BitConverter.ToUInt32(outValue, 0);

                while (!MainForm.StopAllSockets)
                {
                    try
                    {
                        ConnectionInfo connection = new ConnectionInfo();
                        connection.Socket = new TcpSocket();

                        if (optionPort.AcceptConnection(ref connection.Socket.socket))
                        { 
                            connection.Thread = new Thread(ProcessConnection);
                            connection.Thread.Name = connection.Socket.socket.RemoteEndPoint.ToString();
                            connection.Thread.IsBackground = true;
                            connection.Thread.Start(connection);

                            SupportiveFunctions.PrintText("Принято соединение от " + connection.Socket.socket.RemoteEndPoint.ToString());

                            lock (AcceptedClients) { AcceptedClients.Add(connection); }
                        }
                        Thread.Sleep(500);
                    }
                    catch(SocketException e)
                    {
                        if (e.ErrorCode != 10035)
                            SupportiveFunctions.PrintText(e.ToString());
                    }
                    
                }
            }
            catch (ThreadAbortException e)
            {
                Trace.WriteLine("Abort: " + e.ToString());
            }
            catch(Exception e)
            {
                Trace.WriteLine("SocketException: " + e.ToString());
                SupportiveFunctions.PrintText(e.ToString());
            }
            finally
            {
                Dispose(true);
            }
        }

        private void ProcessConnection(object state)
        {
            ConnectionInfo connection = (ConnectionInfo)state;

            IPAddress remoteIP = IPAddress.Parse(connection.Socket.socket.RemoteEndPoint.ToString().Split(':')[0]);

            try
            {
                while (!MainForm.StopAllSockets)
                {
                    string content = string.Empty; 
                    int bytesRead = connection.Socket.ReciveString(ref content);

                    if (content == "ShutDown;<EOF>" || bytesRead < 0)
                    #region ShutDown
                    {
                        connection.Socket.Close();
                        AcceptedClients.Remove(connection);
                        return;
                    }
                    #endregion
                    else if (content.StartsWith("GetPorts") && content.EndsWith("<EOF>"))
                    #region Sending Opened Listening Ports
                    {
                        SupportiveFunctions.Splitter();
                        SupportiveFunctions.PrintText("Запрос открытых портов : ");
                        string data = "Ports;TCP;";

                        foreach (Int32 port in TCPPorts)
                            data += port.ToString() + ";";
                        data += "UDP;";

                        foreach (Int32 port in UDPPorts)
                            data += port.ToString() + ";";

                        connection.Socket.SendString(data + "<EOF>");
                        SupportiveFunctions.PrintText("Отсылка открытых портов : ", data + "<EOF>");
                    }
                    #endregion
                    else if (content.StartsWith("AddPorts") && content.EndsWith("<EOF>"))
                    #region Adding Listening Ports
                    {   
                        int tPort = 0;
                        string[] ports = content.Split(';');

                        ProtocolType pType = ProtocolType.Tcp;
                        string TCPOpened = "Opened;TCP;";
                        string UDPOpened = "UDP;";
                        string TCPNotOpened = "NotOpened;TCP;";
                        string UDPNotOpened = "UDP;";

                        SupportiveFunctions.Splitter();
                        SupportiveFunctions.PrintText("Принят запрос на открытие портов : ", content);

                        for (int i = 1; i < ports.Length; i++)
                        {
                            if (ports[i] == "UDP")
                            {
                                pType = ProtocolType.Udp;
                                continue;
                            }
                            else if (ports[i] == "TCP")
                            {
                                pType = ProtocolType.Tcp;
                                continue;
                            }
                            else if (ports[i] == "<EOF>")
                                break;

                            if (int.TryParse(ports[i], out tPort) && tPort > 0)
                            {
                                try
                                {
                                    ListeningDoneEvents.Add(new ManualResetEvent(false));
                                    ListeningSocket LSS = new ListeningSocket(new IPEndPoint(ServerIP, tPort), pType, ListeningDoneEvents[ListeningDoneEvents.Count - 1]);
                                    ListeningPorts.Add(LSS);
                                    ThreadPool.QueueUserWorkItem(LSS.ThreadPoolCallback, ListeningDoneEvents.Count - 1);

                                    int timer = 0;
                                    while (LSS.Result != "ready" && LSS.Result != "exited" && !LSS.Result.StartsWith("error") && timer++ < 2000 && !MainForm.StopAllSockets)
                                    {
                                        Thread.Sleep(100);
                                    }

                                    if (LSS.Result == "ready")
                                    {
                                        if (pType == ProtocolType.Tcp)
                                        {
                                            TCPOpened += tPort.ToString() + ";";
                                            TCPPorts.Add(tPort);
                                        }
                                        else if (pType == ProtocolType.Udp)
                                        {
                                            UDPOpened += tPort.ToString() + ";";
                                            UDPPorts.Add(tPort);
                                        }
                                        SupportiveFunctions.PrintText(pType.ToString() + " Порт # " + tPort.ToString() + " открыт");
                                    }
                                    else
                                    {
                                        if (pType == ProtocolType.Tcp)
                                            TCPNotOpened += tPort.ToString() + ";";
                                        else if (pType == ProtocolType.Udp)
                                            UDPNotOpened += tPort.ToString() + ";";

                                        if (LSS.Result == "exited")
                                            SupportiveFunctions.PrintText(pType.ToString() + " Порт # " + tPort.ToString() + " не открыт");
                                        else
                                            SupportiveFunctions.PrintText(pType.ToString() + " Порт # " + tPort.ToString() + " не открыт," + LSS.Result);
                                    }
                                }
                                catch (Exception e)
                                {
                                    if (pType == ProtocolType.Tcp)
                                        TCPNotOpened += tPort.ToString() + ";";
                                    else if (pType == ProtocolType.Udp)
                                        UDPNotOpened += tPort.ToString() + ";";

                                    SupportiveFunctions.PrintText(e.ToString());
                                }
                            }
                        }

                        connection.Socket.SendString("PortsOpeningResults;" + TCPOpened + UDPOpened + TCPNotOpened + UDPNotOpened + "<EOF>");
                        SupportiveFunctions.PrintText("Результат открытия : ", TCPOpened + UDPOpened + TCPNotOpened + UDPNotOpened + "<EOF>");
                        Trace.WriteLine("SEVRER: PortsOpeningResults: " + TCPOpened + UDPOpened + TCPNotOpened + UDPNotOpened + Environment.NewLine);

                    }
                    #endregion
                    else if (content.StartsWith("PingMyPorts;"))
                    #region Pinging Clinet`s Ports
                    {
                        SupportiveFunctions.Splitter();
                        SupportiveFunctions.PrintText("Принят запрос на открытие пингующих сокетов");
                        List<PingingSocket> PingingPorts = new List<PingingSocket> { };
                        List<ManualResetEvent> PingDoneEvents = new List<ManualResetEvent> { };

                        string[] ports = content.Split(';');
                        ProtocolType pType = 0;
                        int i = 1;

                        string TCPOpened = "Opened;TCP;";
                        string UDPOpened = "UDP;";
                        string TCPNotOpened = "NotOpened;TCP;";
                        string UDPNotOpened = "UDP;";                         

                        for (; i < ports.Length; i++)
                        {
                            try
                            {
                                if (ports[i] == "UDP")
                                {
                                    pType = ProtocolType.Udp;
                                    continue;
                                }
                                if (ports[i] == "TCP")
                                {
                                    pType = ProtocolType.Tcp;
                                    continue;
                                }
                                else if (ports[i] == "<EOF>")
                                    break;
                                
                                PingDoneEvents.Add(new ManualResetEvent(false));
                                PingingSocket pingingSocket = new PingingSocket(new IPEndPoint(remoteIP, int.Parse(ports[i])), new IPEndPoint(ServerIP, int.Parse(ports[i])), pType, PingDoneEvents[PingDoneEvents.Count - 1]);
                                PingingPorts.Add(pingingSocket);
                                ThreadPool.QueueUserWorkItem(pingingSocket.ThreadPoolCallback, PingDoneEvents.Count - 1);
                            }
                            catch (Exception e)
                            {
                                if (pType == ProtocolType.Tcp)
                                    TCPNotOpened += ports[i].ToString() + ";";
                                else if (pType == ProtocolType.Udp)
                                    UDPNotOpened += ports[i].ToString() + ";";

                                SupportiveFunctions.PrintText(e.ToString());
                            }
                        }

                        ManualResetEvent[] PingTempDoneEvents = PingDoneEvents.ToArray();
                        WaitHandle.WaitAll(PingTempDoneEvents);

                        foreach (PingingSocket pingingSocket in PingingPorts)
                        {
                            if (pingingSocket.Result == "true")
                            {
                                SupportiveFunctions.PrintText(pingingSocket.Type.ToString() + " Порт #" + pingingSocket.Port.ToString() + " открыт для пинга ->");

                                if (pingingSocket.Type == ProtocolType.Tcp)
                                    TCPOpened += pingingSocket.Port.ToString() + ";";
                                else if (pingingSocket.Type == ProtocolType.Udp)
                                    UDPOpened += pingingSocket.Port.ToString() + ";";
                            }
                            else
                            {
                                if (pingingSocket.Result == "false")
                                    SupportiveFunctions.PrintText(pingingSocket.Type.ToString() + " Порт #" + pingingSocket.Port.ToString() + " не открыт для пинга ->");
                                else
                                    SupportiveFunctions.PrintText(pingingSocket.Type.ToString() + " Порт #" + pingingSocket.Port.ToString() + " не открыт для пинга ->. Ошибка: " + pingingSocket.Result);

                                if (pingingSocket.Type == ProtocolType.Tcp)
                                    TCPNotOpened += pingingSocket.Port.ToString() + ";";
                                else if (pingingSocket.Type == ProtocolType.Udp)
                                    UDPNotOpened += pingingSocket.Port.ToString() + ";";
                            }

                            pingingSocket.Dispose();
                        }
                        PingingPorts.Clear();

                        connection.Socket.SendString("PingingPortsOpeningResults;" + TCPOpened + UDPOpened + TCPNotOpened + UDPNotOpened + "<EOF>");
                        SupportiveFunctions.PrintText("Результат открытия пингующих сокетов: ", TCPOpened + UDPOpened + TCPNotOpened + UDPNotOpened + "<EOF>");
                        Trace.WriteLine("SEVRER: ListeningPortsOpeningResults: " + TCPOpened + UDPOpened + TCPNotOpened + UDPNotOpened + Environment.NewLine);
                    }
                    #endregion
                    else
                        Thread.Sleep(500);


                }
            }
            catch (Exception e)
            {
                SupportiveFunctions.PrintText(e.ToString());
            }
            finally
            {
                if (connection.Socket != null)
                {                      
                    connection.Socket.Close();
                    connection.Socket = null;
                }
                lock (AcceptedClients) { AcceptedClients.Remove(connection); }
            }
        }           

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                //foreach (ListeningClientSocket soc in PingingPorts)
                //{
                //    soc.Shutdown(SocketShutdown.Both);
                //    soc.Close();
                //}

                lock (AcceptedClients)
                {
                    foreach (ConnectionInfo conn in AcceptedClients)
                    {
                        if (conn.Socket != null)
                            conn.Socket.Close();
                        conn.Socket = null;
                        SupportiveFunctions.PrintText("Закрытие активных соединений");
                        SupportiveFunctions.PrintText("Thread 4 AcceptedClients closed");
                    }  
                    AcceptedClients.Clear();
                }

                if(optionPort!=null)
                {
                    //optionPort.Shutdown(SocketShutdown.Both);
                    //optionPort.Disconnect(false);
                    optionPort.Close();
                    optionPort = null;

                    SupportiveFunctions.PrintText("Закрытие активных соединений");
                }

            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }    
}
