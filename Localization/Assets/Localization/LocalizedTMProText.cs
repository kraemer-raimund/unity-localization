/*
MIT License
Copyright (c) 2020 Raimund Krämer
For the full license text please refer to the LICENSE file.
 */

using UnityEngine;
using TMPro;

namespace Rakrae.Unity.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTMProText : AbstractLocalizedText
    {
        private TextMeshProUGUI _text = null;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();

            SetUIText();
        }

        protected override void SetUIText()
        {
            if (_text)
            {
                _text.text = LocalizedString;
            }
            else
            {
                Debug.LogWarning($"Component {nameof(LocalizedUIText)} requires a component of type {nameof(TextMeshProUGUI)}.");
            }
        }
    }
}
