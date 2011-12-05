using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ping
{       
    static class SupportiveFunctions
    {
        public static RichTextBox output;
        public enum States
        {
            OpenedToListen = 0,
            OpenedToPing = 1,
            PingedTo = 2,
            PingedFrom = 3,
        }

        public static void Splitter()
        {
            output.Invoke((MethodInvoker)delegate()
            {
                output.Text += Environment.NewLine + "##############################################################################" + Environment.NewLine;
                output.SelectionStart = output.Text.Length;
                output.ScrollToCaret();   
                
            });
        }

        public static void PrintText(string text)
        {
            PrintText(text, "");
        }

        public static void PrintText(string text, string dynamicText)
        {
            DateTime time = DateTime.Now;
            string[] splited = dynamicText.Split(';');
            output.Invoke((MethodInvoker)delegate()
            {
                output.Text += time.Hour.ToString() + ":" + time.Minute.ToString() + ":" + time.Second.ToString() + " " + text + Environment.NewLine; ;

                if (dynamicText.Length > 0)
                {
                    output.Text += "    ###################################";
                    foreach (string ss in splited)
                    {
                        output.Text += Environment.NewLine + "    " + ss;
                    }
                    output.Text += Environment.NewLine + "    ###################################" + Environment.NewLine;
                }
                output.SelectionStart = output.Text.Length;
                output.ScrollToCaret();
            });
        }

        public static MySocket[] toArray(string content, char sep, States state)
        {
            return toArray(content.Split(sep), state);
        }
        public static MySocket[] toArray(string[] ports, States state)
        {
            List<MySocket> sockets = new List<MySocket> { };
            ProtocolType pType = 0;
            bool HaveOpened = false;

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
                else if (ports[i] == "NotOpened")
                {
                    HaveOpened = false;
                    continue;
                }
                else if (ports[i] == "Opened")
                {
                    HaveOpened = true;
                    continue;
                }
                else if (ports[i] == "<EOF>")
                    break;

                try 
                {
                    if (pType == ProtocolType.Tcp)
                        switch (state)
                        {
                            case States.OpenedToListen:
                                sockets.Add(new TcpSocket(int.Parse(ports[i]), HaveOpened));
                                break;
                            case States.OpenedToPing:
                                sockets.Add(new TcpSocket(int.Parse(ports[i]), HaveOpened));
                                break;
                            case States.PingedFrom:
                                sockets.Add(new TcpSocket(int.Parse(ports[i]), HaveOpened));
                                break;
                            case States.PingedTo:
                                sockets.Add(new TcpSocket(int.Parse(ports[i]), HaveOpened));
                                break;
                        }
                    else if (pType == ProtocolType.Udp)
                        switch (state)
                        {
                            case States.OpenedToListen:
                                sockets.Add(new UdpSocket(int.Parse(ports[i]), HaveOpened));
                                break;
                            case States.OpenedToPing:
                                sockets.Add(new UdpSocket(int.Parse(ports[i]), HaveOpened));
                                break;
                            case States.PingedFrom:
                                sockets.Add(new UdpSocket(int.Parse(ports[i]), HaveOpened));
                                break;
                            case States.PingedTo:
                                sockets.Add(new UdpSocket(int.Parse(ports[i]), HaveOpened));
                                break;
                        }
                }
                catch { }
            }

            return sockets.ToArray();
        }

        public static MySocket[] ChangeState(string content, char sep, ref MySocket[] sockets, States state)
        {
            return ChangeState(content.Split(sep), ref sockets, state);
        }
        public static MySocket[] ChangeState(string[] ports, ref MySocket[] sockets, States state)
        {            
            ProtocolType pType = 0;
            bool HaveOpened = false;

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
                else if (ports[i] == "NotOpened")
                {
                    HaveOpened = false;
                    continue;
                }
                else if (ports[i] == "Opened")
                {
                    HaveOpened = true;
                    continue;
                }
                else if (ports[i] == "<EOF>")
                    break;

                try 
                {
                    switch (state)
                    {
                        case States.OpenedToListen:
                            sockets[findSocket(int.Parse(ports[i]), pType, sockets)].OpenedToListen = HaveOpened;
                            break;
                        case States.OpenedToPing:
                            sockets[findSocket(int.Parse(ports[i]), pType, sockets)].OpenedToPing = HaveOpened;
                            break;
                        case States.PingedFrom:
                            sockets[findSocket(int.Parse(ports[i]), pType, sockets)].PingedFrom = HaveOpened;
                            break;
                        case States.PingedTo:
                            sockets[findSocket(int.Parse(ports[i]), pType, sockets)].PingedTo = HaveOpened;
                            break;
                    }                                      
                }
                catch { }
            }

            return sockets;
        }

        public static string toString(List<MySocket> sockets, bool onlyList)
        {            
            string TCPOpened = "Opened;TCP;";
            string UDPOpened = "UDP;";
            string TCPNotOpened = "NotOpened;TCP;";
            string UDPNotOpened = "UDP;";

            if (onlyList)
            {
                TCPNotOpened = String.Empty;
                UDPNotOpened = String.Empty;
            }

            foreach (MySocket sock in sockets)
            {
                if(onlyList)
                    if (sock.Type == ProtocolType.Tcp)
                        TCPOpened += sock.Port.ToString() + ";";
                    else if (sock.Type == ProtocolType.Udp)
                        UDPOpened += sock.Port.ToString() + ";";
                else if (sock.OpenedToListen)
                {
                    if (sock.Type == ProtocolType.Tcp)
                        TCPOpened += sock.Port.ToString() + ";";
                    else if (sock.Type == ProtocolType.Udp)
                        UDPOpened += sock.Port.ToString() + ";";
                }
                else if (!sock.OpenedToListen)
                {
                    if (sock.Type == ProtocolType.Tcp)
                        TCPNotOpened += sock.Port.ToString() + ";";
                    else if (sock.Type == ProtocolType.Udp)
                        UDPNotOpened += sock.Port.ToString() + ";";
                }
            }

            return TCPOpened + UDPOpened + TCPNotOpened + UDPNotOpened;
        }

        public static int findSocket(int port, ProtocolType type, MySocket[] sockets)
        {
            for (int i = 0; i < sockets.Length; i++)
                if (sockets[i].Port == port && sockets[i].Type == type)
                    return i;
            return -1;
        }

        public static int getPort(EndPoint ip)
        {
            return int.Parse(ip.ToString().Split(':')[1]);
        }
    }
}
