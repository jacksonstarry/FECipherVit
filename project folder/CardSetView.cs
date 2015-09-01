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
    public partial class CardSetView : Form
    {
        public CardSetView(Region _ThisRegion, string _RegionType, User _Player, FECipherVit _Owner)
        {
            InitializeComponent();
            ThisRegion = _ThisRegion;
            RegionType = _RegionType;
            Player = _Player;
            Owner = _Owner;
        }
        Region ThisRegion;
        string RegionType;
        User Player;
        new FECipherVit Owner;

        private void CardSetView_Load(object sender, EventArgs e)
        {
            CardListRenew();
        }
        private void CardListRenew()
        {
            int index_old = CardListBox.SelectedIndex;
            CardListBox.Items.Clear();
            foreach (Card card in ThisRegion.CardList)
            {
                string temp = "";
                temp += card.UnitTitle + " " + card.UnitName;
                CardListBox.Items.Add(temp);
            }
            switch(RegionType)
            {
                case "Deck":
                    this.Text = "查看卡组：" + ThisRegion.CardList.Count.ToString() + "张";
                    button_ToDeckShuffle.Enabled = false;
                    break;
                case "Grave":
                    this.Text = "查看退避区：" + ThisRegion.CardList.Count.ToString() + "张";
                    button_ToGrave.Enabled = false;
                    break;
                case "RivalGrave":
                    this.Text = "查看对方退避区：" + ThisRegion.CardList.Count.ToString() + "张";
                    button_Show.Enabled = false;
                    button_ToBackField.Enabled = false;
                    button_ToDeckBottom.Enabled = false;
                    button_ToDeckShuffle.Enabled = false;
                    button_ToDeckTop.Enabled = false;
                    button_ToFrontField.Enabled = false;
                    button_ToGrave.Enabled = false;
                    button_ToHand.Enabled = false;
                    break;
                case "Hand":
                    this.Text = "查看手牌：" + ThisRegion.CardList.Count.ToString() + "张";
                    button_ToHand.Enabled = false;
                    break;
            }
            if (index_old < 0)
            {
                CardListBox.SelectedIndex = 0;
            }
            else if (index_old >= CardListBox.Items.Count)
            {
                CardListBox.SelectedIndex = CardListBox.Items.Count - 1;
            }
            else
            {
                CardListBox.SelectedIndex = index_old;
            }
        }
        private void button_ToFrontField_Click(object sender, EventArgs e)
        {
            if (CardListBox.SelectedIndex >= 0)
            {
                Card thisCard = ThisRegion.CardList[CardListBox.SelectedIndex];
                int CardNoWithSameName = -1;
                if (Player.FrontField.SearchCard(thisCard.UnitName) != null)
                {
                    CardNoWithSameName = Player.FrontField.SearchCard(thisCard.UnitName).NumberInDeck;
                }
                else if (Player.BackField.SearchCard(thisCard.UnitName) != null)
                {
                    CardNoWithSameName = Player.BackField.SearchCard(thisCard.UnitName).NumberInDeck;
                }
                if (CardNoWithSameName != -1)
                {
                    DialogResult Overlay = MessageBox.Show("已有同角色名的单位在场，是否叠放？", "叠放", MessageBoxButtons.YesNo);
                    if (Overlay == DialogResult.Yes)
                    {
                        Card OverlayedCard = Player.SearchCard(CardNoWithSameName);
                        Owner.msgProcessor.Send("Overlay", "#从[" + Owner.GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + Owner.GetRegionNameInString(OverlayedCard.BelongedRegion()) + "][" + OverlayedCard.UnitTitle + " " + OverlayedCard.UnitName + "]升级/转职为[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]。");
                        Player.OverlayCard(thisCard, CardNoWithSameName);
                    }
                    else
                    {
                        Owner.msgProcessor.Send("Summon", "#从[" + Owner.GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]出击到前卫区（已存在同角色名的单位）。");
                        Player.MoveCard(thisCard, Player.FrontField);
                    }
                }
                else
                {
                    Owner.msgProcessor.Send("Summon", "#从[" + Owner.GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]出击到前卫区。");
                    Player.MoveCard(thisCard, Player.FrontField);
                }
                thisCard.FrontShown = true;
                thisCard.IsHorizontal = false;
                Owner.msgProcessor.Send("Update", "");
                CardListRenew();
                Owner.Renew();
            }
        }
        private void button_ToBackField_Click(object sender, EventArgs e)
        {
            if (CardListBox.SelectedIndex >= 0)
            {
                Card thisCard = ThisRegion.CardList[CardListBox.SelectedIndex];
                int CardNoWithSameName = -1;
                if (Player.BackField.SearchCard(thisCard.UnitName) != null)
                {
                    CardNoWithSameName = Player.BackField.SearchCard(thisCard.UnitName).NumberInDeck;
                }
                else if (Player.FrontField.SearchCard(thisCard.UnitName) != null)
                {
                    CardNoWithSameName = Player.FrontField.SearchCard(thisCard.UnitName).NumberInDeck;
                }
                if (CardNoWithSameName != -1)
                {
                    DialogResult Overlay = MessageBox.Show("已有同角色名的单位在场，是否叠放？", "叠放", MessageBoxButtons.YesNo);
                    if (Overlay == DialogResult.Yes)
                    {
                        Card OverlayedCard = Player.SearchCard(CardNoWithSameName);
                        Owner.msgProcessor.Send("Overlay", "#从[" + Owner.GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + Owner.GetRegionNameInString(OverlayedCard.BelongedRegion()) + "][" + OverlayedCard.UnitTitle + " " + OverlayedCard.UnitName + "]升级/转职为[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]。");
                        Player.OverlayCard(thisCard, CardNoWithSameName);
                    }
                    else
                    {
                        Owner.msgProcessor.Send("Summon", "#从[" + Owner.GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]出击到后卫区（已存在同角色名的单位）。");
                        Player.MoveCard(thisCard, Player.BackField);
                    }
                }
                else
                {
                    Owner.msgProcessor.Send("Summon", "#从[" + Owner.GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]出击到后卫区。");
                    Player.MoveCard(thisCard, Player.BackField);
                }
                thisCard.FrontShown = true;
                thisCard.IsHorizontal = false;
                Owner.msgProcessor.Send("Update", "");
                CardListRenew();
                Owner.Renew();
            }
        }
        private void button_Show_Click(object sender, EventArgs e)
        {
            if (CardListBox.SelectedIndex >= 0)
            {
                Card thisCard = ThisRegion.CardList[CardListBox.SelectedIndex];
                Owner.msgProcessor.Send("ShowCard", "#展示[" + Owner.GetRegionNameInString(thisCard.BelongedRegion()) + "(" + (thisCard.BelongedRegion().CardList.IndexOf(thisCard) + 1).ToString() + ")]：[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]。");
                CardListRenew();
                Owner.Renew();
            }
        }
        private void button_ToHand_Click(object sender, EventArgs e)
        {
            if (CardListBox.SelectedIndex >= 0)
            {
                Card thisCard = ThisRegion.CardList[CardListBox.SelectedIndex];
                Owner.msgProcessor.Send("ToHand", Owner.GetTextOfMovingToRegion(thisCard, "Hand", true));
                Player.MoveCard(thisCard, Player.Hand);
                thisCard.FrontShown = true;
                thisCard.IsHorizontal = false;
                thisCard.Comments = "";
                if (thisCard.OverlayCardNo.Count != 0)
                {
                    DialogResult result = MessageBox.Show("这张卡下面的叠放卡将被送入退避区。", "加入手牌");
                    foreach (int CardNo in thisCard.OverlayCardNo)
                    {
                        Player.MoveCard(Player.Overlay.SearchCard(CardNo), Player.Grave);
                    }
                    thisCard.OverlayCardNo = new List<int>();
                }
                Owner.msgProcessor.Send("Update", "");
                CardListRenew();
                Owner.Renew();
            }
        }
        private void button_ToGrave_Click(object sender, EventArgs e)
        {
            if (CardListBox.SelectedIndex >= 0)
            {
                Card thisCard = ThisRegion.CardList[CardListBox.SelectedIndex];
                Owner.msgProcessor.Send("ToGrave", Owner.GetTextOfMovingToRegion(thisCard, "Grave", true));
                Player.MoveCard(thisCard, Player.Grave);
                thisCard.FrontShown = true;
                thisCard.IsHorizontal = false;
                thisCard.Comments = "";
                if (thisCard.OverlayCardNo.Count != 0)
                {
                    foreach (int CardNo in thisCard.OverlayCardNo)
                    {
                        Player.MoveCard(Player.Overlay.SearchCard(CardNo), Player.Grave);
                    }
                    thisCard.OverlayCardNo = new List<int>();
                }
                Owner.msgProcessor.Send("Update", "");
                CardListRenew();
                Owner.Renew();
            }
        }
        private void button_ToDeckShuffle_Click(object sender, EventArgs e)
        {
            if (CardListBox.SelectedIndex >= 0)
            {
                Card thisCard = ThisRegion.CardList[CardListBox.SelectedIndex];
                Owner.msgProcessor.Send("ToDeckShuffle", Owner.GetTextOfMovingToRegion(thisCard, "DeckShuffle", true));
                if (thisCard.OverlayCardNo.Count != 0)
                {
                    DialogResult result = MessageBox.Show("这张卡下面的叠放卡将被送入退避区。", "加入卡组并切洗");
                    foreach (int CardNo in thisCard.OverlayCardNo)
                    {
                        Player.MoveCard(Player.Overlay.SearchCard(CardNo), Player.Grave);
                    }
                    thisCard.OverlayCardNo = new List<int>();
                }
                Player.MoveCard(thisCard, Player.Deck);
                thisCard.FrontShown = true;
                thisCard.IsHorizontal = false;
                thisCard.Comments = "";
                Player.Deck.Shuffle();
                Owner.msgProcessor.Send("Update", "");
                CardListRenew();
                Owner.Renew();
            }
        }
        private void button_ToDeckTop_Click(object sender, EventArgs e)
        {
            if (CardListBox.SelectedIndex >= 0)
            {
                Card thisCard = ThisRegion.CardList[CardListBox.SelectedIndex];
                Owner.msgProcessor.Send("ToDeckTop", Owner.GetTextOfMovingToRegion(thisCard, "DeckTop", true));
                if (thisCard.OverlayCardNo.Count != 0)
                {
                    DialogResult result = MessageBox.Show("这张卡下面的叠放卡将被送入退避区。", "加入卡组并切洗");
                    foreach (int CardNo in thisCard.OverlayCardNo)
                    {
                        Player.MoveCard(Player.Overlay.SearchCard(CardNo), Player.Grave);
                    }
                    thisCard.OverlayCardNo = new List<int>();
                }
                Player.MoveCard(thisCard, Player.Deck, 0);
                thisCard.FrontShown = true;
                thisCard.IsHorizontal = false;
                thisCard.Comments = "";
                Owner.msgProcessor.Send("Update", "");
                CardListRenew();
                Owner.Renew();
            }
        }
        private void button_ToDeckBottom_Click(object sender, EventArgs e)
        {
            if (CardListBox.SelectedIndex >= 0)
            {
                Card thisCard = ThisRegion.CardList[CardListBox.SelectedIndex];
                Owner.msgProcessor.Send("ToDeckBottom", Owner.GetTextOfMovingToRegion(thisCard, "DeckBottom", true));
                if (thisCard.OverlayCardNo.Count != 0)
                {
                    DialogResult result = MessageBox.Show("这张卡下面的叠放卡将被送入退避区。", "加入卡组并切洗");
                    foreach (int CardNo in thisCard.OverlayCardNo)
                    {
                        Player.MoveCard(Player.Overlay.SearchCard(CardNo), Player.Grave);
                    }
                    thisCard.OverlayCardNo = new List<int>();
                }
                Player.MoveCard(thisCard, Player.Deck);
                thisCard.FrontShown = true;
                thisCard.IsHorizontal = false;
                thisCard.Comments = "";
                Owner.msgProcessor.Send("Update", "");
                CardListRenew();
                Owner.Renew();
            }
        }
        private void button_Confirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CardSetView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (RegionType == "Deck")
            {
                DialogResult result = MessageBox.Show("是否切洗卡组？", "查看卡组", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Owner.msgProcessor.Send("ExitDeckCheck", "#关闭卡组并切洗。");
                    Player.Deck.Shuffle();
                }
                else
                {
                    Owner.msgProcessor.Send("ExitDeckCheck", "#关闭卡组，未切洗。");
                }
            }
        }
        private void CardListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CardListBox.SelectedIndex >= 0 && CardListBox.SelectedIndex < CardListBox.Items.Count)
            {
                CardInfoRenew(ThisRegion.CardList[CardListBox.SelectedIndex]);
            }
        }
        public void CardInfoRenew(Card thisCard)
        {
            try
            {
                pictureBoxCardInfo.Image = Image.FromFile(@"img/" + thisCard.SerialNo.ToString() + ".jpg");
            }
            catch
            {
                pictureBoxCardInfo.Image = pictureBoxCardInfo.ErrorImage;
            }
            string[] CardDataInfo = Owner.CardData[thisCard.SerialNo];
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
                str += CardDataInfo[17] + Environment.NewLine;
            }
            if (thisCard.OverlayCardNo.Count != 0)
            {
                str += Environment.NewLine;
                foreach (int CardNo in thisCard.OverlayCardNo)
                {
                    Card OverlayedCard = Player.Overlay.SearchCard(CardNo);
                    if (OverlayedCard != null)
                    {
                        str += "叠放：" + OverlayedCard.UnitTitle + " " + OverlayedCard.UnitName + Environment.NewLine;
                    }
                }
            }
            if (thisCard.Comments != "")
            {
                str += Environment.NewLine;
                str += "实际战斗力：" + (thisCard.Power + thisCard.Comments).ToString() + Environment.NewLine;
            }
            textBoxCardInfo.Text = str;
        }
    }
}
