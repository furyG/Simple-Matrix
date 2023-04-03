using System.Linq;
using UnityEngine;

public class TapeObjectsFactory : MonoBehaviour, IFactory
{
    public static TapeObjectsFactory instance;

    private Item[] items;

    private void Awake()
    {
        instance = this;

        items = Resources.LoadAll<Item>("Items");
    }

    public T Get<T>(Transform parent = null) where T : Component
    {
        Item item = items.FirstOrDefault(item => item.prefab.GetComponent<T>() != null);
        if (item == null) return null;

        var go = parent == null ? Instantiate(item.prefab) : Instantiate(item.prefab, parent);
        var componentToReturn = go.GetComponent<T>();

        return componentToReturn;
    }
}
