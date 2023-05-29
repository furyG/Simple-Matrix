using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    None,
    Start,
    Continue,
    Pause,
    MainMenu,
    Leaderboard,
    Credits,
    GameOver,
    SaveScore,
    ChangeLanguage
}

[RequireComponent(typeof(Button))]
public abstract class Clickable : MonoBehaviour
{
    protected abstract ButtonType type { get; }

    protected Button buttonComponent;

    protected virtual void Awake()
    {
        buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.AddListener(OnClick);
    }

    protected abstract void OnClick();
}
