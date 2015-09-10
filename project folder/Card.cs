using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FECipherVit
{
    public class Card
    {
        public Card(int _NumberInDeck, int _SerialNo,  string _CardName, int _Power, int _Support, User _Controller)
        {
            NumberInDeck = _NumberInDeck;
            SerialNo = _SerialNo;
            if (_CardName != "")
            {
                int pos = _CardName.IndexOf(" ");
                UnitTitle = _CardName.Substring(0, pos);
                UnitName = _CardName.Substring(pos + 1);
            }
            Power = _Power;
            Support = _Support;
            Controller = _Controller;
            OverlayCardNo = new List<int>();
        }
        public Card(string CardAsString, User _Controller)
        {
            List<string> Card = new List<string>();
            Card.AddRange(CardAsString.Replace("[", "").Replace("]", "").Split(new string[] { "," }, StringSplitOptions.None));
            NumberInDeck = Convert.ToInt32(Card[0]);
            SerialNo = Convert.ToInt32(Card[1]);
            UnitTitle = Card[2];
            UnitName = Card[3];
            Power = Convert.ToInt32(Card[4]);
            Support = Convert.ToInt32(Card[5]);
            IsHorizontal = Convert.ToBoolean(Card[6]);
            FrontShown = Convert.ToBoolean(Card[7]);
            Visible = Convert.ToBoolean(Card[8]);
            List<string> temp = new List<string>();
            if (Card[9] != "()")
            {
                temp.AddRange(Card[9].Replace("(", "").Replace(")", "").Split(new string[] { ";" }, StringSplitOptions.None));
                while (temp.Contains(""))
                {
                    temp.Remove("");
                }
                OverlayCardNo = temp.ConvertAll<Int32>(Convert.ToInt32);
            }
            else
            {
                OverlayCardNo = new List<int>();
            }
            Comments = Card[10];
            Controller = _Controller;
        }
        public int NumberInDeck;
        public int SerialNo;
        public string UnitTitle;
        public string UnitName;
        public int Power;
        public int Support;
        public User Controller;
        public bool IsHorizontal = false;
        public bool FrontShown = false;
        public bool Visible = false;
        public List <int> OverlayCardNo;
        public string Comments = "";

        public Region BelongedRegion()
        {
            foreach (Region region in this.Controller.AllRegions)
            {
                if (region.CardList.Contains(this))
                {
                    return region;
                }
            }
            return null;
        }
        public void RemoveFromAny()
        {
            Region region = BelongedRegion();
            region.CardList.Remove(this);
        }
        public string toString()
        {
            string text = "[";
            text += NumberInDeck.ToString() + ",";
            text += SerialNo.ToString() + ",";
            text += UnitTitle + ",";
            text+= UnitName + ",";
            text += Power.ToString() + ",";
            text += Support.ToString() + ",";
            text += IsHorizontal.ToString() + ",";
            text += FrontShown.ToString() + ",";
            text += Visible.ToString() + ",";
            text += "(";
            foreach(int no in OverlayCardNo)
            {
                text += no.ToString() + ";";
            }
            text += "),";
            text += Comments + "]";
            return text;
        }

    }
    public class Region
    {
        public Region(User _Controller)
        {
            CardList = new List<Card>();
            Controller = _Controller;
        }
        public Region(string RegionAsString, User _Controller)
        {
            Controller = _Controller;
            List<string> temp = new List<string>();
            temp.AddRange(RegionAsString.Replace("{", "").Replace("}", "").Split(new string[] { "],[" }, StringSplitOptions.RemoveEmptyEntries));
            CardList = new List<Card>();
            foreach (string cardtemp in temp)
            {
                CardList.Add(new Card(cardtemp, Controller));
            }
        }
        public List<Card> CardList;
        public User Controller;
        public void Clear()
        {
            this.CardList.Clear();
        }
        public void Shuffle()
        {
            int N = this.CardList.Count;
            int[] array = new int[N];
            for (int i = 0; i < N; i++)
            {
                array[i] = i;
            }
            Random rnd = new Random();
            for (int j = 0; j < N; j++)
            {
                int pos = rnd.Next(j, N);
                int temp = array[pos];
                array[pos] = array[j];
                array[j] = temp;
            }
            List<Card> CardList_temp = new List<Card>();
            for (int i = 0; i < N; i++)
            {
                CardList_temp.Add(CardList[array[i]]);
            }
            CardList = CardList_temp;
        }
        public Card SearchCard(int NumberInDeck)
        {
            foreach (object card in CardList)
            {
                if (((Card)card).NumberInDeck == NumberInDeck)
                {
                    return (Card)card;
                }
            }
            return null;
        }
        public Card SearchCard(string unitname)
        {
            foreach (Card card in CardList)
            {
                if (card.UnitName == unitname)
                {
                    return card;
                }
                if (card.UnitTitle == "自称马尔斯的剑士" && unitname == "马尔斯")
                {
                    return card;
                }
            }
            return null;
        }

        public int CardNum; //Only for invisible region of rival
        public int GetReversedNum()
        {
            int count = 0;
            foreach(Card card in CardList)
            {
                if(!card.FrontShown)
                {
                    count++;
                }
            }
            return count;
        }
        public int GetHorizontalNum()
        {
            int count = 0;
            foreach (Card card in CardList)
            {
                if (card.IsHorizontal)
                {
                    count++;
                }
            }
            return count;
        }
        public string toString()
        {
            int count = 0;
            string text = "{";
            foreach(Card card in CardList)
            {
                if(count!=0)
                {
                    text += ",";
                }
                text += card.toString();
                count++;
            }
            text += "}";
            return text;
        }
    }
    public class User
    {
        public User()
        {
            Deck = new Region(this);
            Hand = new Region(this);
            Grave = new Region(this);
            Support = new Region(this);
            Kizuna = new Region(this);
            KizunaUsed = new Region(this);
            Orb = new Region(this);
            FrontField = new Region(this);
            BackField = new Region(this);
            Overlay = new Region(this);
            AllRegions = new List<Region>();
            AllRegions.Add(Deck);
            AllRegions.Add(Hand);
            AllRegions.Add(Grave);
            AllRegions.Add(Support);
            AllRegions.Add(Kizuna);
            AllRegions.Add(KizunaUsed);
            AllRegions.Add(Orb);
            AllRegions.Add(FrontField);
            AllRegions.Add(BackField);
            AllRegions.Add(Overlay);
        }
        public User(string UserAsString)
        {
            List<string> temp = new List<string>();
            temp.AddRange(UserAsString.Replace("#", "").Replace("#", "").Split(new string[] { "},{" }, StringSplitOptions.None));
            Deck = new Region(temp[0], this);
            Hand = new Region(temp[1], this);
            Grave = new Region(temp[2], this);
            Support = new Region(temp[3], this);
            Kizuna = new Region(temp[4], this);
            KizunaUsed = new Region(temp[5], this);
            Orb = new Region(temp[6], this);
            FrontField = new Region(temp[7], this);
            BackField = new Region(temp[8], this);
            Overlay = new Region(temp[9], this);
            AllRegions = new List<Region>();
            AllRegions.Add(Deck);
            AllRegions.Add(Hand);
            AllRegions.Add(Grave);
            AllRegions.Add(Support);
            AllRegions.Add(Kizuna);
            AllRegions.Add(KizunaUsed);
            AllRegions.Add(Orb);
            AllRegions.Add(FrontField);
            AllRegions.Add(BackField);
            AllRegions.Add(Overlay);
        }
        public List<Region> AllRegions;
        public Region Deck;
        public Region Hand;
        public Region Grave;
        public Region Support;
        public Region Kizuna;
        public Region KizunaUsed;
        public Region Orb;
        public Region FrontField;
        public Region BackField;
        public Region Overlay;

        public void MoveCard(Region fromWhere, int Number, Region toWhere)
        {
            if (toWhere.Equals(this.Deck) || toWhere.Equals(this.Orb))
            {
                fromWhere.CardList[Number].FrontShown = false;
                fromWhere.CardList[Number].Visible = false;
            }
            else
            {
                if (!((toWhere.Equals(this.Kizuna) || toWhere.Equals(this.KizunaUsed)) && (fromWhere.Equals(this.Kizuna) || fromWhere.Equals(this.KizunaUsed))))
                {
                    fromWhere.CardList[Number].FrontShown = true;
                    fromWhere.CardList[Number].Visible = true;
                }
            }
            toWhere.CardList.Add(fromWhere.CardList[Number]);
            fromWhere.CardList.RemoveAt(Number);
        }
        public void MoveCard(Region fromWhere, int Number, Region toWhere, int NumberDes )
        {
            if (toWhere.Equals(this.Deck) || toWhere.Equals(this.Orb))
            {
                fromWhere.CardList[Number].FrontShown = false;
                fromWhere.CardList[Number].Visible = false;
            }
            else
            {
                if (!((toWhere.Equals(this.Kizuna) || toWhere.Equals(this.KizunaUsed)) && (fromWhere.Equals(this.Kizuna) || fromWhere.Equals(this.KizunaUsed))))
                {
                    fromWhere.CardList[Number].FrontShown = true;
                    fromWhere.CardList[Number].Visible = true;
                }
            }
            toWhere.CardList.Insert(NumberDes, fromWhere.CardList[Number]);
            fromWhere.CardList.RemoveAt(Number);
        }
        public void MoveCard(Card card, Region toWhere)
        {
            if (toWhere.Equals(this.Deck) || toWhere.Equals(this.Orb))
            {
                card.FrontShown = false;
                card.Visible = false;
            }
            else
            {
                if (!((toWhere.Equals(this.Kizuna) || toWhere.Equals(this.KizunaUsed)) && (card.BelongedRegion().Equals(this.Kizuna) || card.BelongedRegion().Equals(this.KizunaUsed))))
                {
                    card.FrontShown = true;
                    card.Visible = true;
                }
            }
            Region fromWhere = card.BelongedRegion();
            fromWhere.CardList.Remove(card);
            toWhere.CardList.Add(card);
        }
        public void MoveCard(Card card, Region toWhere, int NumberDes)
        {
            if (toWhere.Equals(this.Deck) || toWhere.Equals(this.Orb))
            {
                card.FrontShown = false;
                card.Visible = false;
            }
            else
            {
                if (!((toWhere.Equals(this.Kizuna) || toWhere.Equals(this.KizunaUsed)) && (card.BelongedRegion().Equals(this.Kizuna) || card.BelongedRegion().Equals(this.KizunaUsed))))
                {
                    card.FrontShown = true;
                    card.Visible = true;
                }
            }
            Region fromWhere = card.BelongedRegion();
            fromWhere.CardList.Remove(card);
            toWhere.CardList.Insert(NumberDes, card);
        }
        public void RotateCard(Region Region, int CardNo, int Option)
        {
            if (Option == 0)
            {
                Region.CardList[CardNo].IsHorizontal = true;
            }
            else if (Option == 1)
            {
                Region.CardList[CardNo].IsHorizontal = false;
            }
            else
            {
                Region.CardList[CardNo].IsHorizontal = !Region.CardList[CardNo].IsHorizontal;
            }
        }
        public void ReverseCard(Region Region, int CardNo, int Option)
        {
            if (Option == 0)
            {
                Region.CardList[CardNo].FrontShown = false;
            }
            else if (Option == 1)
            {
                Region.CardList[CardNo].FrontShown = true;
            }
            else
            {
                Region.CardList[CardNo].FrontShown = !Region.CardList[CardNo].FrontShown;
            }
        }
        public void Draw()
        {
            MoveCard(Deck, 0, Hand);
        }
        public void Draw(int count)
        {
            for (int i = 0; i < count; i++)
            {
                MoveCard(Deck, 0, Hand);
            }
        }
        public Card SearchCard(int NumberInDeck)
        {
            foreach (Region region in AllRegions)
            {
                if (region.SearchCard(NumberInDeck) != null)
                {
                    return region.SearchCard(NumberInDeck);
                }
            }
            return null;
        }
        public void OverlayCard(Card card, int DesCardNo)
        {
            Card DesCard = SearchCard(DesCardNo);
            Region DesRegion = DesCard.BelongedRegion();
            int pos = DesCard.BelongedRegion().CardList.IndexOf(DesCard);
            MoveCard(DesCard, Overlay);
            MoveCard(card, DesRegion, pos);
            if (DesCard.OverlayCardNo.Count != 0)
            {
                card.OverlayCardNo.AddRange(DesCard.OverlayCardNo);
                DesCard.OverlayCardNo = new List<int>();
            }
            card.OverlayCardNo.Add(DesCard.NumberInDeck);
        }
        public string toString()
        {
            int count = 0;
            string text = "";
            foreach (Region region in AllRegions)
            {
                if (count != 0)
                {
                    text += ",";
                }
                text += region.toString();
                count++;
            }
            return text;
        }
    }
}
