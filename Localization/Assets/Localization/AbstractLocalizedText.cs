/*
MIT License
Copyright (c) 2020 Raimund Krämer
For the full license text please refer to the LICENSE file.
 */

using System.Globalization;
using UnityEngine;

namespace Rakrae.Unity.Localization
{
    public abstract class AbstractLocalizedText : MonoBehaviour
    {
        [SerializeField] private string _localizedStringKey = null;
        [SerializeField] private bool _useCustomCulture = false;
        [SerializeField] private string _culture = null;

        protected string LocalizedString
        {
            get
            {
                if (_useCustomCulture)
                {
                    CultureInfo culture = CultureInfo.GetCultureInfo(_culture);
                    return Localization.GetString(_localizedStringKey, culture);
                }

                return Localization.GetString(_localizedStringKey);
            }
        }

        protected abstract void SetUIText();
    }
}
