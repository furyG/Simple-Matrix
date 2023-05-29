using UnityEngine;

[CreateAssetMenu(fileName = "LifesSettingsConfig", menuName = "Gameplay/Lifes config")]
public class LifesSettingsConfig : ScriptableObject
{
    [SerializeField] private float _maxHeartScore;
    [SerializeField] private float _startHeartScoreAmount;
    [SerializeField] private float _heartPointsForOnePoint;

    public float maxHeartScore => _maxHeartScore;
    public float startHeartScoreAmount => _startHeartScoreAmount;

    public float heartPointsForOnePoint => _heartPointsForOnePoint;

}
