using Architecture;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopupController : PopupController
{
    [SerializeField] private TextMeshProUGUI _lifesAmountText;
    [SerializeField] private Button _continueButton;

    private LifesInteractor _lifesInteractor;

    protected override async void OnEnable()
    {
        base.OnEnable();
        
        await Task.Delay(1);

        _lifesInteractor = Game.GetInteractor<LifesInteractor>();
        if (_lifesInteractor != null)
        {
            _lifesAmountText.text = $"x{Mathf.FloorToInt(_lifesInteractor.heartScore)}";
            _continueButton.interactable = _lifesInteractor.IsEnoughLifes();
        }
    }
    protected override void OnAppearAnimationOver()
    {
        base.OnAppearAnimationOver();

        GameModeManager.GetInstance().GameOver(false);
    }
}
