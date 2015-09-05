using NetAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FECipherVit
{
    public partial class FECipherVit : Form
    {
        public FECipherVit()
        {
            InitializeComponent();
        }

        #region Initialization
        public int DatabaseVer = 20150828;
        public List<string[]> CardData;
        public User Player;
        public User Rival;
        string DeckFilename = "";
        public string PlayerName = "";
        public string RivalName = "";
        //public string Allinfo = "";
        public bool GameOn = false;
        List<CardPic> HandCardPics = new List<CardPic>();
        List<CardPic> OrbCardPics = new List<CardPic>();
        public List<CardPic> FrontFieldCardPics = new List<CardPic>();
        public List<CardPic> BackFieldCardPics = new List<CardPic>();
        List<CardPic> KizunaCardPics = new List<CardPic>();
        List<CardPic> KizunaUsedCardPics = new List<CardPic>();
        CardPic DeckCardPic;
        CardPic GraveCardPic;
        CardPic SupportCardPic;
        List<CardPic> RivalOrbCardPics = new List<CardPic>();
        public List<CardPic> RivalFrontFieldCardPics = new List<CardPic>();
        public List<CardPic> RivalBackFieldCardPics = new List<CardPic>();
        List<CardPic> RivalKizunaCardPics = new List<CardPic>();
        List<CardPic> RivalKizunaUsedCardPics = new List<CardPic>();
        CardPic RivalDeckCardPic;
        CardPic RivalGraveCardPic;
        CardPic RivalSupportCardPic;
        public static Control CardPicClicked = null;

        public Graphics BorderPainter;
        public CardPic PointedOutCardPic;

        string report = "创建时间：" + DateTime.Now.ToString();

        public Connection connection;
        public SocketFunc socket;
        public System.Action<string> ReceiveAction;
        public System.Action AccessAction;
        public MsgProcessor msgProcessor;
        public bool EverReceived;

        ContextMenuStrip contextMenuStrip_Card;
        ContextMenuStrip contextMenuStrip_RivalCard;
        ToolStripMenuItem 置于前卫区ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 置于后卫区ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 查看区域ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 展示ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 其他ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 置于退避区ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 置于羁绊区ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 置于支援区ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 置于宝玉区ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 加入卡组ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 查看对手区域ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 加入卡组并切洗ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 置于卡组顶端ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 置于卡组底端ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 加入手牌ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 攻击ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 移动ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 翻面ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 横置竖置ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 升级转职ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 发动能力ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 发动支援能力ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 指定为对象ToolStripMenuItem = new ToolStripMenuItem();
        ToolStripMenuItem 指定对手卡为对象ToolStripMenuItem = new ToolStripMenuItem(); 
        #endregion

        #region InfoUpdate
        private void FECipherVit_Load(object sender, EventArgs e)
        {
            File.Delete("decktemp");
            Player = new User();
            Rival = new User();
            msgProcessor = new MsgProcessor(this);
            string[] CardDataTemp = File.ReadAllLines("CardData.fe0db", Encoding.UTF8);
            CardData = new List<string[]>();
            foreach (string carddatumtemp in CardDataTemp)
            {
                string[] temp = carddatumtemp.Split(new char[] { ',' }, StringSplitOptions.None);
                CardData.Add(temp);
            }
            CardData.Sort(delegate (string[] x, string[] y)
            {
                return Convert.ToInt32(x[0]) - Convert.ToInt32(y[0]);
            });
            try
            {
                pictureBoxCardInfo.Image = Image.FromFile(@"img/back.jpg");
            }
            catch
            {
                pictureBoxCardInfo.Image = pictureBoxCardInfo.ErrorImage;
            }
            ConnectionRenew();
            connection = new Connection(this);
            connection.ShowDialog();
            ContextMenuStripRenew();
        }
        void ContextMenuStripRenew()
        {
            contextMenuStrip_Card = new ContextMenuStrip();
            this.contextMenuStrip_Card.Name = "contextMenuStrip_Card";
            this.contextMenuStrip_Card.Size = new System.Drawing.Size(149, 378);
            this.contextMenuStrip_Card.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Card_Opening);
            置于前卫区ToolStripMenuItem = new ToolStripMenuItem();
            this.置于前卫区ToolStripMenuItem.Name = "置于前卫区ToolStripMenuItem";
            this.置于前卫区ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.置于前卫区ToolStripMenuItem.Text = "置于前卫区";
            this.置于前卫区ToolStripMenuItem.Click += new System.EventHandler(this.置于前卫区ToolStripMenuItem_Click);
            置于后卫区ToolStripMenuItem = new ToolStripMenuItem();
            this.置于后卫区ToolStripMenuItem.Name = "置于后卫区ToolStripMenuItem";
            this.置于后卫区ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.置于后卫区ToolStripMenuItem.Text = "置于后卫区";
            this.置于后卫区ToolStripMenuItem.Click += new System.EventHandler(this.置于后卫区ToolStripMenuItem_Click);
            查看区域ToolStripMenuItem = new ToolStripMenuItem();
            this.查看区域ToolStripMenuItem.Name = "查看区域ToolStripMenuItem";
            this.查看区域ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.查看区域ToolStripMenuItem.Text = "查看区域";
            this.查看区域ToolStripMenuItem.Click += new System.EventHandler(this.查看区域ToolStripMenuItem_Click);

            展示ToolStripMenuItem = new ToolStripMenuItem();
            this.展示ToolStripMenuItem.Name = "展示ToolStripMenuItem";
            this.展示ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.展示ToolStripMenuItem.Text = "展示";
            this.展示ToolStripMenuItem.Click += new System.EventHandler(this.展示ToolStripMenuItem_Click);
            其他ToolStripMenuItem = new ToolStripMenuItem();
            this.其他ToolStripMenuItem.Name = "其他ToolStripMenuItem";
            this.其他ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.其他ToolStripMenuItem.Text = "其他";
            置于退避区ToolStripMenuItem = new ToolStripMenuItem();
            this.置于退避区ToolStripMenuItem.Name = "置于退避区ToolStripMenuItem";
            this.置于退避区ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.置于退避区ToolStripMenuItem.Text = "置于退避区";
            this.置于退避区ToolStripMenuItem.Click += new System.EventHandler(this.置于退避区ToolStripMenuItem_Click);
            置于羁绊区ToolStripMenuItem = new ToolStripMenuItem();
            this.置于羁绊区ToolStripMenuItem.Name = "置于羁绊区ToolStripMenuItem";
            this.置于羁绊区ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.置于羁绊区ToolStripMenuItem.Text = "置于羁绊区";
            this.置于羁绊区ToolStripMenuItem.Click += new System.EventHandler(this.置于羁绊区ToolStripMenuItem_Click);
            置于支援区ToolStripMenuItem = new ToolStripMenuItem();
            this.置于支援区ToolStripMenuItem.Name = "置于支援区ToolStripMenuItem";
            this.置于支援区ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.置于支援区ToolStripMenuItem.Text = "置于支援区";
            this.置于支援区ToolStripMenuItem.Click += new System.EventHandler(this.置于支援区ToolStripMenuItem_Click);
            置于宝玉区ToolStripMenuItem = new ToolStripMenuItem();
            this.置于宝玉区ToolStripMenuItem.Name = "置于宝玉区ToolStripMenuItem";
            this.置于宝玉区ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.置于宝玉区ToolStripMenuItem.Text = "置于宝玉区";
            this.置于宝玉区ToolStripMenuItem.Click += new System.EventHandler(this.置于宝玉区ToolStripMenuItem_Click);
            加入卡组并切洗ToolStripMenuItem = new ToolStripMenuItem();
            this.加入卡组并切洗ToolStripMenuItem.Name = "加入卡组并切洗ToolStripMenuItem";
            this.加入卡组并切洗ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.加入卡组并切洗ToolStripMenuItem.Text = "加入卡组并切洗";
            this.加入卡组并切洗ToolStripMenuItem.Click += new System.EventHandler(this.加入卡组并切洗ToolStripMenuItem_Click);
            置于卡组顶端ToolStripMenuItem = new ToolStripMenuItem();
            this.置于卡组顶端ToolStripMenuItem.Name = "置于卡组顶端ToolStripMenuItem";
            this.置于卡组顶端ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.置于卡组顶端ToolStripMenuItem.Text = "置于卡组顶端";
            this.置于卡组顶端ToolStripMenuItem.Click += new System.EventHandler(this.置于卡组顶端ToolStripMenuItem_Click);
            置于卡组底端ToolStripMenuItem = new ToolStripMenuItem();
            this.置于卡组底端ToolStripMenuItem.Name = "置于卡组底端ToolStripMenuItem";
            this.置于卡组底端ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.置于卡组底端ToolStripMenuItem.Text = "置于卡组底端";
            this.置于卡组底端ToolStripMenuItem.Click += new System.EventHandler(this.置于卡组底端ToolStripMenuItem_Click);
            加入卡组ToolStripMenuItem = new ToolStripMenuItem();
            this.加入卡组ToolStripMenuItem.Name = "加入卡组ToolStripMenuItem";
            this.加入卡组ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.加入卡组ToolStripMenuItem.Text = "加入卡组";
            this.加入卡组ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                加入卡组并切洗ToolStripMenuItem,
                置于卡组顶端ToolStripMenuItem,
                置于卡组底端ToolStripMenuItem });
            加入手牌ToolStripMenuItem = new ToolStripMenuItem();
            this.加入手牌ToolStripMenuItem.Name = "加入手牌ToolStripMenuItem";
            this.加入手牌ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.加入手牌ToolStripMenuItem.Text = "加入手牌";
            this.加入手牌ToolStripMenuItem.Click += new System.EventHandler(this.加入手牌ToolStripMenuItem_Click);
            攻击ToolStripMenuItem = new ToolStripMenuItem();
            this.攻击ToolStripMenuItem.Name = "攻击ToolStripMenuItem";
            this.攻击ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.攻击ToolStripMenuItem.Text = "攻击";
            this.攻击ToolStripMenuItem.Click += new System.EventHandler(this.攻击ToolStripMenuItem_Click);
            移动ToolStripMenuItem = new ToolStripMenuItem();
            this.移动ToolStripMenuItem.Name = "移动ToolStripMenuItem";
            this.移动ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.移动ToolStripMenuItem.Text = "移动";
            this.移动ToolStripMenuItem.Click += new System.EventHandler(this.移动ToolStripMenuItem_Click);
            翻面ToolStripMenuItem = new ToolStripMenuItem();
            this.翻面ToolStripMenuItem.Name = "翻面ToolStripMenuItem";
            this.翻面ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.翻面ToolStripMenuItem.Text = "翻面";
            this.翻面ToolStripMenuItem.Click += new System.EventHandler(this.翻面ToolStripMenuItem_Click);
            横置竖置ToolStripMenuItem = new ToolStripMenuItem();
            this.横置竖置ToolStripMenuItem.Name = "横置竖置ToolStripMenuItem";
            this.横置竖置ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.横置竖置ToolStripMenuItem.Text = "横置/竖置";
            this.横置竖置ToolStripMenuItem.Click += new System.EventHandler(this.横置竖置ToolStripMenuItem_Click);
            升级转职ToolStripMenuItem = new ToolStripMenuItem();
            this.升级转职ToolStripMenuItem.Name = "升级转职ToolStripMenuItem";
            this.升级转职ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.升级转职ToolStripMenuItem.Text = "升级/转职";
            this.升级转职ToolStripMenuItem.Click += new System.EventHandler(this.升级转职ToolStripMenuItem_Click);
            发动能力ToolStripMenuItem = new ToolStripMenuItem();
            this.发动能力ToolStripMenuItem.Name = "发动能力ToolStripMenuItem";
            this.发动能力ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.发动能力ToolStripMenuItem.Text = "发动能力";
            this.发动能力ToolStripMenuItem.Click += new System.EventHandler(this.发动能力ToolStripMenuItem_Click);
            发动支援能力ToolStripMenuItem = new ToolStripMenuItem();
            this.发动支援能力ToolStripMenuItem.Name = "发动支援能力ToolStripMenuItem";
            this.发动支援能力ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.发动支援能力ToolStripMenuItem.Text = "发动支援能力";
            this.发动支援能力ToolStripMenuItem.Click += new System.EventHandler(this.发动支援能力ToolStripMenuItem_Click);
            指定为对象ToolStripMenuItem = new ToolStripMenuItem();
            this.指定为对象ToolStripMenuItem.Name = "指定为对象ToolStripMenuItem";
            this.指定为对象ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.指定为对象ToolStripMenuItem.Text = "指定为对象";
            this.指定为对象ToolStripMenuItem.Click += new System.EventHandler(this.指定为对象ToolStripMenuItem_Click);

            this.contextMenuStrip_Card.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.抽卡ToolStripMenuItem,
                        this.抽复数张卡ToolStripMenuItem,
                        this.查看区域ToolStripMenuItem,
                        this.展示ToolStripMenuItem,
                        this.其他ToolStripMenuItem});
            this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.置于前卫区ToolStripMenuItem,
                        this.置于后卫区ToolStripMenuItem,
                        this.置于退避区ToolStripMenuItem,
                        this.置于羁绊区ToolStripMenuItem,
                        this.置于支援区ToolStripMenuItem,
                        this.置于宝玉区ToolStripMenuItem });

        }
        void RivalContextMenuStripRenew()
        {
            contextMenuStrip_RivalCard = new ContextMenuStrip();
            this.contextMenuStrip_RivalCard.Name = "contextMenuStrip_RivalCard";
            this.contextMenuStrip_RivalCard.Size = new System.Drawing.Size(149, 378);
            this.contextMenuStrip_RivalCard.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_RivalCard_Opening);
            查看对手区域ToolStripMenuItem = new ToolStripMenuItem();
            this.查看对手区域ToolStripMenuItem.Name = "查看对手区域ToolStripMenuItem";
            this.查看对手区域ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.查看对手区域ToolStripMenuItem.Text = "查看对手区域";
            this.查看对手区域ToolStripMenuItem.Click += new System.EventHandler(this.查看对手区域ToolStripMenuItem_Click);
            指定对手卡为对象ToolStripMenuItem = new ToolStripMenuItem();
            this.指定对手卡为对象ToolStripMenuItem.Name = "指定对手卡为对象ToolStripMenuItem";
            this.指定对手卡为对象ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.指定对手卡为对象ToolStripMenuItem.Text = "指定对手卡为对象";
            this.指定对手卡为对象ToolStripMenuItem.Click += new System.EventHandler(this.指定为对象ToolStripMenuItem_Click);
            this.contextMenuStrip_RivalCard.Items.Add(查看对手区域ToolStripMenuItem);
        }
        void Reset()
        {
            if (DeckFilename != "")
            {
                string[] DeckContents = File.ReadAllLines(DeckFilename);
                int CardNum = 0;
                Player = new User();
                foreach (string CardSerialNo in DeckContents)
                {
                    if (CardSerialNo != "")
                    {
                        string[] CardDataInfo = CardData[Convert.ToInt32(CardSerialNo)];
                        Player.Deck.CardList.Add(new Card(CardNum, Convert.ToInt32(CardSerialNo), CardDataInfo[4], Convert.ToInt32(CardDataInfo[13]), Convert.ToInt32(CardDataInfo[14]), Player));
                        CardNum++;
                    }
                }
                buttonGameOn.Visible = true;
                buttonTurnStart.Visible = false;
                buttonTurnEnd.Visible = false;
                buttonUseKizuna.Visible = false;
                buttonSupport.Visible = false;
                button_CriticalAttack.Visible = false;
                button_Miss.Visible = false;
                buttonGameOn.Enabled = true;
                游戏开始ToolStripMenuItem.Enabled = true;
                label_RivalHandLabel.Visible = false;
                label_RivalHandTotal.Visible = false;
                动作ToolStripMenuItem.Enabled = false;
                导出场面信息ToolStripMenuItem.Enabled = false;
                GameOn = false;
                try
                {
                    pictureBoxCardInfo.Image = Image.FromFile(@"img/back.jpg");
                }
                catch
                {
                    pictureBoxCardInfo.Image = pictureBoxCardInfo.ErrorImage;
                }
                textBoxCardInfo.Text = "";
            }
            Renew();
        }
        public void Renew()
        {
            if (contextMenuStrip_Card != null)
            {
                contextMenuStrip_Card.Dispose();
            }
            ContextMenuStripRenew();
            GC.Collect();
            HandCardPicsRenew();
            OrbCardPicsRenew();
            DeckCardPicRenew();
            GraveCardPicRenew();
            SupportCardPicRenew();
            FrontFieldCardPicsRenew();
            BackFieldCardPicsRenew();
            KizunaCardPicsRenew();
        }
        private void HandCardPicsRenew()
        {
            foreach (PictureBox HandCardPic in HandCardPics)
            {
                panelHand.Controls.Remove(HandCardPic);
                HandCardPic.Dispose();
            }
            HandCardPics = new List<CardPic>();
            int CardCount = Player.Hand.CardList.Count;
            for (int i = 0; i < CardCount; i++)
            {
                HandCardPics.Add(new CardPic(Player.Hand.CardList[i], "Hand"));
                HandCardPics[i].ContextMenuStrip = contextMenuStrip_Card;
                HandCardPics[i].MouseClick += new MouseEventHandler(Card_MouseClick);
                HandCardPics[i].MouseDown += new MouseEventHandler(Card_MouseDown);
            }
            switch (CardCount)
            {
                case 0:
                    break;
                case 1:
                    HandCardPics[0].Location = new Point(3, 3);
                    break;
                case 2:
                    HandCardPics[0].Location = new Point(3, 3);
                    HandCardPics[1].Location = new Point(93, 3);
                    break;
                case 3:
                    HandCardPics[0].Location = new Point(3, 3);
                    HandCardPics[1].Location = new Point(93, 3);
                    HandCardPics[2].Location = new Point(184, 3);
                    break;
                case 4:
                    HandCardPics[0].Location = new Point(3, 3);
                    HandCardPics[1].Location = new Point(93, 3);
                    HandCardPics[2].Location = new Point(184, 3);
                    HandCardPics[3].Location = new Point(3, 119);
                    break;
                case 5:
                    HandCardPics[0].Location = new Point(3, 3);
                    HandCardPics[1].Location = new Point(93, 3);
                    HandCardPics[2].Location = new Point(184, 3);
                    HandCardPics[3].Location = new Point(3, 119);
                    HandCardPics[4].Location = new Point(93, 119);
                    break;
                case 6:
                    HandCardPics[0].Location = new Point(3, 3);
                    HandCardPics[1].Location = new Point(93, 3);
                    HandCardPics[2].Location = new Point(184, 3);
                    HandCardPics[3].Location = new Point(3, 119);
                    HandCardPics[4].Location = new Point(93, 119);
                    HandCardPics[5].Location = new Point(184, 119);
                    break;
                case 7:
                    HandCardPics[0].Location = new Point(3, 3);
                    HandCardPics[1].Location = new Point(63, 3);
                    HandCardPics[2].Location = new Point(123, 3);
                    HandCardPics[3].Location = new Point(183, 3);
                    HandCardPics[4].Location = new Point(3, 119);
                    HandCardPics[5].Location = new Point(93, 119);
                    HandCardPics[6].Location = new Point(184, 119);
                    break;
                default:
                    if (CardCount % 2 == 1)
                    {
                        for (int i = 0; i < (CardCount + 1) / 2 - 1; i++)
                        {
                            HandCardPics[i].Location = new Point(3 + i * (181 / ((CardCount - 1) / 2)), 3);
                        }
                        HandCardPics[(CardCount + 1) / 2 - 1].Location = new Point(183, 3);
                        for (int i = (CardCount + 1) / 2; i < CardCount - 1; i++)
                        {
                            HandCardPics[i].Location = new Point(3 + (i - (CardCount + 1) / 2) * (181 / ((CardCount - 3) / 2)), 119);
                        }
                        HandCardPics[CardCount - 1].Location = new Point(183, 119);
                    }
                    else
                    {
                        for (int i = 0; i < CardCount / 2 - 1; i++)
                        {
                            HandCardPics[i].Location = new Point(3 + i * (181 / (CardCount / 2 - 1)), 3);
                        }
                        HandCardPics[CardCount / 2 - 1].Location = new Point(183, 3);
                        for (int i = CardCount / 2; i < CardCount - 1; i++)
                        {
                            HandCardPics[i].Location = new Point(3 + (i - CardCount / 2) * (181 / (CardCount / 2 - 1)), 119);
                        }
                        HandCardPics[CardCount - 1].Location = new Point(183, 119);
                    }
                    break;
            }
            foreach (PictureBox HandCardPic in HandCardPics)
            {
                panelHand.Controls.Add(HandCardPic);
                HandCardPic.BringToFront();
            }
        }
        private void OrbCardPicsRenew()
        {
            foreach (PictureBox OrbCardPic in OrbCardPics)
            {
                this.Controls.Remove(OrbCardPic);
                OrbCardPic.Dispose();
            }
            OrbCardPics = new List<CardPic>();
            int CardCount = Player.Orb.CardList.Count;
            for (int i = 0; i < CardCount; i++)
            {
                OrbCardPics.Add(new CardPic(Player.Orb.CardList[i], "Orb"));
                OrbCardPics[i].ReverseToBack();
                OrbCardPics[i].ContextMenuStrip = contextMenuStrip_Card;
                OrbCardPics[i].MouseDown += new MouseEventHandler(Card_MouseDown);
                OrbCardPics[i].MouseDoubleClick += new MouseEventHandler(Orb_MouseDoubleClick);
            }
            for (int i = 0; i < CardCount; i++)
            {
                OrbCardPics[i].Location = new Point(6, 412 + i * 25);
            }
            foreach (PictureBox OrbCardPic in OrbCardPics)
            {
                this.Controls.Add(OrbCardPic);
                OrbCardPic.BringToFront();
            }
        }
        private void FrontFieldCardPicsRenew()
        {
            foreach (PictureBox FrontFieldCardPic in FrontFieldCardPics)
            {
                this.Controls.Remove(FrontFieldCardPic);
                FrontFieldCardPic.Dispose();
            }
            FrontFieldCardPics = new List<CardPic>();
            int CardCount = Player.FrontField.CardList.Count;
            for (int i = 0; i < CardCount; i++)
            {
                FrontFieldCardPics.Add(new CardPic(Player.FrontField.CardList[i], "FrontField"));
                FrontFieldCardPics[i].ContextMenuStrip = contextMenuStrip_Card;
                FrontFieldCardPics[i].MouseClick += new MouseEventHandler(Card_MouseClick);
                FrontFieldCardPics[i].MouseDown += new MouseEventHandler(Card_MouseDown);
                FrontFieldCardPics[i].MouseDoubleClick += new MouseEventHandler(FieldCard_MouseDoubleClick);
            }
            for (int i = 0; i < CardCount; i++)
            {
                FrontFieldCardPics[i].Location = new Point((int)((i + 1) * 400 / (CardCount + 1) - 0.5 * 80) + 107, 378);
                if (Player.FrontField.CardList[i].IsHorizontal)
                {
                    FrontFieldCardPics[i].SetHorizontal();
                }
            }
            foreach (PictureBox FrontFieldCardPic in FrontFieldCardPics)
            {
                this.Controls.Add(FrontFieldCardPic);
                FrontFieldCardPic.BringToFront();
            }
        }
        private void BackFieldCardPicsRenew()
        {
            foreach (PictureBox BackFieldCardPic in BackFieldCardPics)
            {
                this.Controls.Remove(BackFieldCardPic);
                BackFieldCardPic.Dispose();
            }
            BackFieldCardPics = new List<CardPic>();
            int CardCount = Player.BackField.CardList.Count;
            for (int i = 0; i < CardCount; i++)
            {
                BackFieldCardPics.Add(new CardPic(Player.BackField.CardList[i], "BackField"));
                BackFieldCardPics[i].ContextMenuStrip = contextMenuStrip_Card;
                BackFieldCardPics[i].MouseClick += new MouseEventHandler(Card_MouseClick);
                BackFieldCardPics[i].MouseDown += new MouseEventHandler(Card_MouseDown);
                BackFieldCardPics[i].MouseDoubleClick += new MouseEventHandler(FieldCard_MouseDoubleClick);
            }
            for (int i = 0; i < CardCount; i++)
            {
                BackFieldCardPics[i].Location = new Point((int)((i + 1) * 400 / (CardCount + 1) - 0.5 * 80) + 107, 500);
                if (Player.BackField.CardList[i].IsHorizontal)
                {
                    BackFieldCardPics[i].SetHorizontal();
                }
            }
            foreach (PictureBox BackFieldCardPic in BackFieldCardPics)
            {
                this.Controls.Add(BackFieldCardPic);
                BackFieldCardPic.BringToFront();
            }
        }
        private void KizunaCardPicsRenew()
        {
            //Kizuna
            foreach (PictureBox KizunaCardPic in KizunaCardPics)
            {
                this.Controls.Remove(KizunaCardPic);
                KizunaCardPic.Dispose();
            }
            KizunaCardPics = new List<CardPic>();
            int CardCount = Player.Kizuna.CardList.Count;
            for (int i = 0; i < CardCount; i++)
            {
                KizunaCardPics.Add(new CardPic(Player.Kizuna.CardList[i], "Kizuna"));
                KizunaCardPics[i].ContextMenuStrip = contextMenuStrip_Card;
                KizunaCardPics[i].MouseClick += new MouseEventHandler(Card_MouseClick);
                KizunaCardPics[i].MouseDown += new MouseEventHandler(Card_MouseDown);
            }
            for (int i = 0; i < CardCount; i++)
            {
                if (!Player.Kizuna.CardList[i].FrontShown)
                {
                    KizunaCardPics[i].ReverseToBack();
                }
                KizunaCardPics[i].SetHorizontal();
                if (CardCount + Player.KizunaUsed.CardList.Count <= 15)
                {
                    KizunaCardPics[i].Location = new Point(6 + 20 * i, 634);
                }
                else
                {
                    KizunaCardPics[i].Location = new Point(6 + 388 * i / (CardCount + Player.KizunaUsed.CardList.Count), 634);
                }
            }
            foreach (PictureBox KizunaCardPic in KizunaCardPics)
            {
                this.Controls.Add(KizunaCardPic);
                KizunaCardPic.BringToFront();
            }
            //KizunaUsed
            foreach (PictureBox KizunaUsedCardPic in KizunaUsedCardPics)
            {
                this.Controls.Remove(KizunaUsedCardPic);
                KizunaUsedCardPic.Dispose();
            }
            KizunaUsedCardPics = new List<CardPic>();
            CardCount = Player.KizunaUsed.CardList.Count;
            for (int i = 0; i < CardCount; i++)
            {
                KizunaUsedCardPics.Add(new CardPic(Player.KizunaUsed.CardList[i], "KizunaUsed"));
                KizunaUsedCardPics[i].ContextMenuStrip = contextMenuStrip_Card;
                KizunaUsedCardPics[i].MouseClick += new MouseEventHandler(Card_MouseClick);
                KizunaUsedCardPics[i].MouseDown += new MouseEventHandler(Card_MouseDown);
            }
            for (int i = 0; i < CardCount; i++)
            {
                if (!Player.KizunaUsed.CardList[i].FrontShown)
                {
                    KizunaUsedCardPics[i].ReverseToBack();
                }
                KizunaUsedCardPics[i].SetHorizontal();
                if (CardCount + Player.Kizuna.CardList.Count <= 15)
                {
                    KizunaUsedCardPics[i].Location = new Point(394 - 20 * i, 634);
                }
                else
                {
                    KizunaUsedCardPics[i].Location = new Point(394 - 388 * i / (CardCount + Player.Kizuna.CardList.Count), 634);
                }
            }
            for (int i = KizunaUsedCardPics.Count - 1; i >= 0; i--)
            {
                this.Controls.Add(KizunaUsedCardPics[i]);
                KizunaUsedCardPics[i].BringToFront();
            }
        }
        private void DeckCardPicRenew()
        {
            if (this.Controls.Contains(DeckCardPic))
            {
                this.Controls.Remove(DeckCardPic);
                DeckCardPic.Dispose();
            }
            if (Player.Deck.CardList.Count > 0)
            {
                DeckCardPic = new CardPic(Player.Deck.CardList[0], "Deck");
                DeckCardPic.ReverseToBack();
                DeckCardPic.Location = new Point(522, 478);
                DeckCardPic.ContextMenuStrip = contextMenuStrip_Card;
                DeckCardPic.MouseDown += new MouseEventHandler(Card_MouseDown);
                DeckCardPic.MouseDoubleClick += new MouseEventHandler(Deck_MouseDoubleClick);
                this.Controls.Add(DeckCardPic);
                DeckCardPic.BringToFront();
            }
        }
        private void GraveCardPicRenew()
        {
            if (this.Controls.Contains(GraveCardPic))
            {
                this.Controls.Remove(GraveCardPic);
                GraveCardPic.Dispose();
            }
            if (Player.Grave.CardList.Count > 0)
            {
                GraveCardPic = new CardPic(Player.Grave.CardList[Player.Grave.CardList.Count - 1], "Grave");
                GraveCardPic.Location = new Point(522, 602);
                GraveCardPic.ContextMenuStrip = contextMenuStrip_Card;
                GraveCardPic.MouseClick += new MouseEventHandler(Card_MouseClick);
                GraveCardPic.MouseDown += new MouseEventHandler(Card_MouseDown);
                GraveCardPic.MouseDoubleClick += new MouseEventHandler(Grave_MouseDoubleClick);
                this.Controls.Add(GraveCardPic);
                GraveCardPic.BringToFront();
            }
        }
        private void SupportCardPicRenew()
        {
            if (this.Controls.Contains(SupportCardPic))
            {
                this.Controls.Remove(SupportCardPic);
                SupportCardPic.Dispose();
            }
            if (Player.Support.CardList.Count > 0)
            {
                SupportCardPic = new CardPic(Player.Support.CardList[Player.Support.CardList.Count - 1], "Support");
                SupportCardPic.Location = new Point(522, 359);
                SupportCardPic.ContextMenuStrip = contextMenuStrip_Card;
                SupportCardPic.MouseClick += new MouseEventHandler(Card_MouseClick);
                SupportCardPic.MouseDown += new MouseEventHandler(Card_MouseDown);
                this.Controls.Add(SupportCardPic);
                SupportCardPic.BringToFront();
            }
        }
        public void RivalRenew()
        {
            if (contextMenuStrip_RivalCard != null)
            {
                contextMenuStrip_RivalCard.Dispose();
            }
            RivalContextMenuStripRenew();
            GC.Collect();
            RivalHandCardPicsRenew();
            RivalOrbCardPicsRenew();
            RivalDeckCardPicRenew();
            RivalGraveCardPicRenew();
            RivalSupportCardPicRenew();
            RivalFrontFieldCardPicsRenew();
            RivalBackFieldCardPicsRenew();
            RivalKizunaCardPicsRenew();
        }
        private void RivalHandCardPicsRenew()
        {
            label_RivalHandTotal.Text = Rival.Hand.CardNum.ToString();
        }
        private void RivalOrbCardPicsRenew()
        {
            foreach (PictureBox RivalOrbCardPic in RivalOrbCardPics)
            {
                this.Controls.Remove(RivalOrbCardPic);
                RivalOrbCardPic.Dispose();
            }
            RivalOrbCardPics = new List<CardPic>();
            int CardCount = Rival.Orb.CardNum;
            for (int i = 0; i < CardCount; i++)
            {
                RivalOrbCardPics.Add(new CardPic("RivalOrb"));
                RivalOrbCardPics[i].ContextMenuStrip = contextMenuStrip_RivalCard;
                RivalOrbCardPics[i].MouseDown += new MouseEventHandler(Card_MouseDown);
                RivalOrbCardPics[i].ReverseToBack();
                RivalOrbCardPics[i].Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                RivalOrbCardPics[i].Location = new Point(525, 201 - i * 25);
            }
            foreach (PictureBox RivalOrbCardPic in RivalOrbCardPics)
            {
                this.Controls.Add(RivalOrbCardPic);
                RivalOrbCardPic.BringToFront();
            }
        }
        private void RivalFrontFieldCardPicsRenew()
        {
            foreach (PictureBox RivalFrontFieldCardPic in RivalFrontFieldCardPics)
            {
                this.Controls.Remove(RivalFrontFieldCardPic);
                RivalFrontFieldCardPic.Dispose();
            }
            RivalFrontFieldCardPics = new List<CardPic>();
            int CardCount = Rival.FrontField.CardList.Count;
            for (int i = 0; i < CardCount; i++)
            {
                RivalFrontFieldCardPics.Add(new CardPic(Rival.FrontField.CardList[i], "RivalFrontField"));
                RivalFrontFieldCardPics[i].ContextMenuStrip = contextMenuStrip_RivalCard;
                RivalFrontFieldCardPics[i].MouseClick += new MouseEventHandler(Card_MouseClick);
                RivalFrontFieldCardPics[i].MouseDown += new MouseEventHandler(Card_MouseDown);
                RivalFrontFieldCardPics[i].Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                RivalFrontFieldCardPics[i].Location = new Point((int)(-(i + 1) * 400 / (CardCount + 1) - 0.5 * 80) + 507, 230);
                if (Rival.FrontField.CardList[i].IsHorizontal)
                {
                    RivalFrontFieldCardPics[i].SetHorizontal();
                }
            }
            foreach (PictureBox RivalFrontFieldCardPic in RivalFrontFieldCardPics)
            {
                this.Controls.Add(RivalFrontFieldCardPic);
                RivalFrontFieldCardPic.BringToFront();
            }
        }
        private void RivalBackFieldCardPicsRenew()
        {
            foreach (PictureBox RivalBackFieldCardPic in RivalBackFieldCardPics)
            {
                this.Controls.Remove(RivalBackFieldCardPic);
                RivalBackFieldCardPic.Dispose();
            }
            RivalBackFieldCardPics = new List<CardPic>();
            int CardCount = Rival.BackField.CardList.Count;
            for (int i = 0; i < CardCount; i++)
            {
                RivalBackFieldCardPics.Add(new CardPic(Rival.BackField.CardList[i], "RivalBackField"));
                RivalBackFieldCardPics[i].ContextMenuStrip = contextMenuStrip_RivalCard;
                RivalBackFieldCardPics[i].MouseClick += new MouseEventHandler(Card_MouseClick);
                RivalBackFieldCardPics[i].MouseDown += new MouseEventHandler(Card_MouseDown);
                RivalBackFieldCardPics[i].Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                RivalBackFieldCardPics[i].Location = new Point((int)(-(i + 1) * 400 / (CardCount + 1) - 0.5 * 80) + 507, 108);
                if (Rival.BackField.CardList[i].IsHorizontal)
                {
                    RivalBackFieldCardPics[i].SetHorizontal();
                }
            }
            foreach (PictureBox RivalBackFieldCardPic in RivalBackFieldCardPics)
            {
                this.Controls.Add(RivalBackFieldCardPic);
                RivalBackFieldCardPic.BringToFront();
            }
        }
        private void RivalKizunaCardPicsRenew()
        {
            //RivalKizuna
            foreach (PictureBox RivalKizunaCardPic in RivalKizunaCardPics)
            {
                this.Controls.Remove(RivalKizunaCardPic);
                RivalKizunaCardPic.Dispose();
            }
            RivalKizunaCardPics = new List<CardPic>();
            int CardCount = Rival.Kizuna.CardList.Count;
            for (int i = 0; i < CardCount; i++)
            {
                RivalKizunaCardPics.Add(new CardPic(Rival.Kizuna.CardList[i], "RivalKizuna"));
                RivalKizunaCardPics[i].ContextMenuStrip = contextMenuStrip_RivalCard;
                RivalKizunaCardPics[i].MouseClick += new MouseEventHandler(Card_MouseClick);
                RivalKizunaCardPics[i].MouseDown += new MouseEventHandler(Card_MouseDown);
            }
            for (int i = 0; i < CardCount; i++)
            {
                if (!Rival.Kizuna.CardList[i].FrontShown)
                {
                    RivalKizunaCardPics[i].ReverseToBack();
                }
                RivalKizunaCardPics[i].SetHorizontal();
                RivalKizunaCardPics[i].Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                if (CardCount + Rival.KizunaUsed.CardList.Count <= 15)
                {
                    RivalKizunaCardPics[i].Location = new Point(606 - 112 - 20 * i, 5);
                }
                else
                {
                    RivalKizunaCardPics[i].Location = new Point(606 - 112 - 388 * i / (CardCount + Rival.KizunaUsed.CardList.Count), 5);
                }
            }
            foreach (PictureBox RivalKizunaCardPic in RivalKizunaCardPics)
            {
                this.Controls.Add(RivalKizunaCardPic);
                RivalKizunaCardPic.BringToFront();
            }
            //RivalKizunaUsed
            foreach (PictureBox RivalKizunaUsedCardPic in RivalKizunaUsedCardPics)
            {
                this.Controls.Remove(RivalKizunaUsedCardPic);
                RivalKizunaUsedCardPic.Dispose();
            }
            RivalKizunaUsedCardPics = new List<CardPic>();
            CardCount = Rival.KizunaUsed.CardList.Count;
            for (int i = 0; i < CardCount; i++)
            {
                RivalKizunaUsedCardPics.Add(new CardPic(Rival.KizunaUsed.CardList[i], "RivalKizunaUsed"));
                RivalKizunaUsedCardPics[i].ContextMenuStrip = contextMenuStrip_RivalCard;
                RivalKizunaUsedCardPics[i].MouseClick += new MouseEventHandler(Card_MouseClick);
                RivalKizunaUsedCardPics[i].MouseDown += new MouseEventHandler(Card_MouseDown);
            }
            for (int i = 0; i < CardCount; i++)
            {
                if (!Rival.KizunaUsed.CardList[i].FrontShown)
                {
                    RivalKizunaUsedCardPics[i].ReverseToBack();
                }
                RivalKizunaUsedCardPics[i].SetHorizontal();
                RivalKizunaUsedCardPics[i].Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                if (CardCount + Rival.Kizuna.CardList.Count <= 15)
                {
                    RivalKizunaUsedCardPics[i].Location = new Point(106 + 20 * i, 5);
                }
                else
                {
                    RivalKizunaUsedCardPics[i].Location = new Point(106 + 388 * i / (CardCount + Rival.Kizuna.CardList.Count), 5);
                }
            }
            for (int i = RivalKizunaUsedCardPics.Count - 1; i >= 0; i--)
            {
                this.Controls.Add(RivalKizunaUsedCardPics[i]);
                RivalKizunaUsedCardPics[i].BringToFront();
            }
        }
        private void RivalDeckCardPicRenew()
        {
            if (this.Controls.Contains(RivalDeckCardPic))
            {
                this.Controls.Remove(RivalDeckCardPic);
                RivalDeckCardPic.Dispose();
            }
            if (Rival.Deck.CardNum > 0)
            {
                RivalDeckCardPic = new CardPic("RivalDeck");
                RivalDeckCardPic.ReverseToBack();
                RivalDeckCardPic.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                RivalDeckCardPic.Location = new Point(9, 131);
                RivalDeckCardPic.ContextMenuStrip = contextMenuStrip_RivalCard;
                RivalDeckCardPic.MouseDown += new MouseEventHandler(Card_MouseDown);
                this.Controls.Add(RivalDeckCardPic);
                RivalDeckCardPic.BringToFront();
            }
        }
        private void RivalGraveCardPicRenew()
        {
            if (this.Controls.Contains(RivalGraveCardPic))
            {
                this.Controls.Remove(RivalGraveCardPic);
                RivalGraveCardPic.Dispose();
            }
            if (Rival.Grave.CardList.Count > 0)
            {
                RivalGraveCardPic = new CardPic(Rival.Grave.CardList[Rival.Grave.CardList.Count - 1], "RivalGrave");
                RivalGraveCardPic.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                RivalGraveCardPic.Location = new Point(9, 7);
                RivalGraveCardPic.ContextMenuStrip = contextMenuStrip_RivalCard;
                RivalGraveCardPic.MouseClick += new MouseEventHandler(Card_MouseClick);
                RivalGraveCardPic.MouseDown += new MouseEventHandler(Card_MouseDown);
                RivalGraveCardPic.MouseDoubleClick += new MouseEventHandler(RivalGrave_MouseDoubleClick);
                this.Controls.Add(RivalGraveCardPic);
                RivalGraveCardPic.BringToFront();
            }
        }
        private void RivalSupportCardPicRenew()
        {
            if (this.Controls.Contains(RivalSupportCardPic))
            {
                this.Controls.Remove(RivalSupportCardPic);
                RivalSupportCardPic.Dispose();
            }
            if (Rival.Support.CardList.Count > 0)
            {
                RivalSupportCardPic = new CardPic(Rival.Support.CardList[Rival.Support.CardList.Count - 1], "RivalSupport");
                RivalSupportCardPic.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                RivalSupportCardPic.Location = new Point(9, 248);
                RivalSupportCardPic.MouseClick += new MouseEventHandler(Card_MouseClick);
                this.Controls.Add(RivalSupportCardPic);
                RivalSupportCardPic.BringToFront();
            }
        }
        public void UpdateGetMsgTextBox(string message)
        {
            if (textBoxMsgRcv.Text == "")
            {
                message = message.Replace(Environment.NewLine + Environment.NewLine, "");
            }
            textBoxMsgRcv.AppendText(message);
            textBoxMsgRcv.ScrollToCaret();
            report += message;
        }
        private void TimerConnectStatus_Tick(object sender, EventArgs e)
        {
            if (connection.connected && socket.communicateSocket == null)
            {
                connection.connected = false;
                UpdateGetMsgTextBox(Environment.NewLine + Environment.NewLine + PlayerName + " " + System.DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine + "##对方断开连接。" + Environment.NewLine);
            }
        }
        public void ConnectionRenew()
        {
            //异步建立连接回调
            AccessAction = () =>
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    try
                    {
                        String friendIP = socket.communicateSocket.RemoteEndPoint.ToString();
                        socket.Receive(ReceiveAction);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message, "错误");
                        System.Windows.Forms.Application.Restart();
                        return;
                    }
                    //MessageBox.Show("连接成功", "连接");
                    connection.connected = true;
                    connection.Close();
                    UpdateGetMsgTextBox(connection.textBox_UserName.Text + " " + System.DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine + "##连接成功。" + Environment.NewLine);
                });
            };
            //异步接收消息回调
            ReceiveAction = code =>
            {
                if (connection.connected)
                {
                    textBoxMsgRcv.Invoke((MethodInvoker)delegate ()
                    {
                        msgProcessor.Receive(code);
                    });
                }
            };
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
            string[] CardDataInfo = CardData[thisCard.SerialNo];
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
        private void FECipherVit_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.Delete("decktemp");
            File.Delete("fieldstatustemp");
        }
        #endregion
        #region Buttons
        public void Card_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CardPic thisCardPic = (CardPic)sender;
                CardInfoRenew(thisCardPic.thisCard);
            }
        }
        public void Card_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CardPicClicked = (Control)sender;
            }
        }
        public void Deck_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                抽卡ToolStripMenuItem_Click(null, null);
            }
        }
        public void FieldCard_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CardPicClicked = (Control)sender;
                横置竖置ToolStripMenuItem_Click(null, null);
            }
        }
        public void Orb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CardPicClicked = (Control)sender;
                加入手牌ToolStripMenuItem_Click(null, null);
            }
        }
        public void Grave_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                查看退避区ToolStripMenuItem_Click(null, null);
            }
        }
        public void RivalGrave_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                查看对手退避区ToolStripMenuItem_Click(null, null);
            }
        }
        private void pictureBoxField_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X < 602 && e.X > 522 && e.Y < 590 && e.Y > 478) //Deck
                {
                    if (Player.Deck.CardList.Count == 0 && Player.Grave.CardList.Count > 0)
                    {
                        补充卡组ToolStripMenuItem_Click(null, null);
                    }
                }
            }
        }
        private void buttonGameOn_Click(object sender, EventArgs e)
        {
            Reset();
            int HeroNum;
            int HeroSerialNo;
            if (AppConfig.GetValue("UseFirsrCardAsHero") == "True")
            {
                HeroNum = 0;
            }
            else
            {
                SelectHero SelectHeroFromDeck = new SelectHero(Player, this);
                SelectHeroFromDeck.ShowDialog();
                if (SelectHeroFromDeck.DialogResult == DialogResult.OK)
                {
                    HeroNum = SelectHeroFromDeck.HeroNum;
                }
                else
                {
                    SelectHeroFromDeck.Dispose();
                    return;
                }
                SelectHeroFromDeck.Dispose();
            }
            buttonGameOn.Visible = false;
            buttonTurnStart.Visible = true;
            buttonTurnEnd.Visible = true;
            buttonUseKizuna.Visible = true;
            buttonSupport.Visible = true;
            button_CriticalAttack.Visible = true;
            button_Miss.Visible = true;
            label_RivalHandLabel.Visible = true;
            label_RivalHandTotal.Visible = true;
            label_RivalHandTotal.BringToFront();
            buttonGameOn.Enabled = false;
            游戏开始ToolStripMenuItem.Enabled = false;
            动作ToolStripMenuItem.Enabled = true;
            导出场面信息ToolStripMenuItem.Enabled = true;
            GameOn = true;
            HeroSerialNo = Player.Deck.CardList[HeroNum].SerialNo;
            Player.MoveCard(Player.Deck, HeroNum, Player.FrontField);
            Player.Deck.Shuffle();
            Player.Draw(6);
            for (int i = 0; i < 5; i++)
            {
                Player.MoveCard(Player.Deck, 0, Player.Orb);
            }
            msgProcessor.Send("GameOn", "#游戏开始。主人公为[" + CardData[HeroSerialNo][4] + "]。");
            Renew();
        }
        private void buttonTurnStart_Click(object sender, EventArgs e)
        {
            while (Player.KizunaUsed.CardList.Count > 0)
            {
                Player.MoveCard(Player.KizunaUsed, Player.KizunaUsed.CardList.Count - 1, Player.Kizuna);
            }
            foreach (Card card in Player.FrontField.CardList)
            {
                card.IsHorizontal = false;
            }
            foreach (Card card in Player.BackField.CardList)
            {
                card.IsHorizontal = false;
            }
            msgProcessor.Send("TurnStart", "#回合开始。");
            Renew();
        }
        private void buttonTurnEnd_Click(object sender, EventArgs e)
        {
            msgProcessor.Send("TurnEnd", TranslateAllCardsInfo(GetAllCardsInfo()).Replace("#发送场地信息" + Environment.NewLine, "") + Environment.NewLine + "#回合结束。");
        }
        private void buttonUseKizuna_Click(object sender, EventArgs e)
        {
            if (Player.Kizuna.CardList.Count > 0)
            {
                Player.MoveCard(Player.Kizuna.CardList[Player.Kizuna.CardList.Count - 1], Player.KizunaUsed);
                msgProcessor.Send("UseKizuna", "#右移1张羁绊卡。");
                Renew();
            }
        }
        private void buttonSupport_Click(object sender, EventArgs e)
        {
            if (Player.Support.CardList.Count != 0)
            {
                string cardname = Player.Support.CardList[0].UnitTitle + " " + Player.Support.CardList[0].UnitName;
                Player.MoveCard(Player.Support.CardList[0], Player.Grave);
                msgProcessor.Send("SupportOff", "#将[支援区][" + cardname + "]置于退避区。");
            }
            else
            {
                if (Player.Deck.CardList.Count > 0)
                {
                    Player.MoveCard(Player.Deck.CardList[0], Player.Support);
                    string cardname = Player.Support.CardList[0].UnitTitle + " " + Player.Support.CardList[0].UnitName;
                    msgProcessor.Send("SupportOn", "#支援判定：[" + cardname + "]（支援力" + Player.Support.CardList[0].Support.ToString() + "）。");
                }
            }
            Renew();
        }
        private void buttonMsgSend_Click(object sender, EventArgs e)
        {
            if (textBoxMsgSend.Text != "")
            {
                msgProcessor.Send("Text", ">>  " + textBoxMsgSend.Text);
                textBoxMsgSend.Text = "";
            }
        }
        private void button_CriticalAttack_Click(object sender, EventArgs e)
        {
            msgProcessor.Send("CriticalAttack", "#发动必杀攻击！");
        }
        private void button_Miss_Click(object sender, EventArgs e)
        {
            msgProcessor.Send("Miss", "#发动神速回避！");
        }
        private void textBoxCardInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                msgProcessor.Send("SendCardInfo", "#发送卡片信息：" + Environment.NewLine + textBoxCardInfo.Text);
            }
        }
        private void textBoxMsgSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonMsgSend_Click(null, null);
            }
        }
        private void contextMenuStrip_Card_Opening(object sender, CancelEventArgs e)
        {
            if (contextMenuStrip_Card.SourceControl != null)
            {
                this.contextMenuStrip_Card.Items.Clear();
                this.其他ToolStripMenuItem.DropDownItems.Clear();
                置于前卫区ToolStripMenuItem.Text = "置于前卫区";
                置于后卫区ToolStripMenuItem.Text = "置于后卫区";
                switch (((CardPic)contextMenuStrip_Card.SourceControl).Type)
                {
                    case "Deck":
                        this.contextMenuStrip_Card.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.抽卡ToolStripMenuItem,
                        this.抽复数张卡ToolStripMenuItem,
                        this.查看区域ToolStripMenuItem,
                        this.展示ToolStripMenuItem,
                        this.其他ToolStripMenuItem});
                        查看区域ToolStripMenuItem.Text = "查看卡组：" + Player.Deck.CardList.Count.ToString() + "张";
                        this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.置于前卫区ToolStripMenuItem,
                        this.置于后卫区ToolStripMenuItem,
                        this.置于退避区ToolStripMenuItem,
                        this.置于羁绊区ToolStripMenuItem,
                        this.置于支援区ToolStripMenuItem,
                        this.置于宝玉区ToolStripMenuItem,
                    });
                        break;
                    case "Hand":
                        this.contextMenuStrip_Card.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.置于前卫区ToolStripMenuItem,
                        this.置于后卫区ToolStripMenuItem,
                        this.升级转职ToolStripMenuItem,
                        this.置于退避区ToolStripMenuItem,
                        this.置于羁绊区ToolStripMenuItem,
                        this.展示ToolStripMenuItem,
                        this.其他ToolStripMenuItem});
                        查看区域ToolStripMenuItem.Text = "查看卡组：" + Player.Deck.CardList.Count.ToString() + "张";
                        this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.加入卡组ToolStripMenuItem,
                        this.置于支援区ToolStripMenuItem,
                        this.置于宝玉区ToolStripMenuItem,
                    });
                        置于前卫区ToolStripMenuItem.Text = "出击到前卫区";
                        置于后卫区ToolStripMenuItem.Text = "出击到后卫区";
                        break;
                    case "Grave":
                        this.contextMenuStrip_Card.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.查看区域ToolStripMenuItem,
                        this.其他ToolStripMenuItem});
                        查看区域ToolStripMenuItem.Text = "查看卡组：" + Player.Deck.CardList.Count.ToString() + "张";
                        this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.加入卡组ToolStripMenuItem,
                        this.加入手牌ToolStripMenuItem,
                        this.置于前卫区ToolStripMenuItem,
                        this.置于后卫区ToolStripMenuItem,
                        this.置于羁绊区ToolStripMenuItem,
                        this.置于支援区ToolStripMenuItem,
                        this.置于宝玉区ToolStripMenuItem,
                    });
                        查看区域ToolStripMenuItem.Text = "查看退避区：" + Player.Grave.CardList.Count.ToString() + "张";
                        break;
                    case "Support":
                        this.contextMenuStrip_Card.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.发动支援能力ToolStripMenuItem,
                        this.置于退避区ToolStripMenuItem,
                        this.其他ToolStripMenuItem});
                        this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.加入卡组ToolStripMenuItem,
                        this.加入手牌ToolStripMenuItem,
                        this.置于前卫区ToolStripMenuItem,
                        this.置于后卫区ToolStripMenuItem,
                        this.置于羁绊区ToolStripMenuItem,
                        this.置于宝玉区ToolStripMenuItem,
                    });
                        break;
                    case "Kizuna":
                        this.contextMenuStrip_Card.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.翻面ToolStripMenuItem,
                        this.羁绊卡右移ToolStripMenuItem,
                        this.复数羁绊卡右移ToolStripMenuItem,
                        this.其他ToolStripMenuItem});
                        this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.加入卡组ToolStripMenuItem,
                        this.加入手牌ToolStripMenuItem,
                        this.置于前卫区ToolStripMenuItem,
                        this.置于后卫区ToolStripMenuItem,
                        this.置于退避区ToolStripMenuItem,
                        this.置于支援区ToolStripMenuItem,
                        this.置于宝玉区ToolStripMenuItem });
                        break;
                    case "KizunaUsed":
                        this.contextMenuStrip_Card.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.翻面ToolStripMenuItem,
                        this.羁绊卡右移ToolStripMenuItem,
                        this.复数羁绊卡右移ToolStripMenuItem,
                        this.其他ToolStripMenuItem});
                        this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.加入卡组ToolStripMenuItem,
                        this.加入手牌ToolStripMenuItem,
                        this.置于前卫区ToolStripMenuItem,
                        this.置于后卫区ToolStripMenuItem,
                        this.置于退避区ToolStripMenuItem,
                        this.置于支援区ToolStripMenuItem,
                        this.置于宝玉区ToolStripMenuItem });
                        break;
                    case "Orb":
                        this.contextMenuStrip_Card.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.加入手牌ToolStripMenuItem,
                        this.其他ToolStripMenuItem});
                        this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.加入卡组ToolStripMenuItem,
                        this.置于前卫区ToolStripMenuItem,
                        this.置于后卫区ToolStripMenuItem,
                        this.置于退避区ToolStripMenuItem,
                        this.置于羁绊区ToolStripMenuItem,
                        this.置于支援区ToolStripMenuItem });
                        break;
                    case "FrontField":
                        this.contextMenuStrip_Card.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.攻击ToolStripMenuItem,
                        this.移动ToolStripMenuItem,
                        this.横置竖置ToolStripMenuItem,
                        this.发动能力ToolStripMenuItem,
                        this.指定为对象ToolStripMenuItem,
                        this.置于退避区ToolStripMenuItem,
                        this.其他ToolStripMenuItem});
                        this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.加入卡组ToolStripMenuItem,
                        this.加入手牌ToolStripMenuItem,
                        this.置于羁绊区ToolStripMenuItem,
                        this.置于支援区ToolStripMenuItem,
                        this.置于宝玉区ToolStripMenuItem });
                        break;
                    case "BackField":
                        this.contextMenuStrip_Card.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.攻击ToolStripMenuItem,
                        this.移动ToolStripMenuItem,
                        this.横置竖置ToolStripMenuItem,
                        this.发动能力ToolStripMenuItem,
                        this.指定为对象ToolStripMenuItem,
                        this.置于退避区ToolStripMenuItem,
                        this.其他ToolStripMenuItem});
                        this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                        this.加入卡组ToolStripMenuItem,
                        this.加入手牌ToolStripMenuItem,
                        this.置于羁绊区ToolStripMenuItem,
                        this.置于支援区ToolStripMenuItem,
                        this.置于宝玉区ToolStripMenuItem });
                        break;
                }
            }
        }
        private void contextMenuStrip_RivalCard_Opening(object sender, CancelEventArgs e)
        {
            if (contextMenuStrip_RivalCard.SourceControl != null)
            {
                this.contextMenuStrip_RivalCard.Items.Clear();
                switch (((CardPic)contextMenuStrip_RivalCard.SourceControl).Type)
                {
                    case "RivalDeck":
                        this.contextMenuStrip_RivalCard.Items.Add(查看对手区域ToolStripMenuItem);
                        查看对手区域ToolStripMenuItem.Text = "对手卡组：" + Rival.Deck.CardNum.ToString() + "张";
                        break;
                    case "RivalGrave":
                        this.contextMenuStrip_RivalCard.Items.Add(查看对手区域ToolStripMenuItem);
                        查看对手区域ToolStripMenuItem.Text = "查看对手退避区：" + Rival.Grave.CardList.Count.ToString() + "张";
                        break;
                    case "RivalFrontField":
                        this.contextMenuStrip_RivalCard.Items.Add(指定对手卡为对象ToolStripMenuItem);
                        break;
                    case "RivalBackField":
                        this.contextMenuStrip_RivalCard.Items.Add(指定对手卡为对象ToolStripMenuItem);
                        break;
                    case "RivalOrb":
                        this.contextMenuStrip_RivalCard.Items.Add(查看对手区域ToolStripMenuItem);
                        查看对手区域ToolStripMenuItem.Text = "对手宝玉区：" + Rival.Orb.CardNum.ToString() + "张";
                        break;
                    case "RivalKizuna":
                        this.contextMenuStrip_RivalCard.Items.Add(查看对手区域ToolStripMenuItem);
                        查看对手区域ToolStripMenuItem.Text = "对手羁绊区：" + (Rival.Kizuna.CardList.Count + Rival.KizunaUsed.CardList.Count).ToString() + "张 未使用：" + Rival.Kizuna.CardList.Count.ToString() + "张 未翻面：" + (Rival.Kizuna.CardList.Count - Rival.Kizuna.GetReversedNum() + Rival.KizunaUsed.CardList.Count - Rival.KizunaUsed.GetReversedNum()).ToString() + "张";
                        break;
                    case "RivalKizunaUsed":
                        this.contextMenuStrip_RivalCard.Items.Add(查看对手区域ToolStripMenuItem);
                        查看对手区域ToolStripMenuItem.Text = "对手羁绊区：" + (Rival.Kizuna.CardList.Count + Rival.KizunaUsed.CardList.Count).ToString() + "张 未使用：" + Rival.Kizuna.CardList.Count.ToString() + "张 未翻面：" + (Rival.Kizuna.CardList.Count - Rival.Kizuna.GetReversedNum() + Rival.KizunaUsed.CardList.Count - Rival.KizunaUsed.GetReversedNum()).ToString() + "张";
                        break;
                }
            }
        }
        private void 载入卡组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeckSelect DeckSelect = new DeckSelect(this);
            DeckSelect.ShowDialog();
            if (DeckSelect.DialogResult == DialogResult.OK)
            {
                DeckFilename = DeckSelect.SelectedDeckFilename;
                Reset();
                Renew();
                buttonGameOn.Enabled = true;
                游戏开始ToolStripMenuItem.Enabled = true;
                msgProcessor.Send("SelectDeck", "#载入卡组，共" + Player.Deck.CardList.Count.ToString() + "张卡。");
            }
        }
        private void 切洗手牌ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player.Hand.Shuffle();
            msgProcessor.Send("HandShuffle", "#切洗手牌。");
            HandCardPicsRenew();
        }
        private void 展示全部手牌ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "#展示全部手牌：";
            foreach (Card handcard in Player.Hand.CardList)
            {
                text += Environment.NewLine + "    [" + handcard.UnitTitle + " " + handcard.UnitName + "]";
            }
            msgProcessor.Send("ShowAllHand", text);
        }
        private void 横置竖置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            thisCard.IsHorizontal = !thisCard.IsHorizontal;
            if (thisCard.IsHorizontal)
            {
                msgProcessor.Send("SetHorizontal", "#将[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]横置。");
            }
            else
            {
                msgProcessor.Send("SetVertical", "#将[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]竖置。");
            }
            Renew();
        }
        private void 翻面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            thisCard.FrontShown = !thisCard.FrontShown;
            if (thisCard.FrontShown)
            {
                msgProcessor.Send("UnReverse", "#将[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]翻到正面。");
            }
            else
            {
                msgProcessor.Send("Reverse", "#将[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]翻到背面。");
            }
            Renew();
        }
        private void 移动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            if (thisCard.BelongedRegion().Equals(Player.FrontField))
            {
                Player.MoveCard(thisCard, Player.BackField);
                msgProcessor.Send("Move", "#将[前卫区][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]移动到后卫区。");
            }
            else
            {
                Player.MoveCard(thisCard, Player.FrontField);
                msgProcessor.Send("Move", "#将[后卫区][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]移动到前卫区。");
            }
            Renew();
        }
        private void 进军ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int total = Player.BackField.CardList.Count;
            if (total > 0)
            {
                while (Player.BackField.CardList.Count > 0)
                {
                    Player.MoveCard(Player.BackField, 0, Player.FrontField);
                }
                msgProcessor.Send("MoveAlltoFront", "#将" + total.ToString() + "个单位进军。");
            }
            Renew();
        }
        private void 攻击ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            thisCard.IsHorizontal = true;
            if (PointedOutCardPic == null)
            {
                msgProcessor.Send("Attack", "#[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]进行攻击（攻击力" + thisCard.Power.ToString() + "）。");
            }
            else
            {
                Card PointedOutCard = ((CardPic)PointedOutCardPic).thisCard;
                msgProcessor.Send("Attack", "#[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]（攻击力"+thisCard.Power.ToString() + "）以[" + GetRegionNameInString(PointedOutCard.BelongedRegion()) + "][" + PointedOutCard.UnitTitle + " " + PointedOutCard.UnitName + "]（攻击力" + PointedOutCard.Power.ToString() + "）为对象进行攻击。");
            }
            Renew();
        }
        private void 指定为对象ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PointedOutCardPic != null)
            {
                if (CardPicClicked.Equals(PointedOutCardPic))
                {
                    return;
                }
                PointedOutCardPic.Invalidate();
                PointedOutCardPic = null;
            }
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            if (thisCard != null)
            {
                List<string> temp = new List<string>();
                int num = thisCard.BelongedRegion().CardList.IndexOf(thisCard);
                if (thisCard.BelongedRegion().Equals(Player.FrontField))
                {
                    temp.Add("SenderFront");
                    PointedOutCardPic = FrontFieldCardPics[num];
                }
                else if (thisCard.BelongedRegion().Equals(Player.BackField))
                {
                    temp.Add("SenderBack");
                    PointedOutCardPic = BackFieldCardPics[num];
                }
                else if (thisCard.BelongedRegion().Equals(Rival.FrontField))
                {
                    temp.Add("ReceiverFront");
                    PointedOutCardPic = RivalFrontFieldCardPics[num];
                }
                else
                {
                    temp.Add("ReceiverBack");
                    PointedOutCardPic = RivalBackFieldCardPics[num];
                }
                BorderPainter = PointedOutCardPic.CreateGraphics();
                Rectangle rect = new Rectangle(PointedOutCardPic.ClientRectangle.X, PointedOutCardPic.ClientRectangle.Y,
                                                 PointedOutCardPic.ClientRectangle.X + PointedOutCardPic.ClientRectangle.Width - 1,
                                                 PointedOutCardPic.ClientRectangle.Y + PointedOutCardPic.ClientRectangle.Height - 1);
                BorderPainter.DrawRectangle(new Pen(Color.White, 6), rect);
                temp.Add(num.ToString());
                msgProcessor.SendSecret("PointOut", temp, "#将[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]指定为对象。");
            }
        }
        private void 发动能力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            if (PointedOutCardPic == null)
            {
                if (AppConfig.GetValue("SendSkillDetail") == "True")
                {
                    if (!CardData[thisCard.SerialNo][16].Contains("$$"))
                    {
                        msgProcessor.Send("Skill", "#[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]发动能力：" + CardData[thisCard.SerialNo][16]);
                    }
                    else
                    {
                        using (UseSkill useskill = new UseSkill(CardData[thisCard.SerialNo]))
                        {
                            if (useskill.ShowDialog() == DialogResult.OK)
                            {
                                msgProcessor.Send("Skill", "#[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]发动能力：" + useskill.SelectedSkillContent);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    msgProcessor.Send("Skill", "#[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]发动能力。");
                }
            }
            else
            {
                Card PointedOutCard = ((CardPic)PointedOutCardPic).thisCard;
                if (AppConfig.GetValue("SendSkillDetail") == "True")
                {
                    if (!CardData[thisCard.SerialNo][16].Contains("$$"))
                    {
                        msgProcessor.Send("Skill", "#[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]以[" + GetRegionNameInString(PointedOutCard.BelongedRegion()) + "][" + PointedOutCard.UnitTitle + " " + PointedOutCard.UnitName + "]为对象发动能力：" + CardData[thisCard.SerialNo][16]);
                    }
                    else
                    {
                        using (UseSkill useskill = new UseSkill(CardData[thisCard.SerialNo]))
                        {
                            if (useskill.ShowDialog() == DialogResult.OK)
                            {
                                msgProcessor.Send("Skill", "#[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]以[" + GetRegionNameInString(PointedOutCard.BelongedRegion()) + "][" + PointedOutCard.UnitTitle + " " + PointedOutCard.UnitName + "]为对象发动能力：" + useskill.SelectedSkillContent);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    msgProcessor.Send("Skill", "#[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]以[" + GetRegionNameInString(PointedOutCard.BelongedRegion()) + "][" + PointedOutCard.UnitTitle + " " + PointedOutCard.UnitName + "]为对象发动能力。");
                }
            }
            Renew();
        }
        private void 发动支援能力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            if (PointedOutCardPic == null)
            {
                msgProcessor.Send("SupportSkill", "#[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]发动支援能力：" + Environment.NewLine + CardData[thisCard.SerialNo][17]);
            }
            else
            {
                Card PointedOutCard = ((CardPic)PointedOutCardPic).thisCard;
                msgProcessor.Send("Skill", "#[" + GetRegionNameInString(thisCard.BelongedRegion()) + "][" + thisCard.UnitTitle + " " + thisCard.UnitName + "]以[" + GetRegionNameInString(PointedOutCard.BelongedRegion()) + "][" + PointedOutCard.UnitTitle + " " + PointedOutCard.UnitName + "]发动支援能力：" + Environment.NewLine + CardData[thisCard.SerialNo][17]);
            }
            Renew();
        }
        private void 置于前卫区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
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
                    msgProcessor.Send("Overlay", "#从[" + GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + GetRegionNameInString(OverlayedCard.BelongedRegion()) + "][" + OverlayedCard.UnitTitle + " " + OverlayedCard.UnitName + "]升级/转职为[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]。");
                    Player.OverlayCard(thisCard, CardNoWithSameName);
                }
                else
                {
                    msgProcessor.Send("Summon", "#从[" + GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]出击到前卫区（已存在同角色名的单位）。");
                    Player.MoveCard(thisCard, Player.FrontField);
                }
            }
            else
            {
                msgProcessor.Send("Summon", "#从[" + GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]出击到前卫区。");
                Player.MoveCard(thisCard, Player.FrontField);
            }
            thisCard.FrontShown = true;
            thisCard.IsHorizontal = false;
            msgProcessor.Send("Update", "");
            Renew();
        }
        private void 置于后卫区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
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
                    msgProcessor.Send("Overlay", "#从[" + GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + GetRegionNameInString(OverlayedCard.BelongedRegion()) + "][" + OverlayedCard.UnitTitle + " " + OverlayedCard.UnitName + "]升级/转职为[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]。");
                    Player.OverlayCard(thisCard, CardNoWithSameName);
                }
                else
                {
                    msgProcessor.Send("Summon", "#从[" + GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]出击到后卫区（已存在同角色名的单位）。");
                    Player.MoveCard(thisCard, Player.BackField);
                }
            }
            else
            {
                msgProcessor.Send("Summon", "#从[" + GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]出击到后卫区。");
                Player.MoveCard(thisCard, Player.BackField);
            }
            thisCard.FrontShown = true;
            thisCard.IsHorizontal = false;
            msgProcessor.Send("Update", "");
            Renew();
        }
        private void 升级转职ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
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
                Card OverlayedCard = Player.SearchCard(CardNoWithSameName);
                msgProcessor.Send("Overlay", "#从[" + GetRegionNameInString(thisCard.BelongedRegion()) + "]将[" + GetRegionNameInString(OverlayedCard.BelongedRegion()) + "][" + OverlayedCard.UnitTitle + " " + OverlayedCard.UnitName + "]升级/转职为[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]。");
                Player.OverlayCard(thisCard, CardNoWithSameName);
                thisCard.FrontShown = true;
                thisCard.IsHorizontal = false;
                msgProcessor.Send("Update", "");
                Renew();
            }
            else
            {
                MessageBox.Show("战场上没有同角色名的单位。", "升级/转职");
            }
        }
        private void 加入手牌ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            msgProcessor.Send("ToHand", GetTextOfMovingToRegion(thisCard, "Hand", false));
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
            msgProcessor.Send("Update", "");
            Renew();
        }
        private void 置于退避区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            msgProcessor.Send("ToGrave", GetTextOfMovingToRegion(thisCard, "Grave", false));
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
            msgProcessor.Send("Update", "");
            Renew();
        }
        private void 置于羁绊区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            msgProcessor.Send("ToKizuna", GetTextOfMovingToRegion(thisCard, "Kizuna", false));
            Player.MoveCard(thisCard, Player.Kizuna);
            thisCard.FrontShown = true;
            thisCard.IsHorizontal = true;
            thisCard.Comments = "";
            if (thisCard.OverlayCardNo.Count != 0)
            {
                DialogResult result = MessageBox.Show("这张卡下面的叠放卡将被送入退避区。", "置于羁绊区");
                foreach (int CardNo in thisCard.OverlayCardNo)
                {
                    Player.MoveCard(Player.Overlay.SearchCard(CardNo), Player.Grave);
                }
                thisCard.OverlayCardNo = new List<int>();
            }
            msgProcessor.Send("Update", "");
            Renew();
        }
        private void 置于支援区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            if (Player.Support.CardList.Count != 0)
            {
                MessageBox.Show("支援区已经有卡存在。", "无法移动");
            }
            else
            {
                msgProcessor.Send("ToSupport", GetTextOfMovingToRegion(thisCard, "Support", false));
                Player.MoveCard(thisCard, Player.Support);
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
                msgProcessor.Send("Update", "");
                Renew();
            }
        }
        private void 置于宝玉区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            if (Player.Orb.CardList.Count >= 5)
            {
                MessageBox.Show("宝玉区已满。", "无法移动");
            }
            else
            {
                msgProcessor.Send("ToOrb", GetTextOfMovingToRegion(thisCard, "Orb", false));
                Player.MoveCard(thisCard, Player.Orb);
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
                msgProcessor.Send("Update", "");
                Renew();
            }
        }
        private void 加入卡组并切洗ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            msgProcessor.Send("ToDeckShuffle", GetTextOfMovingToRegion(thisCard, "DeckShuffle", false));
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
            msgProcessor.Send("Update", "");
            Renew();
        }
        private void 置于卡组顶端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            msgProcessor.Send("ToDeckTop", GetTextOfMovingToRegion(thisCard, "DeckTop", false));
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
            msgProcessor.Send("Update", "");
            Renew();
        }
        private void 置于卡组底端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            msgProcessor.Send("ToDeckBottom", GetTextOfMovingToRegion(thisCard, "DeckBottom", false));
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
            msgProcessor.Send("Update", "");
            Renew();
        }
        private void 展示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card thisCard = ((CardPic)CardPicClicked).thisCard;
            if (thisCard.BelongedRegion().Equals(Player.Deck))
            {
                msgProcessor.Send("ShowCard", "#展示卡组顶牌：[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]。");
            }
            msgProcessor.Send("ShowCard", "#展示[" + GetRegionNameInString(thisCard.BelongedRegion()) + "(" + (thisCard.BelongedRegion().CardList.IndexOf(thisCard) + 1).ToString() + ")]：[" + thisCard.UnitTitle + " " + thisCard.UnitName + "]。");
            Renew();
        }
        private void 查看手牌ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CardSetView CheckHandRegion = new CardSetView(Player.Hand, "Hand", Player, this);
            CheckHandRegion.Owner = this;
            CheckHandRegion.ShowDialog();
            CheckHandRegion.Dispose();
        }
        private void 查看区域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStrip_Card.SourceControl != null)
            {
                switch (((CardPic)contextMenuStrip_Card.SourceControl).Type)
                {
                    case "Deck":
                        查看卡组ToolStripMenuItem_Click(null, null);
                        break;
                    case "Grave":
                        查看退避区ToolStripMenuItem_Click(null, null);
                        break;
                }
            }
        }
        private void 重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
            msgProcessor.Send("Reset", "#重置游戏。");
        }
        private void 补充卡组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (Player.Grave.CardList.Count > 0)
            {
                Player.MoveCard(Player.Grave.CardList[0], Player.Deck);
            }
            Player.Deck.Shuffle();
            msgProcessor.Send("ReplenishDeck", "#将退避区的卡放回卡组切洗。");
            Renew();
        }
        private void 查看卡组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            msgProcessor.Send("CheckDeck", "#查看卡组。");
            CardSetView CheckDeckRegion = new CardSetView(Player.Deck, "Deck", Player, this);
            CheckDeckRegion.Owner = this;
            CheckDeckRegion.ShowDialog();
            CheckDeckRegion.Dispose();
        }
        private void 查看退避区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CardSetView CheckGraveRegion = new CardSetView(Player.Grave, "Grave", Player, this);
            CheckGraveRegion.Owner = this;
            CheckGraveRegion.ShowDialog();
            CheckGraveRegion.Dispose();
        }
        private void 抽卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player.Draw();
            msgProcessor.Send("Draw", "#抽卡。");
            Renew();
        }
        private void 抽复数张卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInteger DrawMultipleCards = new GetInteger("抽复数张卡");
            DrawMultipleCards.Owner = this;
            DrawMultipleCards.ShowDialog();
            DrawMultipleCards.Dispose();
        }
        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetConfig setconfig = new SetConfig();
            setconfig.ShowDialog();
            setconfig.Dispose();
        }
        private void 导出战报ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog exportReport = new SaveFileDialog();
            exportReport.Filter = "txt files (*.txt)|*.txt";
            exportReport.FilterIndex = 1;
            exportReport.RestoreDirectory = true;
            if (exportReport.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(exportReport.FileName, report, Encoding.UTF8);
            }
        }
        private void 清除战报ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report = "创建时间：" + DateTime.Now.ToString();
            textBoxMsgRcv.Text = "";
        }
        private void 更换全部手牌ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            msgProcessor.Send("ChangeHand", "#更换全部手牌。");
            int count = 0;
            while (Player.Hand.CardList.Count > 0)
            {
                Player.MoveCard(Player.Hand, 0, Player.Deck);
                count++;
            }
            while (Player.Orb.CardList.Count > 0)
            {
                Player.MoveCard(Player.Orb, 0, Player.Deck);
            }
            Player.Deck.Shuffle();
            Player.Draw(count);
            for (int i = 0; i < 5; i++)
            {
                Player.MoveCard(Player.Deck, 0, Player.Orb);
            }
            Renew();
        }
        private void 发送场地信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> allcards = GetAllCardsInfo();
            msgProcessor.Send("AllCards", TranslateAllCardsInfo(allcards));
        }
        private void 查看对手区域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStrip_RivalCard.SourceControl != null)
            {
                switch (((CardPic)contextMenuStrip_RivalCard.SourceControl).Type)
                {
                    case "RivalGrave":
                        查看对手退避区ToolStripMenuItem_Click(null, null);
                        break;
                    default:
                        break;
                }
            }
        }
        private void 查看对手退避区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Rival.Grave.CardList.Count > 0)
            {
                CardSetView CheckRivalGraveRegion = new CardSetView(Rival.Grave, "RivalGrave", Rival, this);
                CheckRivalGraveRegion.Owner = this;
                CheckRivalGraveRegion.ShowDialog();
                CheckRivalGraveRegion.Dispose();
            }
        }
        private void 复数羁绊卡右移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetInteger DrawMultipleCards = new GetInteger("复数羁绊卡右移");
            DrawMultipleCards.Owner = this;
            DrawMultipleCards.ShowDialog();
            DrawMultipleCards.Dispose();
        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
            about.Dispose();
        }
        private void 发送卡组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "#发送卡组：";
            foreach (Card deckcard in Player.Deck.CardList)
            {
                text += Environment.NewLine + "    [" + deckcard.UnitTitle + " " + deckcard.UnitName + "]";
            }
            msgProcessor.Send("SendDeckContents", text);
        }
        private void 切洗卡组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player.Deck.Shuffle();
            msgProcessor.Send("DeckShuffle", "#切洗卡组。");
        }
        private void 请求猜拳ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int PlayerNum = random.Next(3);
            int RivalNum = random.Next(3);
            string text = "#请求猜拳。" + Environment.NewLine;
            text += PlayerName + " 出了";
            switch (PlayerNum)
            {
                case 0:
                    text += "石头";
                    break;
                case 1:
                    text += "剪刀";
                    break;
                case 2:
                    text += "布";
                    break;
                default:
                    break;
            }
            text += "。" + Environment.NewLine;
            text += RivalName + " 出了";
            switch (RivalNum)
            {
                case 0:
                    text += "石头";
                    break;
                case 1:
                    text += "剪刀";
                    break;
                case 2:
                    text += "布";
                    break;
                default:
                    break;
            }
            text += "。" + Environment.NewLine;
            switch (PlayerNum - RivalNum)
            {
                case 0:
                    text += "平局。";
                    break;
                case -1:
                    text += PlayerName + " 获得胜利，先攻。";
                    break;
                case 2:
                    text += PlayerName + " 获得胜利，先攻。";
                    break;
                default:
                    text += RivalName + " 获得胜利，先攻。";
                    break;
            }
            msgProcessor.Send("DecideGoFirst", text);
        }
        private void 导出场面信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog exportFieldStatus = new SaveFileDialog();
            exportFieldStatus.Filter = "FECipher场面信息(*.fe0fs)|*.fe0fs";
            exportFieldStatus.FilterIndex = 1;
            exportFieldStatus.RestoreDirectory = true;
            if (exportFieldStatus.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(exportFieldStatus.FileName, DatabaseVer.ToString() + "#" + Player.toString(), Encoding.UTF8);
            }
        }
        private void 导入场面信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog importFieldStatus = new OpenFileDialog();
            importFieldStatus.Filter = "FECipher场面信息(*.fe0fs)|*.fe0fs";
            importFieldStatus.FilterIndex = 1;
            importFieldStatus.RestoreDirectory = true;
            if (importFieldStatus.ShowDialog() == DialogResult.OK)
            {
                string[] FieldStatus = File.ReadAllText(importFieldStatus.FileName, Encoding.UTF8).Split(new string[] { "#" }, StringSplitOptions.None);
                int ImportDataBaseVer = Convert.ToInt32(FieldStatus[0]);
                if (ImportDataBaseVer > DatabaseVer)
                {
                    MessageBox.Show("要导入的数据版本高于当前程序的数据版本，请下载新版程序。");
                }
                else
                {
                    Player = new User(FieldStatus[1]);
                    Renew();
                    buttonGameOn.Visible = false;
                    buttonTurnStart.Visible = true;
                    buttonTurnEnd.Visible = true;
                    buttonUseKizuna.Visible = true;
                    buttonSupport.Visible = true;
                    button_CriticalAttack.Visible = true;
                    button_Miss.Visible = true;
                    label_RivalHandLabel.Visible = true;
                    label_RivalHandTotal.Visible = true;
                    label_RivalHandTotal.BringToFront();
                    buttonGameOn.Enabled = false;
                    游戏开始ToolStripMenuItem.Enabled = false;
                    动作ToolStripMenuItem.Enabled = true;
                    导出场面信息ToolStripMenuItem.Enabled = true;
                    GameOn = true;
                    string DeckTemp = "";
                    for (int NumberInDeck = 0; ; NumberInDeck++)
                    {
                        if (NumberInDeck != 0)
                        {
                            DeckTemp += Environment.NewLine;
                        }
                        if (Player.SearchCard(NumberInDeck) != null)
                        {
                            DeckTemp += Player.SearchCard(NumberInDeck).SerialNo.ToString();
                        }
                        else
                        {
                            break;
                        }
                    }
                    DeckFilename = "decktemp";
                    File.WriteAllText(DeckFilename, DeckTemp, Encoding.UTF8);
                    msgProcessor.Send("ImportFieldStatus", "#导入场面信息。");
                }
            }
        }
        private void 抛出异常调试用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new Exception();
        }
        private void 编辑卡组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DeckConstruction DeckConstruction = new DeckConstruction())
            {
                DeckConstruction.Owner = this;
                DeckConstruction.ShowDialog();
            }
        }
        private void 用户手册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://sdercolin.gitbooks.io/feciphervit-manual/content/");
        }
        private void bug报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://fecipher.lofter.com/post/1d409908_812d278");
        }
        private void 下载更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://fecipher.lofter.com/post/1d409908_812d27f");
        }
        #endregion

        public List<string> GetAllCardsInfo()
        {
            List<string> allcards = new List<string> { PlayerName, System.DateTime.Now.ToString("HH:mm:ss") };
            allcards.Add("[Deck]");
            allcards.Add(Player.Deck.CardList.Count.ToString());
            allcards.Add("[Hand]");
            allcards.Add(Player.Hand.CardList.Count.ToString());
            allcards.Add("[Grave]");
            allcards.Add(Player.Grave.CardList.Count.ToString());
            foreach (Card card in Player.Grave.CardList)
            {
                allcards.Add(card.SerialNo.ToString());
            }
            allcards.Add("[Support]");
            if (Player.Support.CardList.Count > 0)
            {
                allcards.Add(Player.Support.CardList[0].SerialNo.ToString());
            }
            else
            {
                allcards.Add("-1");
            }
            allcards.Add("[Kizuna]");
            allcards.Add(Player.Kizuna.CardList.Count.ToString());
            foreach (Card card in Player.Kizuna.CardList)
            {
                string temp = "";
                if (!card.FrontShown)
                {
                    temp += "!";
                }
                temp += card.SerialNo.ToString();
                allcards.Add(temp);
            }
            allcards.Add("[KizunaUsed]");
            allcards.Add(Player.KizunaUsed.CardList.Count.ToString());
            foreach (Card card in Player.KizunaUsed.CardList)
            {
                string temp = "";
                if (!card.FrontShown)
                {
                    temp += "!";
                }
                temp += card.SerialNo.ToString();
                allcards.Add(temp);
            }
            allcards.Add("[Orb]");
            allcards.Add(Player.Orb.CardList.Count.ToString());
            allcards.Add("[FrontField]");
            allcards.Add(Player.FrontField.CardList.Count.ToString());
            foreach (Card card in Player.FrontField.CardList)
            {
                string temp = "";
                if (!card.FrontShown)
                {
                    temp += "!";
                }
                if (card.IsHorizontal)
                {
                    temp += "@";
                }
                temp += card.SerialNo.ToString();
                allcards.Add(temp);
            }
            allcards.Add("[BackField]");
            allcards.Add(Player.BackField.CardList.Count.ToString());
            foreach (Card card in Player.BackField.CardList)
            {
                string temp = "";
                if (!card.FrontShown)
                {
                    temp += "!";
                }
                if (card.IsHorizontal)
                {
                    temp += "@";
                }
                temp += card.SerialNo.ToString();
                allcards.Add(temp);
            }
            allcards.Add("[Overlay]");
            allcards.Add(Player.Overlay.CardList.Count.ToString());
            foreach (Card card in Player.Overlay.CardList)
            {
                allcards.Add(card.SerialNo.ToString());
            }
            return allcards;
        }
        public string TranslateAllCardsInfo(List<string> allcards)
        {
            string text = "";
            //"[Deck]", DeckTotal, "[Hand]", HandTotal, "[Grave]", GraveTotal, GraveCard0, ...,
            //"[Support]", SupportCard0, "[Kizuna]", KizunaTotal, KizunaCard0, ..., "[KizunaUsed]", KizunaUsedTotal, KizunaUsedCard0, ...,
            //"[Orb]", OrbTotal, "[FrontField]", FrontFieldTotal, FrontField0, ..., "[BackField]", BackFieldTotal, BackField0, ...,
            //"[Overlay]", OverlayTotal, OverlayCard0, ...
            //For a card number, a "!" in front means "Reversed", a "@" in front means "Horizontal", and also "!@" works.
            //Also, "-1" means there is no card.
            text += "#发送场地信息" + Environment.NewLine;
            text += ">>卡组：" + allcards[3] + "　";
            text += "手牌：" + allcards[5] + "　";
            text += "退避区：" + allcards[7] + Environment.NewLine;
            int pos = 7;
            int serialno;
            while (allcards[pos] != "[Support]")
            {
                pos++;
            }
            text += ">>支援区：";
            serialno = Convert.ToInt32(allcards[pos + 1]);
            if (serialno != -1)
            {
                string[] CardDataSplit = CardData[serialno];
                text += "[" + CardDataSplit[2] + "] " + CardDataSplit[4];
            }
            else
            {
                text += "无";
            }
            text += Environment.NewLine;
            pos += 2;
            int KizunaTotal = Convert.ToInt32(allcards[pos + 1]);
            int KizunaReversed = 0;
            while (allcards[pos] != "[KizunaUsed]")
            {
                if (allcards[pos].Substring(0, 1) == "!")
                {
                    KizunaReversed++;
                }
                pos++;
            }
            int KizunaAndKizunaUsedTotal = KizunaTotal + Convert.ToInt32(allcards[pos + 1]);
            while (allcards[pos] != "[Orb]")
            {
                if (allcards[pos].Substring(0, 1) == "!")
                {
                    KizunaReversed++;
                }
                pos++;
            }
            text += ">>羁绊卡：" + KizunaAndKizunaUsedTotal.ToString() + "　未使用：" + KizunaTotal.ToString() + "　未翻面：" + (KizunaAndKizunaUsedTotal - KizunaReversed).ToString() + Environment.NewLine;
            text += ">>宝玉卡：" + allcards[pos + 1] + Environment.NewLine;
            pos += 2;
            text += ">>前卫区：";
            int FrontFieldTotal = Convert.ToInt32(allcards[pos + 1]);
            pos += 2;
            if (FrontFieldTotal == 0)
            {
                text += "无";
            }
            for (int i = 0; i < FrontFieldTotal; i++)
            {
                text += Environment.NewLine + "    ";
                string temp = "";
                if (allcards[pos + i].Substring(0, 1) == "@")
                {
                    temp += "（已行动）";
                    serialno = Convert.ToInt32(allcards[pos + i].Substring(1));
                }
                else if (allcards[pos + i].Length > 1 && allcards[pos + i].Substring(1, 1) == "@")
                {
                    temp += "（已行动）";
                    serialno = Convert.ToInt32(allcards[pos + i].Substring(2));
                }
                else if (allcards[pos + i].Substring(0, 1) == "!")
                {
                    serialno = Convert.ToInt32(allcards[pos + i].Substring(1));
                }
                else
                {
                    serialno = Convert.ToInt32(allcards[pos + i]);
                }
                string[] CardDataSplit = CardData[serialno];
                text += "[" + CardDataSplit[4] + "]" + temp;
            }
            pos += FrontFieldTotal;
            text += Environment.NewLine + ">>后卫区：";
            int BackFieldTotal = Convert.ToInt32(allcards[pos + 1]);
            pos += 2;
            if (BackFieldTotal == 0)
            {
                text += "无";
            }
            for (int i = 0; i < BackFieldTotal; i++)
            {
                text += Environment.NewLine + "    ";
                string temp = "";
                if (allcards[pos + i].Substring(0, 1) == "@")
                {
                    temp += "（已行动）";
                    serialno = Convert.ToInt32(allcards[pos + i].Substring(1));
                }
                else if (allcards[pos + i].Length > 1 && allcards[pos + i].Substring(1, 1) == "@")
                {
                    temp += "（已行动）";
                    serialno = Convert.ToInt32(allcards[pos + i].Substring(2));
                }
                else if (allcards[pos + i].Substring(0, 1) == "!")
                {
                    serialno = Convert.ToInt32(allcards[pos + i].Substring(1));
                }
                else
                {
                    serialno = Convert.ToInt32(allcards[pos + i]);
                }
                string[] CardDataSplit = CardData[serialno];
                text += "[" + CardDataSplit[4] + "]" + temp;
            }
            return text;
        }
        public string GetRegionNameInString(Region region)
        {
            if (region.Equals(Player.FrontField))
            {
                return "前卫区";
            }
            else if (region.Equals(Player.BackField))
            {
                return "后卫区";
            }
            else if (region.Equals(Player.Deck))
            {
                return "卡组";
            }
            else if (region.Equals(Player.Grave))
            {
                return "退避区";
            }
            else if (region.Equals(Player.Hand))
            {
                return "手卡";
            }
            else if (region.Equals(Player.Kizuna) || region.Equals(Player.KizunaUsed))
            {
                return "羁绊区";
            }
            else if (region.Equals(Player.Orb))
            {
                return "宝玉区";
            }
            else if (region.Equals(Player.Support))
            {
                return "支援区";
            }
            else if (region.Equals(Rival.FrontField))
            {
                return "对手前卫区";
            }
            else if (region.Equals(Rival.BackField))
            {
                return "对手后卫区";
            }
            else
            {
                return "Error";
            }
        }
        public string GetTextOfMovingToRegion(Card card, string toWhere, bool IsCheckingRegion)
        {
            string text;
            if ((new List<string> { "Orb", "DeckShuffle", "DeckTop", "DeckBottom", "Hand" }).Contains(toWhere))
            {
                if (card.BelongedRegion().Equals(Player.Deck))
                {
                    if (!IsCheckingRegion)
                    {
                        text = "#将卡组顶牌";
                    }
                    else
                    {
                        text = "#将[" + GetRegionNameInString(card.BelongedRegion()) + "][" + card.UnitTitle + " " + card.UnitName + "]";
                    }
                }
                else if (card.BelongedRegion().Equals(Player.Orb))
                {
                    text = "#将[宝玉区(" + (Player.Orb.CardList.IndexOf(card) + 1).ToString() + ")]";
                }
                else if (card.BelongedRegion().Equals(Player.Hand))
                {
                    text = "#将[手卡(" + (Player.Hand.CardList.IndexOf(card) + 1).ToString() + ")]";
                }
                else
                {
                    text = "#将[" + GetRegionNameInString(card.BelongedRegion()) + "][" + card.UnitTitle + " " + card.UnitName + "]";
                }
            }
            else
            {
                text = "#将[" + GetRegionNameInString(card.BelongedRegion()) + "][" + card.UnitTitle + " " + card.UnitName + "]";
            }
            switch (toWhere)
            {
                case "Orb":
                    text += "置于宝玉区";
                    break;
                case "DeckShuffle":
                    text += "加入卡组并切洗";
                    break;
                case "DeckTop":
                    text += "置于卡组顶端";
                    break;
                case "DeckBottom":
                    text += "置于卡组底端";
                    break;
                case "Hand":
                    text += "加入手牌";
                    break;
                case "Support":
                    text += "置于支援区";
                    break;
                case "Kizuna":
                    text += "置于羁绊区";
                    break;
                case "Grave":
                    text += "置于退避区";
                    break;
            }
            if (card.OverlayCardNo.Count > 0)
            {
                text += "（叠放卡送入退避区）。";
            }
            else
            {
                text += "。";
            }
            return text;
        }
    }
}