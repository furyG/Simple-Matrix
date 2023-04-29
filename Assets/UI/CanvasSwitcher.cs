public class CanvasSwitcher : PanelSwitcher<CanvasType>
{
    protected override void OnButtonClicked()
    {
        canvasManager.SwitchCanvas(panelTypeToSwitch);
    }
}
