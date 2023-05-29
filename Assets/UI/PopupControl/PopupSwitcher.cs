using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PopupSwitcher : MonoBehaviour
{
    [SerializeField] private PopupType popupTypeToSwitch;
    private Button buttonComponent;

    protected void Start()
    {
        buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.AddListener(SwitchPopup);
    }

    protected void SwitchPopup()
    {
        GameModeManager.GetInstance().UISwitcher.SwitchPopup(popupTypeToSwitch);
    }
}
