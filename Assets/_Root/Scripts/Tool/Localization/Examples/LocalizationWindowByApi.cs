using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Tool.Localization.Examples
{
    internal class LocalizationWindowByApi : LocalizationWindow
    {
        [Header("Localization Settings")]
        [SerializeField] private string _tableNameText;
        [SerializeField] private string _tableNameAsset;
        
        [Header("Modification Text")]
        [SerializeField] private List<TMP_Text> _changeTexts;
        [SerializeField] private List<string> _textTags;
        
        [Header("Modification Asset")]
        [SerializeField] private List<Image> _changeAssets;
        [SerializeField] private string _assetTag;
            
        private Dictionary<string, TMP_Text> _unitedTagsAndTMPTexts;

        protected override void OnStarted()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
            UpdateTextAsync();
        }

        protected override void OnDestroyed()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }


        private void OnSelectedLocaleChanged(Locale locale)
        {
            UpdateTextAsync();
            UpdateAssetAsync();
        }

        private void UpdateTextAsync() =>
            LocalizationSettings.StringDatabase.GetTableAsync(_tableNameText).Completed +=
                handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        StringTable table = handle.Result;
                        Dictionary<string, TMP_Text> localization = UniteTagAndText();
                        foreach (var local in localization)
                        {
                            local.Value.text = table.GetEntry(local.Key)?.GetLocalizedString();
                        }
                    }
                    else
                    {
                        string errorMessage = $"[{GetType().Name}] Could not load String Table: {handle.OperationException}";
                        Debug.LogError(errorMessage);
                    }
                };

        private void UpdateAssetAsync() =>
            LocalizationSettings.AssetDatabase.GetTableAsync(_tableNameAsset).Completed +=
                handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        AssetTable table = handle.Result;
                        foreach (Image asset in _changeAssets)
                        {
                            table.GetEntry(_assetTag)?.SetAssetOverride(asset.sprite);
                        }
                    }
                    else
                    {
                        string errorMessage = $"[{GetType().Name}] Could not load String Table: {handle.OperationException}";
                        Debug.LogError(errorMessage);
                    }
                };

        private Dictionary<string, TMP_Text> UniteTagAndText()
        {
            _unitedTagsAndTMPTexts = new Dictionary<string, TMP_Text>();
            
            if (_textTags.Count == _changeTexts.Count)
            {
                for (int i = 0; i < _textTags.Count; i++)
                {
                    _unitedTagsAndTMPTexts.Add(_textTags[i], _changeTexts[i]);
                }
            }
            else
            {
                string errorMessage = $"Quantity elements of {nameof(_textTags)} is not equal to {nameof(_changeTexts)}";
                Debug.LogError(errorMessage);
            }

            return _unitedTagsAndTMPTexts;
        }
    }
}
