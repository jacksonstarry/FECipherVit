using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FECipherVit
{
    public class MsgProcessor
    {
        public MsgProcessor(FECipherVit _Owner)
        {
            Owner = _Owner;
        }
        FECipherVit Owner;
        string RcvBuf = "";
        public void Send(string type, string text)
        {
            if (Owner.PointedOutCardPic != null)
            {
                Owner.PointedOutCardPic.Invalidate();
                Owner.PointedOutCardPic = null;
            }

            string code;
            List<string> allcards = Owner.GetAllCardsInfo();
            if (text != "")
            {
                text = Environment.NewLine + Environment.NewLine + Owner.PlayerName + " " + System.DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine + text;
            }

            //Combining
            code = type + "#";
            for (int i = 0; i < allcards.Count; i++)
            {
                if (i != 0)
                {
                    code += ";";
                }
                code += allcards[i];
            }
            code += "#";
            code += text;

            //Sending
            if (Owner.connection.connected)
            {
                try
                {
                    Owner.socket.Send("☻" + code + "☂");
                }
                catch (Exception ecp)
                {
                    MessageBox.Show(ecp.Message, "错误");
                    return;
                }
            }

            //Display
            if (text != "")
            {
                Owner.UpdateGetMsgTextBox(text);
            }
        }
        public void SendSecret(string type, List<string> contents, string text)
        {
            switch (type)
            {
                case "MyName":
                    if (Owner.connection.connected)
                    {
                        Owner.socket.Send("☻MyName#null#" + text + "☂");
                    }
                    break;
                case "PointOut":
                    text = Environment.NewLine + Environment.NewLine + Owner.PlayerName + " " + System.DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine + text;
                    if (Owner.connection.connected)
                    {
                        Owner.socket.Send("☻PointOut#" + contents[0] + ";" + contents[1] + "#" + text + "☂");
                    }
                    Owner.UpdateGetMsgTextBox(text);
                    break;
            }
        }
        public void Receive(string code)
        {
            Owner.Allinfo += code;
            if (RcvBuf!="")
            {
                if(code.Contains("☂"))
                {
                    RcvBuf += code;
                    string Buf = RcvBuf;
                    RcvBuf = "";
                    Receive(Buf);
                    return;
                }
            }
            if (code.Contains("☂☻"))
            {
                int posEmoji = code.IndexOf("☂☻");
                string code1 = code.Substring(0, posEmoji + 1);
                string code2 = code.Substring(posEmoji + 1);
                Receive(code1);
                Receive(code2);
                return;
            }
            if(!code.Contains("☂"))
            {
                RcvBuf += code;
                return;
            }
            code = code.Trim('☂', '☻'); 

            if (Owner.PointedOutCardPic != null)
            {
                Owner.PointedOutCardPic.Invalidate();
                Owner.PointedOutCardPic = null;
            }

            //Owner.Allinfo += Environment.NewLine + code; // for debug
            if (!Owner.EverReceived)
            {
                Owner.msgProcessor.SendSecret("MyName", null, Owner.PlayerName);
                Owner.EverReceived = true;
            }
            string type;
            List<string> contents = new List<string>();
            string text;

            //Dividing
            int pos = 0;
            int pos2;
            pos = code.IndexOf("#", pos);
            pos2 = code.IndexOf("#", pos + 1);

            if (pos == -1 || pos2 == -1)
            {
                return;
            }

            type = code.Substring(0, pos);
            string contents_temp = code.Substring(pos + 1, pos2 - pos - 1);
            contents.AddRange(contents_temp.Split(new string[] { ";" }, StringSplitOptions.None));
            text = code.Substring(pos2 + 1);

            switch (type)
            {
                case "MyName":
                    if (Owner.RivalName == "")
                    {
                        Owner.RivalName = text;
                        Owner.UpdateGetMsgTextBox(Environment.NewLine + "##对手为：" + text);
                    }
                    break;
                case "PointOut":
                    switch(contents[0])
                    {
                        case "SenderFront":
                            Owner.PointedOutCardPic = Owner.RivalFrontFieldCardPics[Convert.ToInt32(contents[1])];
                            break;
                        case "SenderBack":
                            Owner.PointedOutCardPic = Owner.RivalBackFieldCardPics[Convert.ToInt32(contents[1])];
                            break;
                        case "ReceiverFront":
                            Owner.PointedOutCardPic = Owner.FrontFieldCardPics[Convert.ToInt32(contents[1])];
                            break;
                        case "ReceiverBack":
                            Owner.PointedOutCardPic = Owner.BackFieldCardPics[Convert.ToInt32(contents[1])];
                            break;
                    }
                    Owner.BorderPainter = Owner.PointedOutCardPic.CreateGraphics();
                    Rectangle rect = new Rectangle(Owner.PointedOutCardPic.ClientRectangle.X, Owner.PointedOutCardPic.ClientRectangle.Y,
                                                     Owner.PointedOutCardPic.ClientRectangle.X + Owner.PointedOutCardPic.ClientRectangle.Width - 1,
                                                     Owner.PointedOutCardPic.ClientRectangle.Y + Owner.PointedOutCardPic.ClientRectangle.Height - 1);
                    Owner.BorderPainter.DrawRectangle(new Pen(Color.White, 6), rect);
                    Owner.UpdateGetMsgTextBox(text);
                    break;
                default:
                    //Decoding
                    ReceiveAllCards(contents);
                    Owner.RivalRenew();

                    if (Owner.RivalName == "")
                    {
                        int pos3 = text.IndexOf(" ");
                        Owner.RivalName = text.Substring(0, pos3).Replace(Environment.NewLine, "");
                    }

                    //Display
                    if (text != "")
                    {
                        Owner.UpdateGetMsgTextBox(text);
                    }
                    break;
            }
        }

        public void ReceiveAllCards(List<string> contents)
        {
            Owner.Rival.Deck.CardNum = Convert.ToInt32(contents[contents.IndexOf("[Deck]") + 1]);
            Owner.Rival.Deck.CardList.Clear();
            if (contents[contents.IndexOf("[Deck]") + 2] != "-1")
            {
                int serialno = Convert.ToInt32(contents[contents.IndexOf("[Deck]") + 2]);
                Owner.Rival.Deck.CardList.Add(new Card(-1, serialno, Owner.CardData[serialno][4], Convert.ToInt32(Owner.CardData[serialno][13]), Convert.ToInt32(Owner.CardData[serialno][14]), Owner.Rival));
                Owner.Rival.Deck.CardList[0].Visible = true;
            }
            else
            {
                Owner.Rival.Deck.CardList.Add(new Card(-1, -1, "", -1, -1, Owner.Rival)); ;
            }
            Owner.Rival.Hand.CardNum = Convert.ToInt32(contents[contents.IndexOf("[Hand]") + 1]);
            Owner.Rival.Grave.CardList.Clear();
            for (int i = 0; i < Convert.ToInt32(contents[contents.IndexOf("[Grave]") + 1]); i++)
            {
                int serialno = Convert.ToInt32(contents[contents.IndexOf("[Grave]") + 2 + i]);
                Owner.Rival.Grave.CardList.Add(new Card(-1, serialno, Owner.CardData[serialno][4], Convert.ToInt32(Owner.CardData[serialno][13]), Convert.ToInt32(Owner.CardData[serialno][14]), Owner.Rival));
            }
            Owner.Rival.Support.CardList.Clear();
            if (contents[contents.IndexOf("[Support]") + 1] != "-1")
            {
                int serialno = Convert.ToInt32(contents[contents.IndexOf("[Support]") + 1]);
                Owner.Rival.Support.CardList.Add(new Card(-1, serialno, Owner.CardData[serialno][4], Convert.ToInt32(Owner.CardData[serialno][13]), Convert.ToInt32(Owner.CardData[serialno][14]), Owner.Rival));
            }
            Owner.Rival.Kizuna.CardList.Clear();
            for (int i = 0; i < Convert.ToInt32(contents[contents.IndexOf("[Kizuna]") + 1]); i++)
            {
                bool Reversed = false;
                int serialno;
                if (contents[contents.IndexOf("[Kizuna]") + 2 + i].Substring(0, 1) == "!")
                {
                    Reversed = true;
                    serialno = Convert.ToInt32(contents[contents.IndexOf("[Kizuna]") + 2 + i].Substring(1));
                }
                else
                {
                    serialno = Convert.ToInt32(contents[contents.IndexOf("[Kizuna]") + 2 + i]);
                }
                Card temp = new Card(-1, serialno, Owner.CardData[serialno][4], Convert.ToInt32(Owner.CardData[serialno][13]), Convert.ToInt32(Owner.CardData[serialno][14]), Owner.Rival);
                if (!Reversed)
                {
                    temp.FrontShown = true;
                }
                temp.IsHorizontal = true;
                Owner.Rival.Kizuna.CardList.Add(temp);
            }
            Owner.Rival.KizunaUsed.CardList.Clear();
            for (int i = 0; i < Convert.ToInt32(contents[contents.IndexOf("[KizunaUsed]") + 1]); i++)
            {
                bool Reversed = false;
                int serialno;
                if (contents[contents.IndexOf("[KizunaUsed]") + 2 + i].Substring(0, 1) == "!")
                {
                    Reversed = true;
                    serialno = Convert.ToInt32(contents[contents.IndexOf("[KizunaUsed]") + 2 + i].Substring(1));
                }
                else
                {
                    serialno = Convert.ToInt32(contents[contents.IndexOf("[KizunaUsed]") + 2 + i]);
                }
                Card temp = new Card(-1, serialno, Owner.CardData[serialno][4], Convert.ToInt32(Owner.CardData[serialno][13]), Convert.ToInt32(Owner.CardData[serialno][14]), Owner.Rival);
                if (!Reversed)
                {
                    temp.FrontShown = true;
                }
                temp.IsHorizontal = true;
                Owner.Rival.KizunaUsed.CardList.Add(temp);
            }
            Owner.Rival.Orb.CardNum = Convert.ToInt32(contents[contents.IndexOf("[Orb]") + 1]);
            Owner.Rival.FrontField.CardList.Clear();
            for (int i = 0; i < Convert.ToInt32(contents[contents.IndexOf("[FrontField]") + 1]); i++)
            {
                bool Reversed = false;
                bool IsHorizontal = false;
                int serialno;
                string serialnotemp = contents[contents.IndexOf("[FrontField]") + 2 + i];
                if (serialnotemp.Substring(0, 1) == "!")
                {
                    Reversed = true;
                    serialnotemp = serialnotemp.Substring(1);
                }
                if (serialnotemp.Substring(0, 1) == "@")
                {
                    IsHorizontal = true;
                    serialnotemp = serialnotemp.Substring(1);
                }
                serialno = Convert.ToInt32(serialnotemp);

                Card temp = new Card(-1, serialno, Owner.CardData[serialno][4], Convert.ToInt32(Owner.CardData[serialno][13]), Convert.ToInt32(Owner.CardData[serialno][14]), Owner.Rival);
                if (Reversed)
                {
                    temp.FrontShown = false;
                }
                if (IsHorizontal)
                {
                    temp.IsHorizontal = true;
                }
                Owner.Rival.FrontField.CardList.Add(temp);
            }
            Owner.Rival.BackField.CardList.Clear();
            for (int i = 0; i < Convert.ToInt32(contents[contents.IndexOf("[BackField]") + 1]); i++)
            {
                bool Reversed = false;
                bool IsHorizontal = false;
                int serialno;
                string serialnotemp = contents[contents.IndexOf("[BackField]") + 2 + i];
                if (serialnotemp.Substring(0, 1) == "!")
                {
                    Reversed = true;
                    serialnotemp = serialnotemp.Substring(1);
                }
                if (serialnotemp.Substring(0, 1) == "@")
                {
                    IsHorizontal = true;
                    serialnotemp = serialnotemp.Substring(1);
                }
                serialno = Convert.ToInt32(serialnotemp);

                Card temp = new Card(-1, serialno, Owner.CardData[serialno][4], Convert.ToInt32(Owner.CardData[serialno][13]), Convert.ToInt32(Owner.CardData[serialno][14]), Owner.Rival);
                if (Reversed)
                {
                    temp.FrontShown = false;
                }
                if (IsHorizontal)
                {
                    temp.IsHorizontal = true;
                }
                Owner.Rival.BackField.CardList.Add(temp);
            }
            Owner.Rival.Overlay.CardList.Clear();
            for (int i = 0; i < Convert.ToInt32(contents[contents.IndexOf("[Overlay]") + 1]); i++)
            {
                int serialno = Convert.ToInt32(contents[contents.IndexOf("[Overlay]") + 2 + i]);
                Owner.Rival.Overlay.CardList.Add(new Card(-1, serialno, Owner.CardData[serialno][4], Convert.ToInt32(Owner.CardData[serialno][13]), Convert.ToInt32(Owner.CardData[serialno][14]), Owner.Rival));
            }
        }
    }
}
