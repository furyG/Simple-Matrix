using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Tapes;

public abstract class TapeTile : MonoBehaviour
{
    public Tape pTape { get; private set; }
    protected SpriteRenderer rend { get; private set; }
    protected Vector3 scale { get; private set; }
    protected float buffer { get; private set; }
    protected Transform parent { get; private set; }

    protected virtual void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        parent = transform.parent;
        pTape = parent.GetComponent<Tape>();

        scale = transform.localScale;
        scale = new(scale.x / 10, 1, scale.z);
        transform.localScale = scale;

        buffer = scale.x / 2 + 0.8f;
    }
    public virtual void Interact() { }
}
