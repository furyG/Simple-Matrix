public class PopupSwitcher : PanelSwitcher<PopupType>
{
    protected override void OnButtonClicked()
    {
        canvasManager.SwitchPopup(panelTypeToSwitch);
    }
}
