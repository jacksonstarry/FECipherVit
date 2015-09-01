using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FECipherVit
{
    public class CardPic : PictureBox
    {
        public CardPic(Card card, string type)
        {
            NumberInDeck = card.NumberInDeck;
            SerialNo = card.SerialNo;
            SizeMode = PictureBoxSizeMode.StretchImage;
            Width = 80;
            Height = 112;
            try
            {

                Image = Image.FromFile(@"img/" + SerialNo.ToString() + ".jpg");
            }
            catch
            {
                Image = this.ErrorImage;
            }
            thisCard = card;
            Type = type;
        }
        public CardPic(string type)
        {
            SizeMode = PictureBoxSizeMode.StretchImage;
            Width = 80;
            Height = 112;
            try
            {
                Image = Image.FromFile(@"img/back.jpg");
            }
            catch
            {
                Image = this.ErrorImage;
            }
            Type = type;
        }
        public int NumberInDeck;
        public int SerialNo;
        public bool FrontShown = true;
        public bool IsHorizontal = false;
        public Card thisCard;
        public string Type;

        public void ReverseToBack()
        {
            try
            {
                Image.Dispose();
                Image = Image.FromFile(@"img/back.jpg");
            }
            catch
            {
                Image = this.ErrorImage;
            }
            FrontShown = false;
        }
        public void ReverseToFront()
        {
            try
            {
                Image.Dispose();
                Image = Image.FromFile(@"img/" + SerialNo.ToString() + ".jpg");
            }
            catch
            {
                Image = this.ErrorImage;
            }
            FrontShown = true;
        }
        public void Reverse()
        {
            if (FrontShown)
            {
                ReverseToBack();
            }
            else
            {
                ReverseToFront();
            }
        }
        public void SetHorizontal()
        {
            Width = 112;
            Height = 80;
            this.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Top += (112 - 80) / 2;
            Left -= (112 - 80) / 2;
            IsHorizontal = true;
        }
        public void UnSetHorizontal()
        {
            Width = 80;
            Height = 112;
            this.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Top -= (112 - 80) / 2;
            Left += (112 - 80) / 2;
            IsHorizontal = false;
        }
        public void Rotate()
        {
            if (IsHorizontal)
            {
                UnSetHorizontal();
            }
            else
            {
                SetHorizontal();
            }
        }
    }
}
