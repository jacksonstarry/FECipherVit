using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FECipherVit
{
    public partial class GetInteger : Form
    {
        public GetInteger(string type)
        {
            InitializeComponent();
            Type = type;
        }

        string Type;
        
        private void GetInteger_Load(object sender, EventArgs e)
        {
            switch (Type)
            {
                case "抽复数张卡":
                    this.Text = "抽复数张卡";
                    this.label.Text = "请输入要抽的卡的数量：";
                    break;
                case "复数羁绊卡右移":
                    this.Text = "复数羁绊卡右移";
                    this.label.Text = "请输入要右移的羁绊卡的数量：";
                    break;
            }
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            int x;
            switch (Type)
            {
                case "抽复数张卡":
                    if (Int32.TryParse(textBox.Text, out x))
                    {
                        if (x > 0 && x <= ((FECipherVit)Owner).Player.Deck.CardList.Count)
                        {
                            ((FECipherVit)Owner).Player.Draw(x);
                            ((FECipherVit)Owner).msgProcessor.Send("MultipleDraw", "#抽" + x.ToString() + "张卡。");
                            ((FECipherVit)Owner).Renew();
                            this.Close();
                        }
                        else if(x > ((FECipherVit)Owner).Player.Deck.CardList.Count)
                        {
                            MessageBox.Show("卡组不足。", "抽复数张卡");
                            textBox.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("请正确输入要抽卡的数量。", "抽复数张卡");
                            textBox.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("请正确输入要抽卡的数量。", "抽X张卡");
                        textBox.Text = "";
                    }
                    break;
                case "复数羁绊卡右移":
                    if (Int32.TryParse(textBox.Text, out x))
                    {
                        if (x > 0 && x <= ((FECipherVit)Owner).Player.Kizuna.CardList.Count)
                        {
                            for (int i = 0; i < x; i++)
                            {
                                ((FECipherVit)Owner).Player.MoveCard(((FECipherVit)Owner).Player.Kizuna.CardList[((FECipherVit)Owner).Player.Kizuna.CardList.Count - 1], ((FECipherVit)Owner).Player.KizunaUsed);
                            }
                            ((FECipherVit)Owner).msgProcessor.Send("MultipleDraw", "#右移" + x.ToString() + "张羁绊卡。");
                            ((FECipherVit)Owner).Renew();
                            this.Close();
                        }
                        else if (x > ((FECipherVit)Owner).Player.Deck.CardList.Count)
                        {
                            MessageBox.Show("羁绊卡不足。", "复数羁绊卡右移");
                            textBox.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("请正确输入要移动的数量。", "复数羁绊卡右移");
                            textBox.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("请正确输入要移动的数量。", "复数羁绊卡右移");
                        textBox.Text = "";
                    }
                    break;
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                Button_Confirm_Click(null, null);
            }
        }
    }
}
