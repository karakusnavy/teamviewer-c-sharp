using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class frmKarsiPc : Form
    {
        Socket resimAl = null;
        byte[] dizi = new byte[9999999];
        string karsiIP = "";
        public frmKarsiPc(string IP)
        {
            karsiIP = IP;
            InitializeComponent();
        }

        private void frmKarsiPc_Load(object sender, EventArgs e)
        {
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostByName(host);
            resimAl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            resimAl.Bind(new IPEndPoint(IPAddress.Parse(ip.AddressList[0].ToString()), 1453));
            resimAl.Listen(1);
            resimAl.BeginAccept(new AsyncCallback(Baglandiginda), null);
            ilkGonderme(ip.AddressList[0].ToString());
        }

        void Baglandiginda(IAsyncResult iar)
        {
            Socket soket = resimAl.EndAccept(iar);
            soket.BeginReceive(dizi, 0, dizi.Length, SocketFlags.None, new AsyncCallback(veriGeldiginde), soket);
            resimAl.BeginAccept(new AsyncCallback(Baglandiginda), null);
        }

        void veriGeldiginde(IAsyncResult iar)
        {
            Socket soket = (Socket)iar.AsyncState;
            int uzunluk = soket.EndReceive(iar);
            byte[] veri = new byte[uzunluk];
            Array.Copy(dizi, veri, veri.Length);
            if (uzunluk < 50)
            {
                string gelen = Encoding.UTF8.GetString(veri);
                int width = int.Parse(gelen.Substring(0, gelen.IndexOf(':')));
                int height = int.Parse(gelen.Substring(gelen.IndexOf(':') + 1, gelen.IndexOf('|') - gelen.IndexOf(':') - 1));
                this.Size = new Size(width + 16, height + 38);
            }
            else
            {
                MemoryStream ms = new MemoryStream(veri);
                Image resim = Bitmap.FromStream(ms);
                pbResim.BackgroundImage = resim;
            }
        }

        void ilkGonderme(string IP)
        {
            Socket soket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soket.Connect(IPAddress.Parse(karsiIP), 1453);
            byte[] gonderilcek = Encoding.UTF8.GetBytes(IP + "|AC");
            soket.Send(gonderilcek);
            soket.Close();
        }

        private void pbResim_MouseDown(object sender, MouseEventArgs e)
        {
            byte[] gonderilcek = null;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                gonderilcek = Encoding.UTF8.GetBytes("Left:MouseDown");

            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                gonderilcek = Encoding.UTF8.GetBytes("Right:MouseDown");

            else
                return;
            Socket soket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soket.Connect(IPAddress.Parse(karsiIP), 1453);
            soket.Send(gonderilcek);
            soket.Close();
        }

        private void pbResim_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            Socket soket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soket.Connect(IPAddress.Parse(karsiIP), 1453);
            byte[] gonderilcek = Encoding.UTF8.GetBytes(x.ToString() + ":" + y.ToString() + "|MouseMove");
            soket.Send(gonderilcek);
            soket.Close();
        }

        private void pbResim_MouseUp(object sender, MouseEventArgs e)
        {
            byte[] gonderilcek = null;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                gonderilcek = Encoding.UTF8.GetBytes("Left:MouseUp");

            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                gonderilcek = Encoding.UTF8.GetBytes("Right:MouseUp");

            else
                return;
            Socket soket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soket.Connect(IPAddress.Parse(karsiIP), 1453);
            soket.Send(gonderilcek);
            soket.Close();
        }

        private void frmKarsiPc_KeyUp(object sender, KeyEventArgs e)
        {
            this.Text = e.KeyValue.ToString();
            Socket soket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soket.Connect(IPAddress.Parse(karsiIP), 1453);
            byte[] gonderilcek = Encoding.UTF8.GetBytes(e.KeyValue.ToString() + ":Key");
            soket.Send(gonderilcek);
            soket.Close();
        }

        private void pbResim_Click(object sender, EventArgs e)
        {

        }
    }
}
