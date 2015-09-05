namespace FECipherVit
{
    partial class UseSkill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UseSkill));
            this.label = new System.Windows.Forms.Label();
            this.Button_Confirm = new System.Windows.Forms.Button();
            this.textBoxSkillContent = new System.Windows.Forms.TextBox();
            this.comboBoxSkillList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(20, 17);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(116, 17);
            this.label.TabIndex = 0;
            this.label.Text = "选择要发动的能力：";
            // 
            // Button_Confirm
            // 
            this.Button_Confirm.Location = new System.Drawing.Point(270, 46);
            this.Button_Confirm.Name = "Button_Confirm";
            this.Button_Confirm.Size = new System.Drawing.Size(75, 25);
            this.Button_Confirm.TabIndex = 2;
            this.Button_Confirm.Text = "确定";
            this.Button_Confirm.UseVisualStyleBackColor = true;
            this.Button_Confirm.Click += new System.EventHandler(this.Button_Confirm_Click);
            // 
            // textBoxSkillContent
            // 
            this.textBoxSkillContent.Location = new System.Drawing.Point(23, 86);
            this.textBoxSkillContent.Multiline = true;
            this.textBoxSkillContent.Name = "textBoxSkillContent";
            this.textBoxSkillContent.ReadOnly = true;
            this.textBoxSkillContent.Size = new System.Drawing.Size(322, 265);
            this.textBoxSkillContent.TabIndex = 3;
            // 
            // comboBoxSkillList
            // 
            this.comboBoxSkillList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSkillList.FormattingEnabled = true;
            this.comboBoxSkillList.Location = new System.Drawing.Point(23, 46);
            this.comboBoxSkillList.Name = "comboBoxSkillList";
            this.comboBoxSkillList.Size = new System.Drawing.Size(226, 25);
            this.comboBoxSkillList.TabIndex = 4;
            this.comboBoxSkillList.SelectedIndexChanged += new System.EventHandler(this.comboBoxSkillList_SelectedIndexChanged);
            // 
            // UseSkill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 372);
            this.Controls.Add(this.comboBoxSkillList);
            this.Controls.Add(this.textBoxSkillContent);
            this.Controls.Add(this.Button_Confirm);
            this.Controls.Add(this.label);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UseSkill";
            this.Text = "发动能力";
            this.Load += new System.EventHandler(this.UseSkill_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button Button_Confirm;
        private System.Windows.Forms.TextBox textBoxSkillContent;
        private System.Windows.Forms.ComboBox comboBoxSkillList;
    }
}