using UnityEngine;

public abstract class Tile : SpawnableObject
{
    protected SpriteRenderer rend { get; set; }
    protected Vector3 scale { get; set; }
    protected Transform parent { get; set; }


    protected virtual void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        parent = transform.parent;

        scale = transform.localScale;
        scale = new(scale.x / 10, 1, scale.z);
        transform.localScale = scale;
    }

    protected virtual void Start()
    {
        SetStartPos();
        SetType();
        InvokeTapeTile();
    }

    protected abstract void SetStartPos();
    protected abstract void InvokeTapeTile();
    protected abstract void SetType();
    public virtual void Interact() { }
}
