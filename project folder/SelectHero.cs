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
    public partial class SelectHero : Form
    {
        public SelectHero(User _Player, FECipherVit _Owner)
        {
            InitializeComponent();
            Player = _Player;
            Owner = _Owner;
        }
        User Player;
        public int HeroNum;
        new FECipherVit Owner;

        private void CardSetView_Load(object sender, EventArgs e)
        {
            CardListRenew();
        }

        private void CardListRenew()
        {
            int index_old = CardListBox.SelectedIndex;
            CardListBox.Items.Clear();
            foreach (Card card in Player.Deck.CardList)
            {
                string temp = "";
                temp += card.UnitTitle + " " + card.UnitName;
                CardListBox.Items.Add(temp);
            }
            if (index_old < 0)
            {
                CardListBox.SelectedIndex = 0;
            }
            else if (index_old >= CardListBox.Items.Count)
            {
                CardListBox.SelectedIndex = CardListBox.Items.Count - 1;
            }
        }

        private void button_Confirm_Click(object sender, EventArgs e)
        {
            HeroNum = CardListBox.SelectedIndex;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CardListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CardListBox.SelectedIndex >= 0 && CardListBox.SelectedIndex < CardListBox.Items.Count)
            {
                Owner.CardInfoRenew(Player.Deck.CardList[CardListBox.SelectedIndex]);
            }
        }
    }
}
