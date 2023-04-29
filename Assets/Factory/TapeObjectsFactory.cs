using System.Linq;
using UnityEngine;

public class TapeObjectsFactory : Singleton<TapeObjectsFactory>, IFactory
{
    private Item[] items;

    public override void Awake()
    {
        base.Awake();

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
