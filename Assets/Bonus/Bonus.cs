using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public enum bType
    {
        time = 0,
        slow = 1
    }

public class Bonus : MonoBehaviour
{
    public int lifeTime = 5;

    public bType type = bType.time;

    private SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        float chance = Random.Range(0.0f, 1.0f);
        if (chance > 0.5f) type = bType.time;
        else type = bType.slow;

        InvokeBonus();
    }

    private void InvokeBonus()
    {
        Sprite[] bSprites = Resources.LoadAll<Sprite>("Sprites/bonuses");
        rend.sprite = bSprites[(int)type];

        Destroy(gameObject, lifeTime);
    }
}
