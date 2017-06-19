using System.ComponentModel;
using System.Configuration;

namespace MichaelBrandonMorris.MvcGrid.Utility
{
    internal class ConfigUtility
    {
        internal const string ShowErrorsAppSettingName =
            "MVCGridShowErrorDetail";

        internal static T GetAppSetting<T>(string name, T defaultValue)
        {
            var val = ConfigurationManager.AppSettings[name];

            if (string.IsNullOrWhiteSpace(val))
            {
                return defaultValue;
            }

            var converter = TypeDescriptor.GetConverter(typeof(T));
            var result = converter.ConvertFrom(val);

            return (T) result;
        }

        internal static bool GetShowErrorDetailsSetting()
        {
            return GetAppSetting(ShowErrorsAppSettingName, false);
        }
    }
}