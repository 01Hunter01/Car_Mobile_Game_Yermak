using UnityEngine;

namespace Rewards
{
    internal class WeeklyRewardView : RewardView
    {
        [field: Header("Settings Time Get Reward")]
        [field: SerializeField] private float _timeCooldown = 604800;
        [field: SerializeField] private float _timeDeadline = 1814400;


        public override float TimeCooldown => _timeCooldown;
        
        public override float TimeDeadline => _timeDeadline;
    }
}
