using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace Ping
{
    public class AClient
    {
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);
        private Form1 myForm;
        private List<AServer> servers = new List<AServer> { };
        private bool createServer;

        private String response = String.Empty;

        public void StartClient(List<int> ports,string ip,Form1 form,bool  type)
        {
            myForm = form;
            createServer = type;
            IPHostEntry ipHostInfo = Dns.Resolve(ip);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            foreach (int port in ports)
            {
                try
                {     
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                    Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);

                Send(client, "1234567876543210<EOF>");
            }
            catch (Exception e)
            {
                myForm.resultView.Text += "Ошибка: " + e + Environment.NewLine;
                myForm.resultView.Text += "==========================" + Environment.NewLine;
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                myForm.resultView.Text += "Ошибка: " + e + Environment.NewLine;
                myForm.resultView.Text += "==========================" + Environment.NewLine;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            try
            {
                
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    string data = state.sb.ToString();
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        data = data.Substring(0, data.IndexOf("<EOF>") + 5);
                        if (data == "1234567876543210<EOF>")
                            myForm.resultView.Text += client.RemoteEndPoint.ToString() + " доступен" + Environment.NewLine;
                        else
                            myForm.resultView.Text += client.RemoteEndPoint.ToString() + " доступен,но ответ неправильный :" + data + Environment.NewLine;
                        
                        client.Shutdown(SocketShutdown.Both);
                        

                        if (createServer)
                        {
                            AServer server = new AServer();
                            string rp = client.RemoteEndPoint.ToString();
                            rp = rp.Substring(rp.IndexOf(":") + 1);
                            server.Start(new List<int> { int.Parse(rp) }, client.ProtocolType, myForm);
                        }
                        client.Close();
                    }
                    else
                        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);                
                }
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception e)
            {
                myForm.resultView.Text += "Ошибка: " + e + Environment.NewLine;
                myForm.resultView.Text += "==========================" + Environment.NewLine;              
            }
        }

        private void Send(Socket client, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                
                int bytesSent = client.EndSend(ar);                
                Receive(client);
            }
            catch (Exception e)
            {
                myForm.resultView.Text += "Ошибка: " + e + Environment.NewLine;
                myForm.resultView.Text += "==========================" + Environment.NewLine;
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }
    }
}