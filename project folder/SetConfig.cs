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
            AppConfig.SetValue("UseFirsrCardAsHero", checkBox_UseFirsrCardAsHero.Checked.ToString());
            AppConfig.SetValue("SendSkillDetail", checkBox_SendSkillDetail.Checked.ToString());
            this.Close();
        }

        private void SetConfig_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        void LoadSettings()
        {
            if (AppConfig.GetValue("UseFirsrCardAsHero") == "True")
            {
                checkBox_UseFirsrCardAsHero.Checked = true;
            }
            if (AppConfig.GetValue("SendSkillDetail") == "True")
            {
                checkBox_SendSkillDetail.Checked = true;
            }
        }
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            AppConfig.SetValue("UseFirsrCardAsHero", "False");
            AppConfig.SetValue("SendSkillDetail", "True");
            LoadSettings();
        }
    }
}
