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
    public interface ILocalizationLoader
    {
        Task<IDictionary<CultureInfo, IDictionary<string, string>>> LoadLocalizedStrings();
    }
}
