using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : Clickable
{
    [SerializeField] private CanvasType canvasTypeToSwitch;

    private GameUISwitcher _uISwitcher;

    protected override ButtonType type => ButtonType.None;

    protected override void Awake()
    {
        base.Awake();

        _uISwitcher = GameModeManager.GetInstance().UISwitcher;
        buttonComponent.AddListener(OnButtonClicked);
    }

    protected void OnButtonClicked()
    {
        StartCoroutine(_uISwitcher.SwitchCanvasRoutine(canvasTypeToSwitch));
    }
}
