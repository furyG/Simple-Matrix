using UnityEngine;

[CreateAssetMenu(fileName = "PointsSettingsConfig", menuName = "Gameplay/points config")]
public class PointsSettingsConfig : ScriptableObject
{
    [SerializeField] private float _pointsFlyingTime;
    [SerializeField] private int _pointsForNumber;

    public int pointsForNumber => _pointsForNumber;
    public float pointsFlyingTime => _pointsFlyingTime;
}
