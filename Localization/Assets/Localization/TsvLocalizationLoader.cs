/*
MIT License
Copyright (c) 2020 Raimund Krämer
For the full license text please refer to the LICENSE file.
 */

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Rakrae.Unity.Localization
{
    public class TsvLocalizationLoader : MonoBehaviour, ILocalizationLoader
    {
        [SerializeField] private TextAsset _localizedStrings = null;

        private const char Delimiter = '\t';

        public bool IsLoaded { get; private set; }

        private async void Start()
        {
            await Localization.LoadStrings(this);
            IsLoaded = true;
        }

        public async Task<IDictionary<CultureInfo, IDictionary<string, string>>> LoadLocalizedStrings()
        {
            var dictionary = new Dictionary<CultureInfo, IDictionary<string, string>>();

            string tsvContent = _localizedStrings.text;

            using (TextReader reader = new StringReader(tsvContent))
            {
                string firstLine = await reader.ReadLineAsync();
                IReadOnlyList<string> firstLineColumns = firstLine.Split(Delimiter);

                // The first two columns are reserved for keys and comments, respectively.
                IReadOnlyList<string> cultureNames = firstLineColumns.ToList().GetRange(2, firstLineColumns.Count - 2);

                IReadOnlyList<CultureInfo> cultures = ParseCultures(cultureNames);
                InitializeDictionary(cultures, dictionary);

                while (true)
                {
                    string line = await reader.ReadLineAsync();
                    if (line == null)
                    {
                        break;
                    }

                    IReadOnlyList<string> columns = line.Split(Delimiter);
                    string key = columns[0];

                    // The first two columns are reserved for keys and comments, respectively.
                    IReadOnlyList<string> localizedStrings = columns.ToList().GetRange(2, firstLineColumns.Count - 2);

                    ParseLocalizedStrings(cultures, key, localizedStrings, dictionary);
                }
            }

            return dictionary;
        }

        private IReadOnlyList<CultureInfo> ParseCultures(IReadOnlyList<string> cultureNames)
        {
            var cultures = new List<CultureInfo>();

            for (var i = 0; i < cultureNames.Count; i++)
            {
                string cultureIdentifier = cultureNames[i];

                try
                {
                    var culture = CultureInfo.GetCultureInfo(cultureIdentifier);
                    cultures.Add(culture);
                }
                catch (CultureNotFoundException)
                {
                    Debug.LogError($"Error while parsing localization file: Culture '{cultureIdentifier}' not found.");
                    throw;
                }
            }

            return cultures;
        }

        private void InitializeDictionary(
            IReadOnlyList<CultureInfo> cultures,
            IDictionary<CultureInfo, IDictionary<string, string>> dictionary
        )
        {
            foreach (var culture in cultures)
            {
                dictionary.Add(culture, new Dictionary<string, string>());
            }
        }

        private void ParseLocalizedStrings(
            IReadOnlyList<CultureInfo> cultures,
            string key,
            IReadOnlyList<string> localizedStrings,
            IDictionary<CultureInfo, IDictionary<string, string>> dictionary
        )
        {
            for (var i = 0; i < cultures.Count; i++)
            {
                CultureInfo culture = cultures[i];
                string localizedString = i < localizedStrings.Count ? localizedStrings[i] : "";
                dictionary[culture].Add(key, localizedString);
            }
        }
    }
}
