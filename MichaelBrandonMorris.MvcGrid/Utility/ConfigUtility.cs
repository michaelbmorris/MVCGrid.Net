using System.ComponentModel;
using System.Configuration;

namespace MichaelBrandonMorris.MvcGrid.Utility
{
    /// <summary>
    ///     Class ConfigUtility.
    /// </summary>
    /// TODO Edit XML Comment Template for ConfigUtility
    internal class ConfigUtility
    {
        /// <summary>
        ///     The show errors application setting name
        /// </summary>
        /// TODO Edit XML Comment Template for ShowErrorsAppSettingName
        internal const string ShowErrorsAppSettingName =
            "MVCGridShowErrorDetail";

        /// <summary>
        ///     Gets the application setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>T.</returns>
        /// TODO Edit XML Comment Template for GetAppSetting`1
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

        /// <summary>
        ///     Gets the show error details setting.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// TODO Edit XML Comment Template for GetShowErrorDetailsSetting
        internal static bool GetShowErrorDetailsSetting()
        {
            return GetAppSetting(ShowErrorsAppSettingName, false);
        }
    }
}