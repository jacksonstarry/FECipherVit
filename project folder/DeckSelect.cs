using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace FECipherVit
{
    public partial class DeckSelect : Form
    {
        public DeckSelect(FECipherVit _Owner)
        {
            InitializeComponent();
            Owner = _Owner;
        }
        new FECipherVit Owner;
        public string SelectedDeckFilename;
        private void DeckSelect_Load(object sender, EventArgs e)
        {
            DeckListRenew();
        }

        private void DeckListRenew()
        {
            listBoxDeckList.Items.Clear();
            string[] decks = Directory.GetFiles(@"deck\", "*.fe0d");
            foreach (string deck in decks)
            {
                listBoxDeckList.Items.Add(deck.Replace(".fe0d", "").Replace(@"deck\",""));
            }
        }

        private void listBoxDeckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDeckList.SelectedIndex >= 0)
            {
                string[] CardSerials = File.ReadAllLines(@"deck\" + listBoxDeckList.SelectedItem.ToString() + ".fe0d");
                textBox_CardList.Text = "";
                foreach (string temp in CardSerials)
                {
                    if (temp != "")
                    {
                        int CardSerial;
                        bool CardFound = Int32.TryParse(temp, out CardSerial);
                        if (CardFound)
                        {
                            if (!(CardSerial > 0 && CardSerial < Owner.CardData.Count))
                            {
                                CardFound = false;
                            }
                        }
                        if (CardFound)
                        {
                            textBox_CardList.Text += "[" + Owner.CardData[Convert.ToInt32(CardSerial)][2] + "]" + Owner.CardData[Convert.ToInt32(CardSerial)][4] + Environment.NewLine;
                        }
                        else
                        {
                            textBox_CardList.Text += "卡片未找到：\"" + temp + "\"" + Environment.NewLine;
                        }
                    }
                }
                textBox_CardList.Text = "卡片总数：" + CardSerials.Count() + Environment.NewLine + Environment.NewLine + textBox_CardList.Text;
            }
        }

        private void buttonDeckImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ImportDeckDlg = new OpenFileDialog();
            ImportDeckDlg.Filter = "FECipher卡组(*.fe0d)|*.fe0d";
            ImportDeckDlg.ShowDialog();
            if (ImportDeckDlg.FileName == "")
            {
                return;
            }
            string filename = ImportDeckDlg.FileName;
            string safefilename = ImportDeckDlg.SafeFileName;
            //ArrayList decknames = new ArrayList();
            File.Copy(filename, @"deck\" + safefilename);
            MessageBox.Show("导入成功。", "导入卡组");
            DeckListRenew();
            textBox_CardList.Text = "请在左边的列表中选择卡组。";
            //string format;
            //if (safefilename.Substring(safefilename.LastIndexOf(".") + 1) == "txt")
            //{
            //    format = "txt";
            //}
            //else
            //{
            //    format = "fe0d";
            //}
            //switch (format)
            //{
            //    case "txt":
            //        foreach (string item in listBoxDeckList.Items)
            //        {
            //            decknames.Add(item);
            //        }
            //        if (decknames.Contains(safefilename.Replace(".txt", "")))
            //        {
            //            DialogResult rst = MessageBox.Show("发现同名卡组，是否覆盖？", "导入卡组", System.Windows.Forms.MessageBoxButtons.YesNo);
            //            if (rst == DialogResult.No)
            //            {
            //                return;
            //            }
            //        }
            //        string ErrorText = "";
            //        string DeckFile = "";
            //        StreamReader reader = new StreamReader(filename);
            //        int totalnum;
            //        ArrayList CardCounts = new ArrayList();
            //        for (int i = 0; i < CardNames.Count; i++)
            //        {
            //            CardCounts.Add((int)0);
            //        }
            //        for (totalnum = 0; ; totalnum++)
            //        {
            //            if (reader.EndOfStream)
            //            {
            //                break;
            //            }
            //            string str = reader.ReadLine();
            //            int num = CardNames.IndexOf(str);
            //            if (num == -1)
            //            {
            //                ErrorText += "卡片[" + str + "]未找到。" + Environment.NewLine;
            //            }
            //            else
            //            {
            //                DeckFile += num + ",";
            //                CardCounts[num] = (int)(CardCounts[num]) + 1;
            //            }
            //        }
            //        reader.Close();
            //        for (int i = 0; i < CardCounts.Count; i++)
            //        {
            //            if ((int)(CardCounts[i]) > 4)
            //            {
            //                if (!((string)CardNames[i]).Contains("安娜"))
            //                {
            //                    ErrorText += "卡片[" + (string)CardNames[i] + "]超出卡组所容纳数量。" + Environment.NewLine;
            //                }
            //            }
            //        }
            //        if (totalnum < 50)
            //        {
            //            ErrorText += "卡组不足50张。" + Environment.NewLine;
            //        }
            //        if (ErrorText == "")
            //        {
            //            File.WriteAllText(@"deck\" + safefilename.Replace(".txt", ".fe0d"), DeckFile);
            //            MessageBox.Show("导入成功。", "导入卡组");
            //            DeckListRenew();
            //            textBox_CardList.Text = "请在左边的列表中选择卡组。";
            //        }
            //        else
            //        {
            //            MessageBox.Show("导入失败。" + Environment.NewLine + ErrorText, "导入卡组");
            //        }
            //        break;
            //    case "fe0d":
            //        ;
            //        break;
            //}
        }

        private void buttonDeckDelete_Click(object sender, EventArgs e)
        {
            if (listBoxDeckList.SelectedIndex >= 0)
            {
                File.Delete(@"deck\" + listBoxDeckList.SelectedItem.ToString() + ".fe0d");
            }
            DeckListRenew();
            textBox_CardList.Text = "请在左边的列表中选择卡组。";
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (listBoxDeckList.SelectedItem != null)
            {
                #region CheckDeckContent
                List<string> CardSerials = new List<string>();
                CardSerials.AddRange(File.ReadAllLines(@"deck\" + listBoxDeckList.SelectedItem.ToString() + ".fe0d"));
                while(CardSerials.Contains(""))
                {
                    CardSerials.Remove("");
                }
                if(CardSerials.Count<50)
                {
                    MessageBox.Show("卡组不足50张。", "卡组内容不符合要求");
                    return;
                }
                List<string> CardNames = new List<string>();
                List<int> CardCounts = new List<int>();
                string ErrorText = "";
                foreach (string temp in CardSerials)
                {
                    int CardSerial;
                    bool CardFound = Int32.TryParse(temp, out CardSerial);
                    if (CardFound)
                    {
                        if (!(CardSerial > 0 && CardSerial < Owner.CardData.Count))
                        {
                            CardFound = false;
                        }
                    }
                    if (CardFound)
                    {
                        string CardName = Owner.CardData[Convert.ToInt32(CardSerial)][4];
                        if (CardNames.Contains(CardName))
                        {
                            CardCounts[CardNames.IndexOf(CardName)]++;
                        }
                        else
                        {
                            CardNames.Add(CardName);
                            CardCounts.Add(1);
                        }
                    }
                    else
                    {
                        ErrorText += "卡片未找到：\"" + temp + "\"" + Environment.NewLine;
                    }
                }
                if(ErrorText!="")
                {
                    MessageBox.Show(ErrorText.Remove(ErrorText.LastIndexOf(Environment.NewLine)), "部分卡片未找到。");
                    return;
                }
                ErrorText = "";
                foreach(string CardName in CardNames)
                {
                    if(CardCounts[CardNames.IndexOf(CardName)]>4)
                    {
                        if (!CardName.Contains("安娜"))
                        {
                            ErrorText += "卡片[" + CardName + "]超出卡组所容纳数量。" + Environment.NewLine;
                        }
                    }
                }
                if(ErrorText!="")
                {
                    MessageBox.Show(ErrorText.Remove(ErrorText.LastIndexOf(Environment.NewLine)), "卡组内容不符合要求");
                    return;
                }
                #endregion

                SelectedDeckFilename = @"deck\" + listBoxDeckList.SelectedItem.ToString() + ".fe0d";
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("请在左边的列表中选择卡组。", "选择卡组");
            }
        }
    }
}
