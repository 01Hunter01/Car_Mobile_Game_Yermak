using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Rewards
{
    internal class RewardView : MonoBehaviour
    {
        private const string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
        private const string TimeGetRewardKey = nameof(TimeGetRewardKey);

        public virtual float TimeCooldown { get; private set; }
        public virtual float TimeDeadline { get; private set; }
        

        [field: Header("Settings Rewards")]
        [field: SerializeField] public List<Reward> Rewards { get; private set; }

        [field: Header("Ui Elements")]
        [field: SerializeField] public TMP_Text TimerNewReward { get; private set; }
        [field: SerializeField] public Transform MountRootSlotsReward { get; private set; }
        [field: SerializeField] public ContainerSlotRewardView ContainerSlotRewardPrefab { get; private set; }
        [field: SerializeField] public Button GetRewardButton { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }

        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeGetRewardKey);
                return !string.IsNullOrEmpty(data) ? DateTime.Parse(data) : null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }
        
    }
}