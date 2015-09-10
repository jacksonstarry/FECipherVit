namespace FECipherVit
{
    partial class CardSetView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardSetView));
            this.CardListBox = new System.Windows.Forms.ListBox();
            this.button_ToFrontField = new System.Windows.Forms.Button();
            this.button_ToBackField = new System.Windows.Forms.Button();
            this.button_ToHand = new System.Windows.Forms.Button();
            this.button_ToDeckTop = new System.Windows.Forms.Button();
            this.button_ToDeckBottom = new System.Windows.Forms.Button();
            this.button_Show = new System.Windows.Forms.Button();
            this.button_Confirm = new System.Windows.Forms.Button();
            this.button_ToGrave = new System.Windows.Forms.Button();
            this.button_ToDeckShuffle = new System.Windows.Forms.Button();
            this.pictureBoxCardInfo = new System.Windows.Forms.PictureBox();
            this.textBoxCardInfo = new System.Windows.Forms.TextBox();
            this.button_Reverse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCardInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // CardListBox
            // 
            this.CardListBox.FormattingEnabled = true;
            this.CardListBox.ItemHeight = 17;
            this.CardListBox.Location = new System.Drawing.Point(12, 13);
            this.CardListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CardListBox.Name = "CardListBox";
            this.CardListBox.Size = new System.Drawing.Size(228, 531);
            this.CardListBox.TabIndex = 0;
            this.CardListBox.SelectedIndexChanged += new System.EventHandler(this.CardListBox_SelectedIndexChanged);
            // 
            // button_ToFrontField
            // 
            this.button_ToFrontField.Location = new System.Drawing.Point(480, 14);
            this.button_ToFrontField.Name = "button_ToFrontField";
            this.button_ToFrontField.Size = new System.Drawing.Size(128, 47);
            this.button_ToFrontField.TabIndex = 1;
            this.button_ToFrontField.Text = "置于前卫区";
            this.button_ToFrontField.UseVisualStyleBackColor = true;
            this.button_ToFrontField.Click += new System.EventHandler(this.button_ToFrontField_Click);
            // 
            // button_ToBackField
            // 
            this.button_ToBackField.Location = new System.Drawing.Point(480, 67);
            this.button_ToBackField.Name = "button_ToBackField";
            this.button_ToBackField.Size = new System.Drawing.Size(128, 47);
            this.button_ToBackField.TabIndex = 2;
            this.button_ToBackField.Text = "置于后卫区";
            this.button_ToBackField.UseVisualStyleBackColor = true;
            this.button_ToBackField.Click += new System.EventHandler(this.button_ToBackField_Click);
            // 
            // button_ToHand
            // 
            this.button_ToHand.Location = new System.Drawing.Point(480, 173);
            this.button_ToHand.Name = "button_ToHand";
            this.button_ToHand.Size = new System.Drawing.Size(128, 47);
            this.button_ToHand.TabIndex = 3;
            this.button_ToHand.Text = "加入手牌";
            this.button_ToHand.UseVisualStyleBackColor = true;
            this.button_ToHand.Click += new System.EventHandler(this.button_ToHand_Click);
            // 
            // button_ToDeckTop
            // 
            this.button_ToDeckTop.Location = new System.Drawing.Point(480, 332);
            this.button_ToDeckTop.Name = "button_ToDeckTop";
            this.button_ToDeckTop.Size = new System.Drawing.Size(128, 47);
            this.button_ToDeckTop.TabIndex = 4;
            this.button_ToDeckTop.Text = "置于卡组顶端";
            this.button_ToDeckTop.UseVisualStyleBackColor = true;
            this.button_ToDeckTop.Click += new System.EventHandler(this.button_ToDeckTop_Click);
            // 
            // button_ToDeckBottom
            // 
            this.button_ToDeckBottom.Location = new System.Drawing.Point(480, 385);
            this.button_ToDeckBottom.Name = "button_ToDeckBottom";
            this.button_ToDeckBottom.Size = new System.Drawing.Size(128, 47);
            this.button_ToDeckBottom.TabIndex = 5;
            this.button_ToDeckBottom.Text = "置于卡组底端";
            this.button_ToDeckBottom.UseVisualStyleBackColor = true;
            this.button_ToDeckBottom.Click += new System.EventHandler(this.button_ToDeckBottom_Click);
            // 
            // button_Show
            // 
            this.button_Show.Location = new System.Drawing.Point(480, 120);
            this.button_Show.Name = "button_Show";
            this.button_Show.Size = new System.Drawing.Size(128, 47);
            this.button_Show.TabIndex = 7;
            this.button_Show.Text = "展示";
            this.button_Show.UseVisualStyleBackColor = true;
            this.button_Show.Click += new System.EventHandler(this.button_Show_Click);
            // 
            // button_Confirm
            // 
            this.button_Confirm.Location = new System.Drawing.Point(480, 497);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(128, 47);
            this.button_Confirm.TabIndex = 8;
            this.button_Confirm.Text = "确定";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.button_Confirm_Click);
            // 
            // button_ToGrave
            // 
            this.button_ToGrave.Location = new System.Drawing.Point(480, 226);
            this.button_ToGrave.Name = "button_ToGrave";
            this.button_ToGrave.Size = new System.Drawing.Size(128, 47);
            this.button_ToGrave.TabIndex = 9;
            this.button_ToGrave.Text = "置于退避区";
            this.button_ToGrave.UseVisualStyleBackColor = true;
            this.button_ToGrave.Click += new System.EventHandler(this.button_ToGrave_Click);
            // 
            // button_ToDeckShuffle
            // 
            this.button_ToDeckShuffle.Location = new System.Drawing.Point(480, 279);
            this.button_ToDeckShuffle.Name = "button_ToDeckShuffle";
            this.button_ToDeckShuffle.Size = new System.Drawing.Size(128, 47);
            this.button_ToDeckShuffle.TabIndex = 10;
            this.button_ToDeckShuffle.Text = "加入卡组并切洗";
            this.button_ToDeckShuffle.UseVisualStyleBackColor = true;
            this.button_ToDeckShuffle.Click += new System.EventHandler(this.button_ToDeckShuffle_Click);
            // 
            // pictureBoxCardInfo
            // 
            this.pictureBoxCardInfo.Location = new System.Drawing.Point(257, 13);
            this.pictureBoxCardInfo.Name = "pictureBoxCardInfo";
            this.pictureBoxCardInfo.Size = new System.Drawing.Size(208, 291);
            this.pictureBoxCardInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCardInfo.TabIndex = 12;
            this.pictureBoxCardInfo.TabStop = false;
            // 
            // textBoxCardInfo
            // 
            this.textBoxCardInfo.Location = new System.Drawing.Point(257, 321);
            this.textBoxCardInfo.Multiline = true;
            this.textBoxCardInfo.Name = "textBoxCardInfo";
            this.textBoxCardInfo.ReadOnly = true;
            this.textBoxCardInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCardInfo.Size = new System.Drawing.Size(208, 223);
            this.textBoxCardInfo.TabIndex = 11;
            // 
            // button_Reverse
            // 
            this.button_Reverse.Location = new System.Drawing.Point(480, 120);
            this.button_Reverse.Name = "button_Reverse";
            this.button_Reverse.Size = new System.Drawing.Size(128, 47);
            this.button_Reverse.TabIndex = 13;
            this.button_Reverse.Text = "翻面";
            this.button_Reverse.UseVisualStyleBackColor = true;
            this.button_Reverse.Visible = false;
            this.button_Reverse.Click += new System.EventHandler(this.button_Reverse_Click);
            // 
            // CardSetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 558);
            this.Controls.Add(this.button_Reverse);
            this.Controls.Add(this.pictureBoxCardInfo);
            this.Controls.Add(this.textBoxCardInfo);
            this.Controls.Add(this.button_ToDeckShuffle);
            this.Controls.Add(this.button_ToGrave);
            this.Controls.Add(this.button_Confirm);
            this.Controls.Add(this.button_Show);
            this.Controls.Add(this.button_ToDeckBottom);
            this.Controls.Add(this.button_ToDeckTop);
            this.Controls.Add(this.button_ToHand);
            this.Controls.Add(this.button_ToBackField);
            this.Controls.Add(this.button_ToFrontField);
            this.Controls.Add(this.CardListBox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CardSetView";
            this.Text = "CardSetView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CardSetView_FormClosing);
            this.Load += new System.EventHandler(this.CardSetView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCardInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox CardListBox;
        private System.Windows.Forms.Button button_ToFrontField;
        private System.Windows.Forms.Button button_ToBackField;
        private System.Windows.Forms.Button button_ToHand;
        private System.Windows.Forms.Button button_ToDeckTop;
        private System.Windows.Forms.Button button_ToDeckBottom;
        private System.Windows.Forms.Button button_Show;
        private System.Windows.Forms.Button button_Confirm;
        private System.Windows.Forms.Button button_ToGrave;
        private System.Windows.Forms.Button button_ToDeckShuffle;
        private System.Windows.Forms.PictureBox pictureBoxCardInfo;
        private System.Windows.Forms.TextBox textBoxCardInfo;
        private System.Windows.Forms.Button button_Reverse;
    }
}