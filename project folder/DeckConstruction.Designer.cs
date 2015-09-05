namespace FECipherVit
{
    partial class DeckConstruction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeckConstruction));
            this.pictureBoxCardInfo = new System.Windows.Forms.PictureBox();
            this.textBoxCardInfo = new System.Windows.Forms.TextBox();
            this.listViewSearchCard = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCardName = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.listBoxDeckCard = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCardTotal = new System.Windows.Forms.Label();
            this.buttonSort = new System.Windows.Forms.Button();
            this.comboBox_DeckList = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_NewDeck = new System.Windows.Forms.TextBox();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonSetHero = new System.Windows.Forms.Button();
            this.buttonDeleteCardWithSameName = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCardInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCardInfo
            // 
            this.pictureBoxCardInfo.Location = new System.Drawing.Point(14, 17);
            this.pictureBoxCardInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBoxCardInfo.Name = "pictureBoxCardInfo";
            this.pictureBoxCardInfo.Size = new System.Drawing.Size(232, 324);
            this.pictureBoxCardInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCardInfo.TabIndex = 14;
            this.pictureBoxCardInfo.TabStop = false;
            // 
            // textBoxCardInfo
            // 
            this.textBoxCardInfo.Location = new System.Drawing.Point(265, 17);
            this.textBoxCardInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxCardInfo.Multiline = true;
            this.textBoxCardInfo.Name = "textBoxCardInfo";
            this.textBoxCardInfo.ReadOnly = true;
            this.textBoxCardInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCardInfo.Size = new System.Drawing.Size(412, 324);
            this.textBoxCardInfo.TabIndex = 13;
            // 
            // listViewSearchCard
            // 
            this.listViewSearchCard.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewSearchCard.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewSearchCard.FullRowSelect = true;
            this.listViewSearchCard.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewSearchCard.Location = new System.Drawing.Point(14, 391);
            this.listViewSearchCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listViewSearchCard.Name = "listViewSearchCard";
            this.listViewSearchCard.Size = new System.Drawing.Size(663, 257);
            this.listViewSearchCard.TabIndex = 16;
            this.listViewSearchCard.UseCompatibleStateImageBehavior = false;
            this.listViewSearchCard.View = System.Windows.Forms.View.Details;
            this.listViewSearchCard.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listViewSearchCard_ColumnWidthChanging);
            this.listViewSearchCard.SelectedIndexChanged += new System.EventHandler(this.listViewSearchCard_SelectedIndexChanged);
            this.listViewSearchCard.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSearchCard_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序列号";
            this.columnHeader1.Width = 54;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "编号";
            this.columnHeader2.Width = 78;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "日文卡名";
            this.columnHeader3.Width = 202;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "中文卡名";
            this.columnHeader4.Width = 210;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 362);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "检索卡名";
            // 
            // textBoxCardName
            // 
            this.textBoxCardName.Location = new System.Drawing.Point(79, 359);
            this.textBoxCardName.Name = "textBoxCardName";
            this.textBoxCardName.Size = new System.Drawing.Size(383, 23);
            this.textBoxCardName.TabIndex = 18;
            this.textBoxCardName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCardName_KeyDown);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(477, 358);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(94, 25);
            this.buttonSearch.TabIndex = 19;
            this.buttonSearch.Text = "检索";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(585, 358);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(92, 25);
            this.buttonReset.TabIndex = 20;
            this.buttonReset.Text = "重置";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // listBoxDeckCard
            // 
            this.listBoxDeckCard.FormattingEnabled = true;
            this.listBoxDeckCard.ItemHeight = 17;
            this.listBoxDeckCard.Location = new System.Drawing.Point(699, 168);
            this.listBoxDeckCard.Name = "listBoxDeckCard";
            this.listBoxDeckCard.Size = new System.Drawing.Size(271, 480);
            this.listBoxDeckCard.TabIndex = 21;
            this.listBoxDeckCard.SelectedIndexChanged += new System.EventHandler(this.listBoxDeckCard_SelectedIndexChanged);
            this.listBoxDeckCard.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxDeckCard_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(696, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "卡片总数：";
            // 
            // labelCardTotal
            // 
            this.labelCardTotal.AutoSize = true;
            this.labelCardTotal.Location = new System.Drawing.Point(764, 135);
            this.labelCardTotal.Name = "labelCardTotal";
            this.labelCardTotal.Size = new System.Drawing.Size(0, 17);
            this.labelCardTotal.TabIndex = 23;
            // 
            // buttonSort
            // 
            this.buttonSort.Location = new System.Drawing.Point(897, 102);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(75, 23);
            this.buttonSort.TabIndex = 24;
            this.buttonSort.Text = "排序";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
            // 
            // comboBox_DeckList
            // 
            this.comboBox_DeckList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DeckList.FormattingEnabled = true;
            this.comboBox_DeckList.Location = new System.Drawing.Point(768, 17);
            this.comboBox_DeckList.Name = "comboBox_DeckList";
            this.comboBox_DeckList.Size = new System.Drawing.Size(121, 25);
            this.comboBox_DeckList.TabIndex = 25;
            this.comboBox_DeckList.SelectedIndexChanged += new System.EventHandler(this.comboBox_DeckList_SelectedIndexChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(897, 17);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 25);
            this.buttonSave.TabIndex = 26;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(698, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 27;
            this.label3.Text = "选择卡组：";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(897, 131);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 28;
            this.buttonClear.Text = "清空";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(698, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 30;
            this.label4.Text = "新建卡组：";
            // 
            // textBox_NewDeck
            // 
            this.textBox_NewDeck.Location = new System.Drawing.Point(768, 52);
            this.textBox_NewDeck.Name = "textBox_NewDeck";
            this.textBox_NewDeck.Size = new System.Drawing.Size(121, 23);
            this.textBox_NewDeck.TabIndex = 31;
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(897, 51);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(75, 25);
            this.buttonNew.TabIndex = 32;
            this.buttonNew.Text = "新建";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonSetHero
            // 
            this.buttonSetHero.Location = new System.Drawing.Point(799, 102);
            this.buttonSetHero.Name = "buttonSetHero";
            this.buttonSetHero.Size = new System.Drawing.Size(90, 23);
            this.buttonSetHero.TabIndex = 33;
            this.buttonSetHero.Text = "置顶";
            this.buttonSetHero.UseVisualStyleBackColor = true;
            this.buttonSetHero.Click += new System.EventHandler(this.buttonSetHero_Click);
            // 
            // buttonDeleteCardWithSameName
            // 
            this.buttonDeleteCardWithSameName.Location = new System.Drawing.Point(799, 131);
            this.buttonDeleteCardWithSameName.Name = "buttonDeleteCardWithSameName";
            this.buttonDeleteCardWithSameName.Size = new System.Drawing.Size(90, 23);
            this.buttonDeleteCardWithSameName.TabIndex = 34;
            this.buttonDeleteCardWithSameName.Text = "删除同名卡";
            this.buttonDeleteCardWithSameName.UseVisualStyleBackColor = true;
            this.buttonDeleteCardWithSameName.Click += new System.EventHandler(this.buttonDeleteCardWithSameName_Click);
            // 
            // DeckConstruction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.buttonDeleteCardWithSameName);
            this.Controls.Add(this.buttonSetHero);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.textBox_NewDeck);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBox_DeckList);
            this.Controls.Add(this.buttonSort);
            this.Controls.Add(this.labelCardTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxDeckCard);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxCardName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewSearchCard);
            this.Controls.Add(this.pictureBoxCardInfo);
            this.Controls.Add(this.textBoxCardInfo);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DeckConstruction";
            this.Text = "卡组编辑";
            this.Load += new System.EventHandler(this.DeckConstruction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCardInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCardInfo;
        private System.Windows.Forms.TextBox textBoxCardInfo;
        private System.Windows.Forms.ListView listViewSearchCard;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCardName;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ListBox listBoxDeckCard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelCardTotal;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.ComboBox comboBox_DeckList;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_NewDeck;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonSetHero;
        private System.Windows.Forms.Button buttonDeleteCardWithSameName;
    }
}