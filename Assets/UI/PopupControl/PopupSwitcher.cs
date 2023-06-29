using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PopupSwitcher : Clickable
{
    [SerializeField] private PopupType popupTypeToSwitch;

    protected override ButtonType type => ButtonType.None;

    protected override void Awake()
    {
        base.Awake();
        buttonComponent.AddListener(SwitchPopup);
    }

    protected void SwitchPopup()
    {
        GameModeManager.GetInstance().UISwitcher.SwitchPopup(popupTypeToSwitch);
    }
}
