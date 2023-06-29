using TMPro;
using UnityEngine;

public class ContinueButton : Clickable
{
    [SerializeField] private TextMeshProUGUI _lifesAmountText;

    protected override ButtonType type => ButtonType.Continue;

    protected override void OnClick()
    {
        base.OnClick();
        GameModeManager.GetInstance().ContinueGame(type);
    }
}
