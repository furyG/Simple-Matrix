using Architecture;
using UnityEngine;
using UnityEngine.UI;

public class LifesPresenter : MonoBehaviour
{
    [SerializeField] private Image[] lifesImages;

    [SerializeField] private Sprite[] lifesSprites;
    private LifesInteractor lifesInteractor;

    private void Start()
    {
        lifesInteractor = Game.GetInteractor<LifesInteractor>();

        if (lifesInteractor != null)
        {
            lifesInteractor.lifesAmountChanged += UpdateView;
        }
        lifesSprites = Resources.LoadAll<Sprite>("Sprites/lifes");
    }

    private void OnDestroy()
    {
        if(lifesInteractor != null)
        {
            lifesInteractor.lifesAmountChanged -= UpdateView;
        }
    }

    private void UpdateView(int lifes)
    {
        foreach(var lifesImage in lifesImages)
        {
            lifesImage.sprite = lifesSprites[lifes];
        }
    }
}
