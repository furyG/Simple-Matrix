using UnityEngine;

[CreateAssetMenu(fileName = "PointsSettingsConfig", menuName = "Gameplay/points config")]
public class PointsSettingsConfig : ScriptableObject
{
    [SerializeField] private int _firstRoundPointsCap;
    [SerializeField] private int _incrementPointsCap;
    [SerializeField] private float _pointsFlyingTime;
    [SerializeField] private int _pointsForNumber;

    public int firstRoundPointsCap => _firstRoundPointsCap;
    public int incrementPointsCap => _incrementPointsCap;

    public int pointsForNumber => _pointsForNumber;
    public float pointsFlyingTime => _pointsFlyingTime;
}
