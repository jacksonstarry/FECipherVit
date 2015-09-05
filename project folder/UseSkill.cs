using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FECipherVit
{
    public partial class UseSkill : Form
    {
        public UseSkill(string[] _CardInfo)
        {
            InitializeComponent();
            CardInfo = _CardInfo;
        }

        string[] CardInfo;
        List<string> SkillNames = new List<string>();
        List<string> SkillContents = new List<string>();
        public string SelectedSkillContent;

        private void UseSkill_Load(object sender, EventArgs e)
        {
            SkillContents.AddRange(CardInfo[16].Split(new string[] { "$$" }, StringSplitOptions.None));
            for (int i = 0; i < SkillContents.Count; i++)
            {
                if (SkillContents[i][0] != '『' && SkillNames.Count > 0) //for 安娜
                {
                    SkillContents[i] = "『100名安娜』" + SkillContents[i];
                }
                SkillNames.Add(SkillContents[i].Substring(0, SkillContents[i].IndexOf("】") + 1));
            }
            comboBoxSkillList.Items.AddRange(SkillNames.ToArray());
            if(comboBoxSkillList.Items.Count>0)
            {
                comboBoxSkillList.SelectedIndex = 0;
            } 
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            if(comboBoxSkillList.SelectedIndex>=0)
            {
                SelectedSkillContent = SkillContents[comboBoxSkillList.SelectedIndex];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void comboBoxSkillList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxSkillList.SelectedIndex>=0)
            {
                textBoxSkillContent.Text = SkillContents[comboBoxSkillList.SelectedIndex];
            }
        }
    }
}
