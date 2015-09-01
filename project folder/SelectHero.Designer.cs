namespace FECipherVit
{
    partial class SelectHero
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectHero));
            this.CardListBox = new System.Windows.Forms.ListBox();
            this.button_Confirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CardListBox
            // 
            this.CardListBox.FormattingEnabled = true;
            this.CardListBox.ItemHeight = 17;
            this.CardListBox.Location = new System.Drawing.Point(12, 13);
            this.CardListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CardListBox.Name = "CardListBox";
            this.CardListBox.Size = new System.Drawing.Size(228, 259);
            this.CardListBox.TabIndex = 0;
            this.CardListBox.SelectedIndexChanged += new System.EventHandler(this.CardListBox_SelectedIndexChanged);
            // 
            // button_Confirm
            // 
            this.button_Confirm.Location = new System.Drawing.Point(12, 279);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(228, 47);
            this.button_Confirm.TabIndex = 8;
            this.button_Confirm.Text = "确定";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.button_Confirm_Click);
            // 
            // SelectHero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 334);
            this.Controls.Add(this.button_Confirm);
            this.Controls.Add(this.CardListBox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectHero";
            this.Text = "选择主人公";
            this.Load += new System.EventHandler(this.CardSetView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox CardListBox;
        private System.Windows.Forms.Button button_Confirm;
    }
}