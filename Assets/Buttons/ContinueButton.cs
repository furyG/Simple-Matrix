using Architecture;
using TMPro;
using UnityEngine;

public class ContinueButton : Clickable
{
    [SerializeField] private TextMeshProUGUI _lifesAmountText;

    protected override ButtonType type => ButtonType.Continue;

    private LifesInteractor _lifesInteractor;

    private void OnEnable()
    {
        if(_lifesInteractor == null)
        {
            _lifesInteractor = Game.GetInteractor<LifesInteractor>();
        }
        _lifesAmountText.text = $"x{Mathf.FloorToInt(_lifesInteractor.heartScore)}";
        buttonComponent.interactable = _lifesInteractor.IsEnoughLifes(1);
    }

    protected override void OnClick()
    {
        base.OnClick();
        GameModeManager.GetInstance().ContinueGame(type);
    }
}
