using UnityEngine;

[CreateAssetMenu(fileName = "Round Settings Config", menuName = "Gameplay/round config")]
public class RoundSettingsConfig : ScriptableObject
{
    [SerializeField] private int _firstRoundTime;
    [SerializeField] private float _timerIncrementForPoint;

    public int firstRoundTime => _firstRoundTime;
    public float timerIncrementForPoint => _timerIncrementForPoint;

}
