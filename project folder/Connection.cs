using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetAccess;

namespace FECipherVit
{
    public partial class Connection : Form
    {
        public Connection(FECipherVit _Owner)
        {
            InitializeComponent();
            Owner = _Owner;
        }
        new FECipherVit Owner;
        public bool connected = false;
        bool offline = false;
        private void Connection_Load(object sender, EventArgs e)
        {
            textBox_UserName.Text = AppConfig.GetValue("UserName");
        }

        private void buttonWaitConnect_Click(object sender, EventArgs e)
        {
            buttonConnect.Enabled = false;
            buttonWaitConnect.Enabled = false;
            button_Confirm.Enabled = false;
            textBoxIP.Enabled = false;
            textBox_UserName.Enabled = false;
            textBox_Port.Enabled = false;
            textBox_RivalPort.Enabled = false;
            Owner.socket = new ServerSocket();
            try
            {
                int port;
                
                if(!Int32.TryParse(textBox_Port.Text,out port))
                {
                    port = 9050;
                }
                Owner.socket.Access("", port, port, Owner.AccessAction);
            }
            catch (Exception ecp)
            {
                MessageBox.Show(ecp.Message, "错误");
            }
            labelConnectStatus.Text = "等待对方连接...";
            //Owner.UpdateGetMsgTextBox(Environment.NewLine + "Name" + " " + System.DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine + "##等待对方连接..." + Environment.NewLine);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            buttonConnect.Enabled = false;
            buttonWaitConnect.Enabled = false;
            button_Confirm.Enabled = false;
            textBoxIP.Enabled = false;
            textBox_UserName.Enabled = false;
            textBox_Port.Enabled = false;
            textBox_RivalPort.Enabled = false;
            Owner.socket = new ClientSocket();
            try
            {
                int port;
                int serverport;

                if (!Int32.TryParse(textBox_Port.Text, out port))
                {
                    port = 9051;
                }
                if (!Int32.TryParse(textBox_RivalPort.Text, out serverport))
                {
                    serverport = 9050;
                }
                Owner.socket.Access(textBoxIP.Text, serverport, port, Owner.AccessAction);
            }
            catch (Exception ecp)
            {
                MessageBox.Show(ecp.Message, "错误");
                System.Windows.Forms.Application.Restart();
            }
            labelConnectStatus.Text = "正在连接对方...";
            //UpdateGetMsgTextBox(Environment.NewLine + "Name" + " " + System.DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine + "##正在连接对方..." + Environment.NewLine);
        }

        private void button_Confirm_Click(object sender, EventArgs e)
        {
            offline = true;
            this.Close();
        }

        private void Connection_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!offline && !connected)
            {
                Owner.Close();
            }
            else
            {
                AppConfig.SetValue("UserName", textBox_UserName.Text);
                Owner.PlayerName = textBox_UserName.Text; 
            }
        }
    }
}
