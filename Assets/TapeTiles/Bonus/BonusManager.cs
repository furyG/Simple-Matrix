using System.Collections.Generic;
using UnityEngine;

//not implemented
public class BonusManager : MonoBehaviour, ISpawnable, ITypeChangable<BonusType>
{
    private int lifeTime = 5;
    public BonusType type => _type;
    private BonusType _type = BonusType.time;

    //private IRenderer<Image> _bonusRenderer;
    private TapeManager _tapeManager;
    private BonusTakeHandler _bonusTakeHandler;


    private void Awake()
    {
        //_bonusRenderer = GetComponent<IRenderer<Image>>();
        _bonusTakeHandler = new BonusTakeHandler(this);
        _tapeManager = transform.parent.GetComponent<TapeManager>();
    }

    protected void Start()
    {
        _bonusTakeHandler.InitializeHandler();
        SetType();
        SetStartPosition();

        Destroy(gameObject, lifeTime);
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

    public void SetStartPosition(Vector2 spawnPos = default)
    {
        float xPos = Random.Range(-0.4f, 0.4f);
        if (transform.parent.childCount > 0)
        {
            int index = Random.Range(0, transform.childCount);
            xPos = transform.parent.GetChild(index).localPosition.x;
        }
        transform.localPosition = new(xPos, 0, 0);
    }
    public BonusType SetType()
    {
        return BonusType.time; 
        ////if(type == default)
        ////{
        ////    float typeChance = Random.Range(0f, 1f);

        ////    if(typeChance > 0f && typeChance <= 0.3f) type = BonusType.time;
        
        ////    if (typeChance > 0.3f && typeChance <= 0.8f) type = BonusType.life;

        ////    if (typeChance > 0.8f) type = BonusType.slow;
        //}
        //else
        //{
        //    this._type = type;
        //}
        //return type;

        ////_bonusRenderer.ChangeSprite((int)type);
    }
}
