using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AT2Recycle
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var valueSelected = SettingsManager.SETTING_HIDE;

            if (radioButtonOptionDisable.Checked ) 
                valueSelected = SettingsManager.SETTING_DISABLE;

            if (radioButtonOptionNone.Checked)
                valueSelected = SettingsManager.SETTING_DISPLAY_ALWAYS;

            SettingsManager.Instance.WriteSetting(valueSelected);

            this.Close();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            radioButtonOptionDisable.Checked = SettingsManager.Instance.ReadSetting() == SettingsManager.SETTING_DISABLE;
            radioButtonOptionHide.Checked = SettingsManager.Instance.ReadSetting() == SettingsManager.SETTING_HIDE;
            radioButtonOptionNone.Checked = SettingsManager.Instance.ReadSetting() == SettingsManager.SETTING_DISPLAY_ALWAYS;
        }
    }
}
