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

namespace client_impiccato
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string P = " ";
        public static int lettera = 0;
        public static int errore = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            byte[] bytes = new byte[1024];


            try
            {

                IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);


                Socket send = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);


                try
                {
                    send.Connect(remoteEP);

                    Console.WriteLine("$02 mi sono collegato al server : {0}",
                        send.RemoteEndPoint.ToString());


                    byte[] msg = Encoding.ASCII.GetBytes("<EOF>");


                    int bytesSent = send.Send(msg);


                    int bytesRec = send.Receive(bytes);
                 
                    P = Encoding.ASCII.GetString(bytes, 0, bytesRec);



                    send.Shutdown(SocketShutdown.Both);
                    send.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception g)
                {
                    Console.WriteLine("Unexpected exception : {0}", g.ToString());
                }

            }
            catch (Exception s)
            {
                Console.WriteLine(s.ToString());
            }


            int lunghezza = P.Length;
            Label[] labels = new Label[] { l1, l2, l3, l4, l5, l6, l7, l8, l9, l10 };
            foreach (Label label in labels)
            {
                label.Visible = false;
            }
            for (int i = 0; i < lunghezza; i++)
            {
                labels[i].Visible = true;
                labels[i].Text = "_";
            }
            labels[0].Text = P.Substring(0, 1);
            labels[lunghezza - 1].Text = P.Substring(lunghezza - 1);


            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Label[] labels = new Label[] { l1, l2, l3, l4, l5, l6, l7, l8, l9, l10 };
            
            //lettera = P.IndexOf(textBox3.Text);
            lettera = 0;
            label1.Text = " ";
            var foundIndexes = new List<int>();
            
            for (int i = 0; i < P.Length; i++)
            {
                if (P[i] == textBox3.Text[0])
                    foundIndexes.Add(i);
            }

            foreach (int i in foundIndexes)
            {
               
                    // MessageBox.Show(lettera.ToString());
                    labels[i].Text = textBox3.Text;
                   
            }

            if(foundIndexes.Count() == 0)
            {
                
                label1.Text = "Lettera non presente";
                errore=errore+1;
                label3.Text = errore.ToString();
                if (errore == 1)
                {
                    pictureBox1.Visible = true;
                }
                if (errore == 2)
                {
                    pictureBox1.Visible = false;
                    pictureBox3.Visible = true;
                }
                if (errore == 3)
                {
                    pictureBox1.Visible = false;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = true;
                    MessageBox.Show("partita terminata hai perso");
                    this.Close();
                }
               
                    
                }
            }
            

            /*if (lettera > 0)
            {
                i = lettera;
                // MessageBox.Show(lettera.ToString());
                labels[lettera].Text = textBox3.Text;
                textBox3.Text = "";
            }
            else
            {
                textBox3.Text = "Lettera non presente";
            }*/



    

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }    
    }
   }

