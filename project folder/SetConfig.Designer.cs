﻿namespace FECipherVit
{
    partial class SetConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetConfig));
            this.button_Confirm = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Reset = new System.Windows.Forms.Button();
            this.checkBox_UseFirstCardAsHero = new System.Windows.Forms.CheckBox();
            this.checkBox_SendSkillDetail = new System.Windows.Forms.CheckBox();
            this.checkBox_CardInfoBrief = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button_Confirm
            // 
            this.button_Confirm.Location = new System.Drawing.Point(15, 211);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(78, 32);
            this.button_Confirm.TabIndex = 2;
            this.button_Confirm.Text = "确认";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.button_Confirm_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(109, 211);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(83, 32);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Reset
            // 
            this.button_Reset.Location = new System.Drawing.Point(207, 211);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(83, 32);
            this.button_Reset.TabIndex = 4;
            this.button_Reset.Text = "重置";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // checkBox_UseFirstCardAsHero
            // 
            this.checkBox_UseFirstCardAsHero.AutoSize = true;
            this.checkBox_UseFirstCardAsHero.Location = new System.Drawing.Point(15, 12);
            this.checkBox_UseFirstCardAsHero.Name = "checkBox_UseFirstCardAsHero";
            this.checkBox_UseFirstCardAsHero.Size = new System.Drawing.Size(183, 21);
            this.checkBox_UseFirstCardAsHero.TabIndex = 5;
            this.checkBox_UseFirstCardAsHero.Text = "使用卡组第一张卡作为主人公";
            this.checkBox_UseFirstCardAsHero.UseVisualStyleBackColor = true;
            // 
            // checkBox_SendSkillDetail
            // 
            this.checkBox_SendSkillDetail.AutoSize = true;
            this.checkBox_SendSkillDetail.Location = new System.Drawing.Point(15, 39);
            this.checkBox_SendSkillDetail.Name = "checkBox_SendSkillDetail";
            this.checkBox_SendSkillDetail.Size = new System.Drawing.Size(159, 21);
            this.checkBox_SendSkillDetail.TabIndex = 6;
            this.checkBox_SendSkillDetail.Text = "发动能力时发送能力内容";
            this.checkBox_SendSkillDetail.UseVisualStyleBackColor = true;
            // 
            // checkBox_CardInfoBrief
            // 
            this.checkBox_CardInfoBrief.AutoSize = true;
            this.checkBox_CardInfoBrief.Location = new System.Drawing.Point(15, 66);
            this.checkBox_CardInfoBrief.Name = "checkBox_CardInfoBrief";
            this.checkBox_CardInfoBrief.Size = new System.Drawing.Size(123, 21);
            this.checkBox_CardInfoBrief.TabIndex = 7;
            this.checkBox_CardInfoBrief.Text = "简略显示卡牌信息";
            this.checkBox_CardInfoBrief.UseVisualStyleBackColor = true;
            // 
            // SetConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 255);
            this.Controls.Add(this.checkBox_CardInfoBrief);
            this.Controls.Add(this.checkBox_SendSkillDetail);
            this.Controls.Add(this.checkBox_UseFirstCardAsHero);
            this.Controls.Add(this.button_Reset);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Confirm);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SetConfig";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.SetConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_Confirm;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.CheckBox checkBox_UseFirstCardAsHero;
        private System.Windows.Forms.CheckBox checkBox_SendSkillDetail;
        private System.Windows.Forms.CheckBox checkBox_CardInfoBrief;
    }
}