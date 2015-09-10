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
            AppConfig.SetValue("UseFirstCardAsHero", checkBox_UseFirstCardAsHero.Checked.ToString());
            AppConfig.SetValue("SendSkillDetail", checkBox_SendSkillDetail.Checked.ToString());
            AppConfig.SetValue("CardInfoBrief", checkBox_CardInfoBrief.Checked.ToString());
            this.Close();
        }

        private void SetConfig_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        void LoadSettings()
        {
                checkBox_UseFirstCardAsHero.Checked = Convert.ToBoolean(AppConfig.GetValue("UseFirstCardAsHero"));
                checkBox_SendSkillDetail.Checked = Convert.ToBoolean(AppConfig.GetValue("SendSkillDetail"));
                checkBox_CardInfoBrief.Checked = Convert.ToBoolean(AppConfig.GetValue("CardInfoBrief"));
        }
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            AppConfig.SetValue("UseFirstCardAsHero", "False");
            AppConfig.SetValue("SendSkillDetail", "True");
            AppConfig.SetValue("CardInfoBrief", "False");
            LoadSettings();
        }
    }
}
