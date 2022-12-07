using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Socket handler;
        private void button1_Click(object sender, EventArgs e)
        {
            string parola = " ";
            byte[] bytes = new Byte[1024];


            IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("$01 in attesa di connessione...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    Console.WriteLine("$100 connessione effettuata");


                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        parola += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (parola.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }


                    //  Console.WriteLine("Text received : {0}", parola);

                    parola = textBox1.Text;

                    int i = parola.Length;
                    if (i > 10)
                    {
                        MessageBox.Show(i.ToString());
                        label2.Text = "parola inserita non valida";
                        textBox1.Text = " ";
                    }
                    else
                    {



                        // Echo the data back to the client.  
                        byte[] msg = Encoding.ASCII.GetBytes(parola);

                        handler.Send(msg);
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }

            }
            catch (Exception a)
            {
                Console.WriteLine(a.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
