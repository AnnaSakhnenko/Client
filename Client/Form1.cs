using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        Random r = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //SendMessageFromSocket(11000, textBox1.Text + "|" + textBox2.Text + "|" + textBox3.Text);
                for (int i = 0; i < 15; i++)
                {
                    System.Threading.Thread newThread = new System.Threading.Thread(F);
                    newThread.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void F()
        {
            try
            {
                SendMessageFromSocket(11000, textBox1.Text + "|" + textBox2.Text + "|" + textBox3.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        static void SendMessageFromSocket(int port, string message)
        {
            byte[] bytes = new byte[1024];

            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(ipEndPoint);

            byte[] msg = Encoding.UTF8.GetBytes(message);

            int bytesSent = sender.Send(msg);

            int bytesRec = sender.Receive(bytes);
            //MessageBox.Show(Encoding.UTF8.GetString(bytes, 0, bytesRec));

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
