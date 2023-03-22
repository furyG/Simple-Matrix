using Tapes;
using UnityEngine;
using Architecture;

public enum SpawnableType
{
    Tape,
    Number,
    Bonus,
    Points,
    Zero
}

public abstract class SpawnableObject : MonoBehaviour
{
    public abstract SpawnableType Type { get; }
}

public abstract class TapeChild : SpawnableObject
{
    protected Tape parentTape { get; set; }
    protected SpriteRenderer rend { get; set; }
    protected Vector3 scale { get; set; }
    protected float buffer { get; set; }
    protected Transform parent { get; set; }

   
    protected virtual void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        parent = transform.parent;
        parentTape = parent.GetComponent<Tape>();

        scale = transform.localScale;
        scale = new(scale.x / 10, 1, scale.z);
        transform.localScale = scale;

        buffer = scale.x / 2 + 0.8f;


    }
}

