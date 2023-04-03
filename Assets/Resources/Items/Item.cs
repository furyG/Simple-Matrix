using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Gameplay/New ItemInfo")]
public class Item : ScriptableObject
{
    [SerializeField] private GameObject _prefab;

    public GameObject prefab => _prefab;
}
