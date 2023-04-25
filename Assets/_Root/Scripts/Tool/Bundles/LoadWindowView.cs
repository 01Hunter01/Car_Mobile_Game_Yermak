using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using Image = UnityEngine.UIElements.Image;

namespace Tool.Bundles
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _changedBeckgroundButton;

        [Header("Addressables")]
        [SerializeField] private AssetReference _downloadedBackground;
        [SerializeField] private RectTransform _placeForBackground;
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;

        private AsyncOperationHandle<GameObject> _addressableBackground;


        private void Start()
        {
            _changedBeckgroundButton.onClick.AddListener(LoadAssets);
            _addBackgroundButton.onClick.AddListener(DownloadBackground);
            _removeBackgroundButton.onClick.AddListener(UnloadBackground);
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

        private void DownloadBackground()
        {
            _addressableBackground = Addressables.InstantiateAsync(_downloadedBackground, _placeForBackground);
            
        }
        
        private void UnloadBackground()
        {
            Addressables.Release(_addressableBackground);
        }
    }
}
