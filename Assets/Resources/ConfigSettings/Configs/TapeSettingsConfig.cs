using UnityEngine;

[CreateAssetMenu(fileName = "TapeSettingsConfig", menuName = "Gameplay/tape config")]
public class TapeSettingsConfig : ScriptableObject
{
    [SerializeField] private int _startTapesAmount;
    [SerializeField] private int _tilesOnTape;
    [SerializeField] private float _spawnNumberInTime;
    [SerializeField] private int _pointsForFirstNewTape;
    [SerializeField] private int _pointsForSecondNewTape;
    [SerializeField] private int _pointsForBlocker;
    [SerializeField] private int _maxBlockersAmount;
    [SerializeField] private float _blockersLifeTime;

    public int startTapesAmount => _startTapesAmount;
    public int tilesOnTape => _tilesOnTape;
    public float spawnNumberInTime => _spawnNumberInTime;

    public int pointsForFirstNewTape => _pointsForFirstNewTape;
    public int pointsForSecondNewTape => _pointsForSecondNewTape;

    public int pointsForBlocker => _pointsForBlocker;
    public int maxBlockersAmount => _maxBlockersAmount;
    public float blockersLifeTime => _blockersLifeTime;
}
