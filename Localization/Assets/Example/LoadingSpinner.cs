/*
MIT License
Copyright (c) 2020 Raimund Krämer
For the full license text please refer to the LICENSE file.
 */

using UnityEngine;

namespace Rakrae.Unity.LocalizationExample
{
    public class LoadingSpinner : MonoBehaviour
    {
        private void Update()
        {
            transform.Rotate(Vector3.forward * -180 * Time.deltaTime);
        }
    }
}
