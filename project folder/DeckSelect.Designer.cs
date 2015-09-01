namespace FECipherVit
{
    partial class DeckSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeckSelect));
            this.listBoxDeckList = new System.Windows.Forms.ListBox();
            this.buttonDeckImport = new System.Windows.Forms.Button();
            this.buttonDeckDelete = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.textBox_CardList = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listBoxDeckList
            // 
            this.listBoxDeckList.FormattingEnabled = true;
            this.listBoxDeckList.ItemHeight = 17;
            this.listBoxDeckList.Location = new System.Drawing.Point(12, 12);
            this.listBoxDeckList.Name = "listBoxDeckList";
            this.listBoxDeckList.Size = new System.Drawing.Size(171, 276);
            this.listBoxDeckList.TabIndex = 0;
            this.listBoxDeckList.SelectedIndexChanged += new System.EventHandler(this.listBoxDeckList_SelectedIndexChanged);
            // 
            // buttonDeckImport
            // 
            this.buttonDeckImport.Location = new System.Drawing.Point(12, 311);
            this.buttonDeckImport.Name = "buttonDeckImport";
            this.buttonDeckImport.Size = new System.Drawing.Size(171, 44);
            this.buttonDeckImport.TabIndex = 2;
            this.buttonDeckImport.Text = "导入卡组";
            this.buttonDeckImport.UseVisualStyleBackColor = true;
            this.buttonDeckImport.Click += new System.EventHandler(this.buttonDeckImport_Click);
            // 
            // buttonDeckDelete
            // 
            this.buttonDeckDelete.Location = new System.Drawing.Point(12, 363);
            this.buttonDeckDelete.Name = "buttonDeckDelete";
            this.buttonDeckDelete.Size = new System.Drawing.Size(171, 44);
            this.buttonDeckDelete.TabIndex = 3;
            this.buttonDeckDelete.Text = "删除卡组";
            this.buttonDeckDelete.UseVisualStyleBackColor = true;
            this.buttonDeckDelete.Click += new System.EventHandler(this.buttonDeckDelete_Click);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(12, 413);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(171, 44);
            this.buttonConfirm.TabIndex = 4;
            this.buttonConfirm.Text = "确定";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // textBox_CardList
            // 
            this.textBox_CardList.Location = new System.Drawing.Point(197, 12);
            this.textBox_CardList.Multiline = true;
            this.textBox_CardList.Name = "textBox_CardList";
            this.textBox_CardList.ReadOnly = true;
            this.textBox_CardList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_CardList.Size = new System.Drawing.Size(366, 445);
            this.textBox_CardList.TabIndex = 5;
            this.textBox_CardList.Text = "请在左边的列表中选择卡组。";
            // 
            // DeckSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 469);
            this.Controls.Add(this.textBox_CardList);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.buttonDeckDelete);
            this.Controls.Add(this.buttonDeckImport);
            this.Controls.Add(this.listBoxDeckList);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeckSelect";
            this.Text = "选择卡组";
            this.Load += new System.EventHandler(this.DeckSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxDeckList;
        private System.Windows.Forms.Button buttonDeckImport;
        private System.Windows.Forms.Button buttonDeckDelete;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.TextBox textBox_CardList;
    }
}