using UnityEngine;
using System.Collections.Generic;

public class BonusManager : MonoBehaviour, ISpawnable, ITypeChangable<BonusType>
{
    private int lifeTime = 5;
    private BonusType type = BonusType.time;

    private IRenderer _bonusRenderer;
    private TapeManager _tapeManager;
    private BonusTakeHandler _bonusTakeHandler;

    private void Awake()
    {
        _bonusRenderer = GetComponent<IRenderer>();
        _bonusTakeHandler = new BonusTakeHandler(this);
        _tapeManager = transform.parent.GetComponent<TapeManager>();
    }

    protected void Start()
    {
        if(_tapeManager)
        {
            _tapeManager.OnChildSort += CheckForNearNumber;
        }

        _bonusTakeHandler.InitializeHandler();
        SetType();
        SetStartPosition();

        Destroy(gameObject, lifeTime);
    }
    private void OnDestroy()
    {
        if(_tapeManager)
        {
            _tapeManager.OnChildSort -= CheckForNearNumber;
        }
    }

    private void CheckForNearNumber(TapeManager t, List<NumberManager> nums)
    {
        float x = t.lastNumber.transform.localPosition.x;
        if(x == transform.localPosition.x)
        {
            TakeBonus();
        }
    }

    private void TakeBonus()
    {
        _bonusTakeHandler?.TakeBonus(type);

        Destroy(gameObject);
    }

    public void SetStartPosition()
    {
        float xPos = Random.Range(-0.4f, 0.4f);
        if (transform.parent.childCount > 0)
        {
            int index = Random.Range(0, transform.childCount);
            xPos = transform.parent.GetChild(index).localPosition.x;
        }
        transform.localPosition = new(xPos, 0, 0);
    }
    public void SetType(BonusType type = default)
    {
        if(type == default)
        {
            float typeChance = Random.Range(0f, 1f);

            if(typeChance > 0f && typeChance <= 0.3f) type = BonusType.time;
        
            if (typeChance > 0.3f && typeChance <= 0.8f) type = BonusType.life;

            if (typeChance > 0.8f) type = BonusType.slow;
        }
        else
        {
            this.type = type;
        }

        _bonusRenderer.ChangeSprite((int)type);
    }
}
