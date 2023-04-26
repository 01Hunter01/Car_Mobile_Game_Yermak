using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using UnityEngine.Serialization;
using Image = UnityEngine.UIElements.Image;

namespace Tool.Bundles
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _changedBeckgroundButton;

        
        [Header("Addressables")]
        [SerializeField] private AssetReferenceSprite _spriteReference;
        [SerializeField] private RectTransform _placeForBackground;
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;

        private AsyncOperationHandle<GameObject> _addressableBackground;


        private void Start()
        {
            _changedBeckgroundButton.onClick.AddListener(LoadAssets);
            _addBackgroundButton.onClick.AddListener(LoadBackground);
            _removeBackgroundButton.onClick.AddListener(ReleaseBackground);
        }

        private void OnDestroy()
        {
            _changedBeckgroundButton.onClick.RemoveAllListeners();
            _addBackgroundButton.onClick.RemoveAllListeners();
            _removeBackgroundButton.onClick.RemoveAllListeners();
        }


        private void LoadAssets()
        {
            _changedBeckgroundButton.interactable = false;
            StartCoroutine(DownloadAndSetAssetBundles());
        }

        private void LoadBackground()
        {
            if (_spriteReference != null)
                    _spriteReference.InstantiateAsync(_placeForBackground);
            // _addressableBackground = Addressables.InstantiateAsync(_spriteReference, _placeForBackground);
        }
        
        private void ReleaseBackground()
        {
            _spriteReference.ReleaseAsset();
        }
    }
}
