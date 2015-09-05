using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FECipherVit
{
    public partial class DeckConstruction : Form
    {
        public DeckConstruction()
        {
            InitializeComponent();
        }
        List<string[]> CardData;
        List<int> CardSerialsInDeck = new List<int>();
        List<string> CardNamesInDeck = new List<string>();
        List<int> CardNameCounts = new List<int>();
        List<string> UnitNamesInDeck;
        List<int> UnitNameCounts;
        private void DeckConstruction_Load(object sender, EventArgs e)
        {
            CardData = ((FECipherVit)Owner).CardData;
            CardSearchRenew("");
            try
            {
                pictureBoxCardInfo.Image = Image.FromFile(@"img\back.jpg");
            }
            catch { }
            DeckSelectListRenew();
        }
        private void DeckSelectListRenew()
        {
            comboBox_DeckList.Items.Clear();
            string[] decks = Directory.GetFiles(@"deck\", "*.fe0d");
            foreach (string deck in decks)
            {
                comboBox_DeckList.Items.Add(deck.Replace(".fe0d", "").Replace(@"deck\", ""));
            }
        }
        void CardSearchRenew(string CardName)
        {
            listViewSearchCard.BeginUpdate();
            listViewSearchCard.Items.Clear();
            for (int i = 1; i < CardData.Count; i++)
            {
                if (CardName != "")
                {
                    if (!(CardData[i][3].Contains(CardName) || CardData[i][4].Contains(CardName)))
                    {
                        continue;
                    }
                }
                ListViewItem card = new ListViewItem();
                card.SubItems[0].Text = i.ToString();
                card.SubItems.Add(CardData[i][2]);
                card.SubItems.Add(CardData[i][3]);
                card.SubItems.Add(CardData[i][4]);
                listViewSearchCard.Items.Add(card);
                listViewSearchCard.EndUpdate();
            }
        }

        private void listViewSearchCard_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listViewSearchCard.Columns[e.ColumnIndex].Width;
        }

        void CardInfoRenew(int SerialNo)
        {
            if (SerialNo > 0)
            {
                string[] CardDataInfo = CardData[SerialNo];
                string str = "";
                str += "日文卡名：" + CardDataInfo[3] + Environment.NewLine;
                str += "中文卡名：" + CardDataInfo[4] + Environment.NewLine;
                str += "阶级/兵种：" + CardDataInfo[7] + "/" + CardDataInfo[8] + Environment.NewLine;
                str += "战斗力/支援力/射程：" + CardDataInfo[13] + "/" + CardDataInfo[14] + "/" + CardDataInfo[15] + Environment.NewLine;
                str += "出击/转职费用：" + CardDataInfo[5] + "/" + CardDataInfo[6] + Environment.NewLine;
                str += "势力：" + CardDataInfo[9] + Environment.NewLine;
                str += "性别/武器/属性：" + CardDataInfo[10] + "/" + CardDataInfo[11] + "/";
                if (CardDataInfo[12] == "")
                {
                    str += "-" + Environment.NewLine;
                }
                else
                {
                    str += CardDataInfo[12] + Environment.NewLine;
                }
                str += Environment.NewLine;
                str += "能力：" + Environment.NewLine;
                str += CardDataInfo[16].Replace("$$", Environment.NewLine) + Environment.NewLine;
                if (CardDataInfo[17] != "" && CardDataInfo[17] != "-")
                {
                    str += "支援能力：" + Environment.NewLine;
                    str += CardDataInfo[17];
                }
                textBoxCardInfo.Text = str;
                try
                {
                    pictureBoxCardInfo.Image = Image.FromFile(@"img\" + SerialNo.ToString() + ".jpg");
                }
                catch { }
            }
            else
            {
                try
                {
                    pictureBoxCardInfo.Image = Image.FromFile(@"img\back.jpg");
                }
                catch { }
                textBoxCardInfo.Text = "";
            }
        }
        private void listViewSearchCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSearchCard.SelectedIndices.Count == 1)
            {
                CardInfoRenew(Convert.ToInt32(listViewSearchCard.SelectedItems[0].SubItems[0].Text));
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            CardSearchRenew(textBoxCardName.Text);
            if (listViewSearchCard.Items.Count > 0)
            {
                listViewSearchCard.Items[0].Selected = true;
                listViewSearchCard.Items[0].EnsureVisible();
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxCardName.Text = "";
            CardSearchRenew("");
            CardInfoRenew(0);
        }

        private void textBoxCardName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSearch_Click(null, null);
            }
        }

        private void listViewSearchCard_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewSearchCard.SelectedItems.Count > 0)
            {
                AddToDeck(Convert.ToInt32(listViewSearchCard.SelectedItems[0].SubItems[0].Text));
                listBoxDeckCard.SelectedIndex = listBoxDeckCard.Items.Count - 1;
            }
        }

        bool AddToDeck(int CardSerial)
        {
            string CardName = CardData[CardSerial][4];
            if (CardNamesInDeck.Contains(CardName))
            {
                if (!CardName.Contains("安娜"))
                {
                    if (CardNameCounts[CardNamesInDeck.IndexOf(CardName)] >= 4)
                    {
                        MessageBox.Show("卡片[" + CardName + "]超出卡组所容纳数量。");
                        return false;
                    }
                }
                CardNameCounts[CardNamesInDeck.IndexOf(CardName)]++;
                CardSerialsInDeck.Add(CardSerial);
            }
            else
            {
                CardSerialsInDeck.Add(CardSerial);
                CardNamesInDeck.Add(CardName);
                CardNameCounts.Add(1);
            }
            DeckListRenew();
            return true;
        }

        void DeleteFromDeck(int NumberInDeck)
        {
            int CardSerial = CardSerialsInDeck[NumberInDeck];
            string CardName = CardData[CardSerial][4];
            int pos = CardNamesInDeck.IndexOf(CardName);
            CardNameCounts[pos]--;
            if (CardNameCounts[pos] == 0)
            {
                CardNameCounts.RemoveAt(pos);
                CardNamesInDeck.RemoveAt(pos);
            }
            CardSerialsInDeck.RemoveAt(NumberInDeck);
            DeckListRenew();
        }

        void DeckListRenew()
        {
            listBoxDeckCard.Items.Clear();
            foreach (int CardSerial in CardSerialsInDeck)
            {
                string CardName = CardData[CardSerial][4];
                listBoxDeckCard.Items.Add(CardName);
            }
            labelCardTotal.Text = CardSerialsInDeck.Count.ToString();
        }

        private void listBoxDeckCard_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxDeckCard.SelectedIndex >= 0)
            {
                int oldindex = listBoxDeckCard.SelectedIndex;
                DeleteFromDeck(listBoxDeckCard.SelectedIndex);
                if (oldindex == listBoxDeckCard.Items.Count)
                {
                    listBoxDeckCard.SelectedIndex = listBoxDeckCard.Items.Count - 1;
                }
                else
                {
                    listBoxDeckCard.SelectedIndex = oldindex;
                }
            }
        }

        private void listBoxDeckCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDeckCard.SelectedIndex >= 0 && CardSerialsInDeck.Count > listBoxDeckCard.SelectedIndex)
            {
                CardInfoRenew(CardSerialsInDeck[listBoxDeckCard.SelectedIndex]);
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            CardSerialsInDeck.Sort(CardComparison);
            DeckListRenew();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            CardSerialsInDeck.Clear();
            CardNameCounts.Clear();
            CardNamesInDeck.Clear();
            listBoxDeckCard.Items.Clear();
            labelCardTotal.Text = "0";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBox_DeckList.SelectedIndex >= 0 && CardSerialsInDeck.Count > 0)
            {
                #region CheckDeckContent
                string ErrorText = "";
                foreach (string CardName in CardNamesInDeck)
                {
                    if (CardNameCounts[CardNamesInDeck.IndexOf(CardName)] > 4)
                    {
                        if (!CardName.Contains("安娜"))
                        {
                            ErrorText += "卡片[" + CardName + "]超出卡组所容纳数量。" + Environment.NewLine;
                        }
                    }
                }
                if (CardSerialsInDeck.Count < 50)
                {
                    ErrorText += "卡组不足50张。" + Environment.NewLine;
                }
                if (ErrorText != "")
                {
                    MessageBox.Show(ErrorText + "卡组数据已保存，但您必须修改此卡组以符合要求，否则您不能用其进行游戏。", "保存卡组");
                }
                #endregion

                string text = "";
                int count = 0;
                foreach (int CardSerial in this.CardSerialsInDeck)
                {
                    if (count != 0)
                    {
                        text += Environment.NewLine;
                    }
                    text += CardSerial.ToString();
                    count++;
                }
                File.WriteAllText(@"deck\" + comboBox_DeckList.Text + ".fe0d", text);
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            if (textBox_NewDeck.Text != "")
            {
                if (!comboBox_DeckList.Items.Contains(textBox_NewDeck.Text))
                {
                    comboBox_DeckList.Items.Add(textBox_NewDeck.Text);
                    comboBox_DeckList.SelectedIndex = comboBox_DeckList.Items.IndexOf(textBox_NewDeck.Text);
                }
                textBox_NewDeck.Text = "";
            }
        }

        private void comboBox_DeckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_DeckList.SelectedIndex >= 0)
            {
                if (File.Exists(@"deck\" + comboBox_DeckList.Text + ".fe0d"))
                {
                    CardSerialsInDeck = new List<int>();
                    CardNamesInDeck.Clear();
                    CardNameCounts.Clear();
                    string[] temps = File.ReadAllLines(@"deck\" + comboBox_DeckList.Text + ".fe0d");
                    string ErrorText = "";
                    foreach (string temp in temps)
                    {
                        if (temp != "")
                        {
                            int CardSerial;
                            bool CardFound = Int32.TryParse(temp, out CardSerial);
                            if (CardFound)
                            {
                                if (!(CardSerial > 0 && CardSerial < CardData.Count))
                                {
                                    CardFound = false;
                                }
                            }
                            if (CardFound)
                            {
                                CardSerialsInDeck.Add(CardSerial);
                                string CardName = CardData[Convert.ToInt32(CardSerial)][4];
                                if (CardNamesInDeck.Contains(CardName))
                                {
                                    CardNameCounts[CardNamesInDeck.IndexOf(CardName)]++;
                                }
                                else
                                {
                                    CardNamesInDeck.Add(CardName);
                                    CardNameCounts.Add(1);
                                }
                            }
                            else
                            {
                                ErrorText += "卡片未找到：\"" + temp + "\"" + Environment.NewLine;
                            }
                        }
                    }
                    foreach (string CardName in CardNamesInDeck)
                    {
                        if (CardNameCounts[CardNamesInDeck.IndexOf(CardName)] > 4)
                        {
                            if (!CardName.Contains("安娜"))
                            {
                                ErrorText += "卡片[" + CardName + "]超出卡组所容纳数量。" + Environment.NewLine;
                            }
                        }
                    }
                    if (CardSerialsInDeck.Count < 50)
                    {
                        ErrorText += "卡组不足50张。" + Environment.NewLine;
                    }
                    if (ErrorText != "")
                    {
                        MessageBox.Show(ErrorText + "请修改您的卡组。", "读取卡组");
                    }
                    DeckListRenew();
                }
            }
        }

        private void buttonSetHero_Click(object sender, EventArgs e)
        {
            if (listBoxDeckCard.SelectedIndex >= 0)
            {
                int HeroSerial = CardSerialsInDeck[listBoxDeckCard.SelectedIndex];
                List<int> newList = new List<int>();
                foreach (int Serial in CardSerialsInDeck)
                {
                    if (Serial == HeroSerial)
                    {
                        newList.Add(Serial);
                    }
                }
                List<int> tempList = new List<int>();
                foreach (int Serial in CardSerialsInDeck)
                {
                    if ((Serial != HeroSerial) && (CardData[Serial][3].Substring(CardData[Serial][3].IndexOf(" ") + 1) == CardData[HeroSerial][3].Substring(CardData[HeroSerial][3].IndexOf(" ") + 1)))
                    {
                        tempList.Add(Serial);
                    }
                }
                if (tempList.Count > 0)
                {
                    tempList.Sort(CardComparison);
                    newList.AddRange(tempList);
                }
                foreach (int Serial in CardSerialsInDeck)
                {
                    if ((Serial != HeroSerial) && (CardData[Serial][3].Substring(CardData[Serial][3].IndexOf(" ") + 1) != CardData[HeroSerial][3].Substring(CardData[HeroSerial][3].IndexOf(" ") + 1)))
                    {
                        newList.Add(Serial);
                    }
                }
                CardSerialsInDeck = newList;
                DeckListRenew();
            }
        }

        private void buttonDeleteCardWithSameName_Click(object sender, EventArgs e)
        {
            if (listBoxDeckCard.SelectedIndex >= 0)
            {
                int oldindex = listBoxDeckCard.SelectedIndex;
                string DeleteUnitName = CardData[CardSerialsInDeck[listBoxDeckCard.SelectedIndex]][3].Substring(CardData[CardSerialsInDeck[listBoxDeckCard.SelectedIndex]][3].IndexOf(" ") + 1);
                List<int> NumbersInDeckToDelete = new List<int>();
                for (int i = 0; i < CardSerialsInDeck.Count; i++)
                {
                    if (CardData[CardSerialsInDeck[i]][3].Substring(CardData[CardSerialsInDeck[i]][3].IndexOf(" ") + 1) == DeleteUnitName)
                    {
                        NumbersInDeckToDelete.Add(i);
                    }
                }
                for (int i = 0; i < NumbersInDeckToDelete.Count; i++)
                {
                    DeleteFromDeck(NumbersInDeckToDelete[i] - i);
                }
                if (oldindex >= listBoxDeckCard.Items.Count)
                {
                    listBoxDeckCard.SelectedIndex = listBoxDeckCard.Items.Count - 1;
                }
                else
                {
                    listBoxDeckCard.SelectedIndex = oldindex;
                }
            }
        }

        int CardComparison(int x, int y)
        {
            UnitNamesInDeck = new List<string>();
            UnitNameCounts = new List<int>();
            foreach (int CardSerial in CardSerialsInDeck)
            {
                string UnitName = CardData[Convert.ToInt32(CardSerial)][3].Substring(CardData[Convert.ToInt32(CardSerial)][3].IndexOf(" ") + 1);
                if (UnitNamesInDeck.Contains(UnitName))
                {
                    UnitNameCounts[UnitNamesInDeck.IndexOf(UnitName)]++;
                }
                else
                {
                    UnitNamesInDeck.Add(UnitName);
                    UnitNameCounts.Add(1);
                }
            }
            int posX = CardData[x][3].IndexOf(" ");
            string TitleX = CardData[x][3].Substring(0, posX);
            string NameX = CardData[x][3].Substring(posX + 1);
            int UnitCountX = UnitNameCounts[UnitNamesInDeck.IndexOf(NameX)];
            int CostX = Convert.ToInt32(CardData[x][5]);
            int posY = CardData[y][3].IndexOf(" ");
            string TitleY = CardData[y][3].Substring(0, posY);
            string NameY = CardData[y][3].Substring(posY + 1);
            int UnitCountY = UnitNameCounts[UnitNamesInDeck.IndexOf(NameY)];
            int CostY = Convert.ToInt32(CardData[y][5]);
            if (UnitCountX != UnitCountY)
            {
                return (UnitCountY - UnitCountX);
            }
            else if (string.Compare(NameX, NameY) != 0)
            {
                return string.Compare(NameX, NameY);
            }
            else if (CostX != CostY)
            {
                return CostX - CostY;
            }
            else
            {
                return string.Compare(TitleX, TitleY);
            }
        }
    }
}