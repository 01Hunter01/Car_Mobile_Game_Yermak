using Profile;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(InitialSettingsConfig), menuName = "Configs/" + nameof(InitialSettingsConfig))]
internal class InitialSettingsConfig: ScriptableObject
{
        [field: SerializeField] public float InitialSpeedCar { get; private set; }
        [field: SerializeField] public float InitialJumpHeight { get; private set; }
        [field: SerializeField] public GameState InitialState { get; private set; }
}
