using Tapes;
using UnityEngine;
using Architecture;



public class Bonus : Tile
{
    private int lifeTime = 5;
    private bonusType type = bonusType.time;

    private float buffer = 0;
    private Tape tape;

    private BonusInteractor bonusInteractor;

    protected override void Start()
    {
        base.Start();

        buffer = scale.x / 2 + 0.1f;
        tape = parent.GetComponent<Tape>();
        if(tape)
        {
            tape.tapeClicked += CheckForNearNumber;
        }

        bonusInteractor = Game.GetInteractor<BonusInteractor>();
    }
    private void OnDestroy()
    {
        if(tape)
        {
            tape.tapeClicked -= CheckForNearNumber;
        }
    }

    protected override void SetStartPos()
    {
        float xPos = Random.Range(-0.4f, 0.4f);
        if (parent.childCount > 0)
        {
            int index = Random.Range(0, transform.childCount);
            xPos = parent.GetChild(index).localPosition.x;
            if (parent.GetChild(index).name != 0.ToString())
            {
                xPos -= buffer * Mathf.Sign(xPos);
            }
        }
        transform.localPosition = new(xPos, 0, 0);
    }
    protected override void SetType()
    {
        int typeIndex = Random.Range(0, 3);
        type = (bonusType)typeIndex;
    }

    protected override void InvokeTapeTile()
    {
        Sprite[] bSprites = Resources.LoadAll<Sprite>("Sprites/bonuses");
        rend.sprite = bSprites[(int)type];

        Destroy(gameObject, lifeTime);
    }
    private void CheckForNearNumber(Tape t)
    {
        float x = t.Number.transform.localPosition.x;
        if(Mathf.Abs(x - transform.localPosition.x) <= buffer)
        {
            TakeBonus();
        }
    }

    private void TakeBonus()
    {
        bonusInteractor?.TakeBonus(type);

        Destroy(gameObject);
    }
}
