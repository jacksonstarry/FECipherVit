namespace FECipherVit
{
    partial class GetInteger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetInteger));
            this.label = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.Button_Confirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(20, 17);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(43, 17);
            this.label.TabIndex = 0;
            this.label.Text = "label1";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(23, 46);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(232, 23);
            this.textBox.TabIndex = 1;
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // Button_Confirm
            // 
            this.Button_Confirm.Location = new System.Drawing.Point(270, 46);
            this.Button_Confirm.Name = "Button_Confirm";
            this.Button_Confirm.Size = new System.Drawing.Size(75, 23);
            this.Button_Confirm.TabIndex = 2;
            this.Button_Confirm.Text = "确定";
            this.Button_Confirm.UseVisualStyleBackColor = true;
            this.Button_Confirm.Click += new System.EventHandler(this.Button_Confirm_Click);
            // 
            // GetInteger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 89);
            this.Controls.Add(this.Button_Confirm);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.label);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetInteger";
            this.Text = "GetInteger";
            this.Load += new System.EventHandler(this.GetInteger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button Button_Confirm;
    }
}