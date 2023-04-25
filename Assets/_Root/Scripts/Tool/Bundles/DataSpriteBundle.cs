using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tool.Bundles
{
    [Serializable]
    internal class DataSpriteBundle
    {
        [field: SerializeField] public string NameAssetBundle { get; private set; }
        [field: SerializeField] public Image Image { get; private set; }
    }
}