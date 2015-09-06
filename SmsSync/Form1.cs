using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SmsSync
{
    public partial class Form1 : Form
    {
        private Thread _threadServer ;
        private UdpClient _udpPacket;
        private bool _run;

        public Form1()
        {
            InitializeComponent();
            _run = true;
            _threadServer = new Thread(CreateUdp);

        }

        private void CreateUdp()
        {
            _udpPacket = new UdpClient(ConstantValue.PORT);
            _udpPacket.EnableBroadcast = true;
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            IPAddress from_addr = null;
            while (_run)
            {
                try
                {
                    var receiveBytes = _udpPacket.Receive(ref remoteIpEndPoint);
                    string returnData = Encoding.UTF8.GetString(receiveBytes);
                    from_addr = remoteIpEndPoint.Address;
                    MessageBox.Show(returnData + " " + from_addr);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _threadServer.Start();
        }
    }
}
