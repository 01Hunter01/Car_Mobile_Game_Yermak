using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    internal class DailyRewardView : RewardView
    {
        [field: Header("Settings Time Get Reward")]
        [field: SerializeField] private float _timeCooldown = 86400;
        [field: SerializeField] private float _timeDeadline = 259200;


        public override float TimeCooldown => _timeCooldown;
        
        public override float TimeDeadline => _timeDeadline;

    }
}
