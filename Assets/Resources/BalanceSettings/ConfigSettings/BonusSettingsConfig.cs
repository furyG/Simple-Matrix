using UnityEngine;

[CreateAssetMenu(fileName = "BonusSettingsConfig", menuName = "Gameplay/Bonus config")]
public class BonusSettingsConfig : ScriptableObject
{
    [SerializeField] private float _slowBonusDuration;
    [SerializeField] private float _timeBonusAdditionalTime;
    [SerializeField] private float bonusSpawnChance;

    public float BonusSpawnChance => bonusSpawnChance;
    public float timeBonusAdditionalTime => _timeBonusAdditionalTime;

    public float slowBonusDuration => _slowBonusDuration;

    public bool Slowed { get; set; }
}
