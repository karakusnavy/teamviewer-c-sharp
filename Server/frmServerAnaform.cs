using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
    struct DEVMODE
    {
        public const int CCHDEVICENAME = 32;
        public const int CCHFORMNAME = 32;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        [System.Runtime.InteropServices.FieldOffset(0)]
        public string dmDeviceName;
        [System.Runtime.InteropServices.FieldOffset(32)]
        public Int16 dmSpecVersion;
        [System.Runtime.InteropServices.FieldOffset(34)]
        public Int16 dmDriverVersion;
        [System.Runtime.InteropServices.FieldOffset(36)]
        public Int16 dmSize;
        [System.Runtime.InteropServices.FieldOffset(38)]
        public Int16 dmDriverExtra;
        [System.Runtime.InteropServices.FieldOffset(40)]
        public DM dmFields;

        [System.Runtime.InteropServices.FieldOffset(44)]
        Int16 dmOrientation;
        [System.Runtime.InteropServices.FieldOffset(46)]
        Int16 dmPaperSize;
        [System.Runtime.InteropServices.FieldOffset(48)]
        Int16 dmPaperLength;
        [System.Runtime.InteropServices.FieldOffset(50)]
        Int16 dmPaperWidth;
        [System.Runtime.InteropServices.FieldOffset(52)]
        Int16 dmScale;
        [System.Runtime.InteropServices.FieldOffset(54)]
        Int16 dmCopies;
        [System.Runtime.InteropServices.FieldOffset(56)]
        Int16 dmDefaultSource;
        [System.Runtime.InteropServices.FieldOffset(58)]
        Int16 dmPrintQuality;

        [System.Runtime.InteropServices.FieldOffset(44)]
        public POINTL dmPosition;
        [System.Runtime.InteropServices.FieldOffset(52)]
        public Int32 dmDisplayOrientation;
        [System.Runtime.InteropServices.FieldOffset(56)]
        public Int32 dmDisplayFixedOutput;

        [System.Runtime.InteropServices.FieldOffset(60)]
        public short dmColor;
        [System.Runtime.InteropServices.FieldOffset(62)]
        public short dmDuplex;
        [System.Runtime.InteropServices.FieldOffset(64)]
        public short dmYResolution;
        [System.Runtime.InteropServices.FieldOffset(66)]
        public short dmTTOption;
        [System.Runtime.InteropServices.FieldOffset(68)]
        public short dmCollate;
        [System.Runtime.InteropServices.FieldOffset(72)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        public string dmFormName;
        [System.Runtime.InteropServices.FieldOffset(102)]
        public Int16 dmLogPixels;
        [System.Runtime.InteropServices.FieldOffset(104)]
        public Int32 dmBitsPerPel;
        [System.Runtime.InteropServices.FieldOffset(108)]
        public Int32 dmPelsWidth;
        [System.Runtime.InteropServices.FieldOffset(112)]
        public Int32 dmPelsHeight;
        [System.Runtime.InteropServices.FieldOffset(116)]
        public Int32 dmDisplayFlags;
        [System.Runtime.InteropServices.FieldOffset(116)]
        public Int32 dmNup;
        [System.Runtime.InteropServices.FieldOffset(120)]
        public Int32 dmDisplayFrequency;
    }

    struct POINTL
    {
        public Int32 x;
        public Int32 y;
    }

    [Flags()]
    enum DM : int
    {
        Orientation = 0x1,
        PaperSize = 0x2,
        PaperLength = 0x4,
        PaperWidth = 0x8,
        Scale = 0x10,
        Position = 0x20,
        NUP = 0x40,
        DisplayOrientation = 0x80,
        Copies = 0x100,
        DefaultSource = 0x200,
        PrintQuality = 0x400,
        Color = 0x800,
        Duplex = 0x1000,
        YResolution = 0x2000,
        TTOption = 0x4000,
        Collate = 0x8000,
        FormName = 0x10000,
        LogPixels = 0x20000,
        BitsPerPixel = 0x40000,
        PelsWidth = 0x80000,
        PelsHeight = 0x100000,
        DisplayFlags = 0x200000,
        DisplayFrequency = 0x400000,
        ICMMethod = 0x800000,
        ICMIntent = 0x1000000,
        MediaType = 0x2000000,
        DitherType = 0x4000000,
        PanningWidth = 0x8000000,
        PanningHeight = 0x10000000,
        DisplayFixedOutput = 0x20000000
    }

    [Flags]
    public enum MouseEventFlags : uint
    {
        LEFTDOWN = 0x00000002,
        LEFTUP = 0x00000004,
        MIDDLEDOWN = 0x00000020,
        MIDDLEUP = 0x00000040,
        MOVE = 0x00000001,
        ABSOLUTE = 0x00008000,
        RIGHTDOWN = 0x00000008,
        RIGHTUP = 0x00000010,
        WHEEL = 0x00000800,
        XDOWN = 0x00000080,
        XUP = 0x00000100
    }

    public partial class frmServerAnaform : Form
    {

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
           UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData,
           UIntPtr dwExtraInfo);

        const int KEYEVENTF_KEYUP = 0x2;
        const int KEYEVENTF_EXTENDEDKEY = 0x1;

        Socket mouse_klavyeDinleme = null;
        byte[] dizi = new byte[200];
        delegate void ResimGonderHandler();
        string KarsiIP = "";

        public frmServerAnaform()
        {
            InitializeComponent();
        }

        private void frmServerAnaform_Load(object sender, EventArgs e)
        {
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostByName(host);
            lblIP.Text = ip.AddressList[0].ToString();
            mouse_klavyeDinleme = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mouse_klavyeDinleme.Bind(new IPEndPoint(IPAddress.Parse(ip.AddressList[0].ToString()), 1453));
            mouse_klavyeDinleme.Listen(1);
            mouse_klavyeDinleme.BeginAccept(new AsyncCallback(Baglandiginda), null);
        }

        void Baglandiginda(IAsyncResult iar)
        {
            Socket soket = mouse_klavyeDinleme.EndAccept(iar);
            soket.BeginReceive(dizi, 0, dizi.Length, SocketFlags.None, new AsyncCallback(VeriGeldiginde), soket);
            mouse_klavyeDinleme.BeginAccept(new AsyncCallback(Baglandiginda), null);
        }

        void VeriGeldiginde(IAsyncResult iar)
        {
            Socket soket = (Socket)iar.AsyncState;
            int gelenVeriUzunluk = soket.EndReceive(iar);
            byte[] veri = new byte[gelenVeriUzunluk];
            Array.Copy(dizi, veri, veri.Length);
            string islenecek = Encoding.UTF8.GetString(veri);
            if (islenecek.Contains("AC"))
            {
                KarsiIP = islenecek.Substring(0, islenecek.IndexOf('|'));
                DEVMODE dv = new DEVMODE();
                EnumDisplaySettings(null, -1, ref dv);
                string gonderilcek = dv.dmPelsWidth.ToString() + ":" + dv.dmPelsHeight.ToString() + "|";
                ekranCozunurlukGonder(gonderilcek);
                ResimGonderHandler resimGonder = new ResimGonderHandler(ResimGonder);
                resimGonder.BeginInvoke(new AsyncCallback(islemsonlandi), null);
            }
            else
            {
                if (islenecek.Contains("MouseMove"))
                {
                    int x = int.Parse(islenecek.Substring(0, islenecek.IndexOf(':')));
                    int y = int.Parse(islenecek.Substring(islenecek.IndexOf(':') + 1, islenecek.IndexOf('|') - islenecek.IndexOf(':') - 1));
                    Cursor.Position = new Point(x, y);
                }
                else if (islenecek.Contains("MouseDown"))
                {
                    string a = islenecek.Substring(0, islenecek.IndexOf(':'));
                    if (a == "Left")
                        mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
                    else
                        mouse_event((uint)MouseEventFlags.RIGHTDOWN, 0, 0, 0, UIntPtr.Zero);
                }
                else if (islenecek.Contains("MouseUp"))
                {
                    string a = islenecek.Substring(0, islenecek.IndexOf(':'));
                    if (a == "Left")
                        mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, UIntPtr.Zero);
                    else
                        mouse_event((uint)MouseEventFlags.RIGHTUP, 0, 0, 0, UIntPtr.Zero);
                }
                else if(islenecek.Contains("Key"))
                {
                    
                    byte tusKodu = byte.Parse(islenecek.Substring(0, islenecek.IndexOf(':')));
                    keybd_event(tusKodu, 0x45, KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                }
            }
        }

        void ekranCozunurlukGonder(string gonderilcek)
        {
            Socket baglan = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            baglan.Connect(IPAddress.Parse(KarsiIP), 1453);
            byte[] coz = Encoding.UTF8.GetBytes(gonderilcek);
            baglan.Send(coz, 0, coz.Length, SocketFlags.None);
            baglan.Close();
        }

        void ResimGonder()
        {
            while (true)
            {
                Socket baglan = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                baglan.Connect(IPAddress.Parse(KarsiIP), 1453);
                byte[] ekran = EkranGoruntusu();
                baglan.Send(ekran, 0, ekran.Length, SocketFlags.None);
                baglan.Close();
            }

        }

        byte[] EkranGoruntusu()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics gr = Graphics.FromImage(bmp);
            gr.CopyFromScreen(0, 0, 0, 0, new Size(bmp.Width, bmp.Height));
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            return ms.GetBuffer();
        }

        void islemsonlandi(IAsyncResult iar)
        {}

        private void button1_Click(object sender, EventArgs e)
        {
            this.Opacity = 0;
            this.WindowState = FormWindowState.Minimized;

        }
    }
}
