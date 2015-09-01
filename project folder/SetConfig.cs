using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace FECipherVit
{
    public partial class SetConfig : Form
    {
        public SetConfig()
        {
            InitializeComponent();
        }

        private void button_Confirm_Click(object sender, EventArgs e)
        {
            AppConfig.SetValue("RememberHero", checkBox_RememberHero.Checked.ToString());
            this.Close();
        }

        private void SetConfig_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        void LoadSettings()
        {
            if (AppConfig.GetValue("RememberHero") == "True")
            {
                checkBox_RememberHero.Checked = true;
            }
        }
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            AppConfig.SetValue("RememberHero", "False");
            LoadSettings();
        }
    }
}
