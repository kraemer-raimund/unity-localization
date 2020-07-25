/*
MIT License
Copyright (c) 2020 Raimund Krämer
For the full license text please refer to the LICENSE file.
 */

using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Rakrae.Unity.Localization
{
    public static class Localization
    {
        private static IDictionary<CultureInfo, IDictionary<string, string>> _localizedStringsByLanguage =
            new Dictionary<CultureInfo, IDictionary<string, string>>();

        private static CultureInfo _customLanguage;

        public static string GetString(string key, CultureInfo culture = null)
        {
            CultureInfo language = culture ?? _customLanguage ?? CultureInfo.CurrentCulture;

            if (_localizedStringsByLanguage.TryGetValue(language, out var localizedStrings)
)
            {
                if (localizedStrings.TryGetValue(key, out var value))
                {
                    return value;
                }
            }

            return "";
        }

        public static async Task LoadStrings(ILocalizationLoader localizationLoader)
        {
            _localizedStringsByLanguage = await localizationLoader.LoadLocalizedStrings();
        }

        public static void SetCustomLanguage(CultureInfo language) => _customLanguage = language;

        public static void ResetToSystemLanguage() => _customLanguage = CultureInfo.CurrentCulture;
    }
}
