/*
MIT License
Copyright (c) 2020 Raimund Krämer
For the full license text please refer to the LICENSE file.
 */

using System.Threading.Tasks;
using Rakrae.Unity.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rakrae.Unity.LocalizationExample
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private TsvLocalizationLoader _localizationLoader = null;

        private const string InGameSceneName = "Example In Game Scene";

        private async void Start()
        {
            while (!_localizationLoader.IsLoaded)
            {
                await Task.Yield();
            }

            SceneManager.LoadSceneAsync(InGameSceneName);
        }
    }
}
