using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Ping
{
    class AClient : IDisposable
    {
        private MainForm myForm;
        //private ManualResetEvent ConfigurationDone = new ManualResetEvent(false);

        MySocket[] AllSockets;

        private List<PingingSocket> PingingPorts = new List<PingingSocket> { };
        private List<ManualResetEvent> PingDoneEvents = new List<ManualResetEvent> { };

        private List<ListeningSocket> ListeningPorts = new List<ListeningSocket> { };
        private List<ManualResetEvent> ListeningDoneEvents = new List<ManualResetEvent> { };

        private List<MySocket> sockets = new List<MySocket> { };
        private TcpSocket optionPort = null;
                                                     

        public void StartClient(MySocket[] ports, string ip, MainForm form, int commandPort)
        {
            try
            {
                if (ports == null)
                    throw new ArgumentNullException("ports", "No one port assigned");
                else if (ip == null)
                    throw new ArgumentNullException("ip", "No ip assigned");
                else if (form == null)
                    throw new ArgumentNullException("form", "No main form assigned");
                else if (commandPort < 0)
                    throw new ArgumentNullException("commandPort", "No Command Port assigned");

                bool anySuccess = false;
                MySocket[] portsOnServer = null;                 
                string content = string.Empty;
                IPAddress RemoteIP = IPAddress.Parse(ip);
                AllSockets = ports;

                SupportiveFunctions.output = form.Output;
                myForm = form;

                try
                {
                    SupportiveFunctions.Splitter();
                    SupportiveFunctions.PrintText("Подключаемся к серверу по порту настроек");
                    optionPort = new TcpSocket();
                    optionPort.ConnectTo(new IPEndPoint(RemoteIP, commandPort), new IPEndPoint(IPAddress.Any, 0));
                    IPAddress LocalIP = IPAddress.Parse(optionPort.socket.LocalEndPoint.ToString().Substring(0, optionPort.socket.LocalEndPoint.ToString().IndexOf(':')));
                    sockets.Add(optionPort);

                    //начало цикла повтора попыток открытия портов на сервере
                    //do
                    //{  

                    
                    {
                        #region getting Opened Ports on Server
                        //запрашиваем порты открытые на сервере
                        optionPort.SendString("GetPorts<EOF>");

                        SupportiveFunctions.Splitter();
                        SupportiveFunctions.PrintText("Запрашиваем порты на сервере.");

                        //Получаем их
                        optionPort.ReciveString(ref content);
                        
                        SupportiveFunctions.PrintText("Полученные данные: ", content);

                        //Обрабатываем полученные данные   
                        if (content.StartsWith("Ports;"))
                            portsOnServer = SupportiveFunctions.toArray(content, ';', SupportiveFunctions.States.OpenedToListen);

                        List<MySocket> portsToOpen = new List<MySocket> { };

                        foreach (MySocket sock in AllSockets)
                        {
                            //если порт не найден в списке, добавляем в список на открытие
                            if (SupportiveFunctions.findSocket(sock.Port, sock.Type, portsOnServer) < 0)
                                portsToOpen.Add(sock);
                            //если найден, обновляем значение OpenedToListen
                            else if (SupportiveFunctions.findSocket(sock.Port, sock.Type, AllSockets) >= 0)
                                AllSockets[SupportiveFunctions.findSocket(sock.Port, sock.Type, AllSockets)].OpenedToListen = true;
                        }
                        #endregion


                        //отправляем его если он не пуст
                        if (portsToOpen.Count != 0)
                        {
                            content = SupportiveFunctions.toString(portsToOpen, true);
                            content = content.Substring(7) + "<EOF>";

                            SupportiveFunctions.Splitter();
                            SupportiveFunctions.PrintText("Порты которые нужно открыть: ", content);

                            optionPort.SendString("AddPorts;" + content);

                            {
                                //получаем результат об открытие
                                optionPort.ReciveString(ref content);                                   

                                SupportiveFunctions.PrintText("Результат сервера: ", content);

                                //разбираем информацию                
                                if (content.StartsWith("PortsOpeningResults"))
                                    AllSockets = SupportiveFunctions.ChangeState(content, ';', ref AllSockets, SupportiveFunctions.States.OpenedToListen);

                            }
                        }
                        portsToOpen.Clear();  
                       
                        //составляем пул пингующих сокетов
                        foreach (MySocket sock in AllSockets)
                        {
                            if (sock.OpenedToListen)
                            {                                 
                                PingDoneEvents.Add(new ManualResetEvent(false));
                                PingingSocket LCS = new PingingSocket(new IPEndPoint(RemoteIP, sock.Port), new IPEndPoint(LocalIP, sock.Port), sock.Type, PingDoneEvents[PingDoneEvents.Count - 1]);
                                PingingPorts.Add(LCS);
                                ThreadPool.QueueUserWorkItem(LCS.ThreadPoolCallback, PingDoneEvents.Count - 1);
                            }
                        }
                        if (PingDoneEvents.Count > 0)
                        {
                            ManualResetEvent[] TempDoneEvents = PingDoneEvents.ToArray();
                            WaitHandle.WaitAll(TempDoneEvents);

                            SupportiveFunctions.Splitter();
                            // Display the results...
                            foreach (PingingSocket LCS in PingingPorts)
                            {
                                try
                                {
                                    if (LCS.Result == "true")
                                    {
                                        SupportiveFunctions.PrintText(LCS.Type.ToString() + " Порт #" + LCS.Port.ToString() + " доступен ->");
                                        AllSockets[SupportiveFunctions.findSocket(LCS.Port, LCS.Type, AllSockets)].PingedTo = true;
                                        anySuccess = true;
                                    }
                                    else
                                    {
                                        AllSockets[SupportiveFunctions.findSocket(LCS.Port, LCS.Type, AllSockets)].PingedTo = false;
                                        if (LCS.Result == "false")
                                            SupportiveFunctions.PrintText(LCS.Type.ToString() + " Порт #" + LCS.Port.ToString() + " недоступен ->");
                                        else
                                            SupportiveFunctions.PrintText(LCS.Type.ToString() + " Порт #" + LCS.Port.ToString() + " недоступен ->. Ошибка: " + LCS.Result);
                                    }
                                }
                                catch (Exception e)
                                {
                                    SupportiveFunctions.PrintText("", e.ToString());
                                }
                            }


                            if (!anySuccess)
                                SupportiveFunctions.PrintText("Извините, но у сервера нет доступных портов, либо он не смог открыть не один из требующихся");
                            else
                            {
                                #region Asking to Open Some if it neccessary
                                {
                                    bool anySucceses = false;
                                    //просим пропинговать клиентские порты                                    

                                    //составляем список tcp и udp портов
                                    string PingedTCPPorts = "PingMyPorts;TCP;";
                                    string PingedUDPPorts = "UDP;";

                                    SupportiveFunctions.Splitter();
                                    SupportiveFunctions.PrintText("Открываем серверные сокеты");                                    

                                    foreach (MySocket sock in AllSockets)
                                    {
                                        if (sock.PingedTo)
                                        {
                                            //составляем пул слушающих сокетов
                                            ListeningDoneEvents.Add(new ManualResetEvent(false));
                                            ListeningSocket LSS = new ListeningSocket(new IPEndPoint(LocalIP, sock.Port), sock.Type, ListeningDoneEvents[ListeningDoneEvents.Count - 1]);
                                            ListeningPorts.Add(LSS);
                                            ThreadPool.QueueUserWorkItem(LSS.ThreadPoolCallback, ListeningDoneEvents.Count - 1);

                                            int i = 0;
                                            while (LSS.Result != "ready" && LSS.Result != "exited" && !LSS.Result.StartsWith("error") && i++ < 2000 && !MainForm.StopAllSockets)
                                            {
                                                Thread.Sleep(100);
                                            }

                                            if (LSS.Result == "ready")
                                            {
                                                anySucceses = true;
                                                SupportiveFunctions.PrintText(LSS.Type.ToString() + " Порт #" + LSS.Port.ToString() + " открыт для прослушки <-");
                                                if (sock.Type == ProtocolType.Tcp)
                                                    PingedTCPPorts += sock.Port.ToString() + ";";
                                                else if (sock.Type == ProtocolType.Udp)
                                                    PingedUDPPorts += sock.Port.ToString() + ";";
                                                else
                                                    continue;
                                            }
                                            else if (LSS.Result == "exited")
                                                SupportiveFunctions.PrintText(LSS.Type.ToString() + " Порт #" + LSS.Port.ToString() + " не открыт для прослушки <-");
                                            else
                                                SupportiveFunctions.PrintText(LSS.Type.ToString() + " Порт #" + LSS.Port.ToString() + " не открыт для прослушки <-. Ошбика: " + LSS.Result);
                                        }
                                    }

                                    SupportiveFunctions.Splitter();
                                    if (!anySucceses)
                                        SupportiveFunctions.PrintText("", "Извините, но сервер не смог открыть порты чтобы пропинговать наши.");
                                    else
                                    {
                                        PingedUDPPorts += "<EOF>";

                                        //шлем их на сервер
                                        SupportiveFunctions.PrintText("Шлем на сервер какие нужно: ", PingedTCPPorts + PingedUDPPorts);

                                        optionPort.SendString(PingedTCPPorts + PingedUDPPorts);
                                    }

                                    {                                     
                                        //получаем результат об открытие
                                        optionPort.ReciveString(ref content);

                                        SupportiveFunctions.PrintText("Результат сервера: ", content);

                                        //разбираем информацию  
                                        if (content.StartsWith("PingingPortsOpeningResults"))
                                            AllSockets = SupportiveFunctions.ChangeState(content, ';', ref AllSockets, SupportiveFunctions.States.PingedFrom);

                                        //ManualResetEvent[] ListeningTempDoneEvents = ListeningDoneEvents.ToArray();
                                        //WaitHandle.WaitAll(ListeningTempDoneEvents);

                                        SupportiveFunctions.Splitter();
                                        foreach (MySocket mySocket in AllSockets)
                                        {
                                            if (mySocket.PingedFrom == true)
                                                SupportiveFunctions.PrintText(mySocket.Type.ToString() + " Порт " + mySocket.Port.ToString() + " доступен  со стороны сервера <-");
                                            else if (mySocket.PingedFrom == false)
                                                SupportiveFunctions.PrintText(mySocket.Type.ToString() + " Порт " + mySocket.Port.ToString() + " недоступен  со стороны сервера <-");
                                        }

                                        //закрываем слушающие порты
                                        foreach (ListeningSocket socket in ListeningPorts)
                                            socket.Dispose();
                                        ListeningPorts.Clear();
                                    }
                                }
                            }
                        }
                                #endregion
                    }
                }
                //    //повтторяем процесс пока сервер  не откроет все порты
                //    //} while (TCPports.Count != 0 || UDPports.Count != 0);  
                catch (Exception e)
                {
                    SupportiveFunctions.PrintText("", e.ToString());
                }
                finally
                {
                    if (optionPort != null)
                    {                         
                        optionPort.Close();
                        optionPort = null;
                    }
                    myForm.Stop();
                }
            }
            catch (Exception e)
            {
                SupportiveFunctions.PrintText("", e.ToString());
            }
            finally
            {
                if (optionPort != null)
                {                     
                    optionPort.Close();
                    optionPort = null;
                }
                myForm.Stop();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (MySocket soc in sockets)
                {                     
                    soc.Close();                     
                }
                sockets.Clear();

                if (optionPort != null)
                {                     
                    optionPort.Close();
                    optionPort = null;
                }
            }            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}