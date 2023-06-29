using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    [SerializeField] private CanvasType canvasTypeToSwitch;

    private Button _buttonComponent;
    private GameUISwitcher _uISwitcher;
    
    protected void Awake()
    {
        _buttonComponent = GetComponent<Button>();
        _buttonComponent.AddListener(OnButtonClicked);
        _uISwitcher = GameModeManager.GetInstance().UISwitcher;
    }

    protected void OnButtonClicked()
    {
        StartCoroutine(_uISwitcher.SwitchCanvasRoutine(canvasTypeToSwitch));
    }
}
