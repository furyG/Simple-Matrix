using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class PanelSwitcher<T> : MonoBehaviour where T : System.Enum
{
    public T panelTypeToSwitch;
    protected Button buttonComponent;
    protected CanvasManager canvasManager;

    protected void Start()
    {
        buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.AddListener(OnButtonClicked);
        canvasManager = CanvasManager.GetInstance();
    }

    protected abstract void OnButtonClicked();
    
}
