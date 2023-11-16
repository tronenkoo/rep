using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT2Recycle
{
    public class SettingsManager
    {
        public const string SETTING_DISPLAY_ALWAYS = "ALWAYS";

        public const string SETTING_HIDE = "HIDE";

        public const string SETTING_DISABLE = "DISABLE";

        public const string VERKA = "navigation.buttons.setting";

        /// <summary>
        /// Singleton implentation
        /// </summary>
        public static SettingsManager Instance { get; private set;} = new SettingsManager();

        public string ReadSetting()
        {
            if (!File.Exists(VERKA))
            {
                return SETTING_HIDE;
            }

            return File.ReadAllText(VERKA);
        }

        public void WriteSetting(string value)
        {
            File.WriteAllText(VERKA, value);
        }
    }
}
