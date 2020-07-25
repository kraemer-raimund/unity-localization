/*
MIT License
Copyright (c) 2020 Raimund Krämer
For the full license text please refer to the LICENSE file.
 */

using UnityEngine;
using UnityEngine.UI;

namespace Rakrae.Unity.Localization
{
    [RequireComponent(typeof(Text))]
    public class LocalizedUIText : AbstractLocalizedText
    {
        private Text _text = null;

        private void Start()
        {
            _text = GetComponent<Text>();

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
                Debug.LogWarning($"Component {nameof(LocalizedUIText)} requires a component of type {nameof(Text)}.");
            }
        }
    }
}
