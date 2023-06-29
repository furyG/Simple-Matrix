using Architecture;
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
    private AudioInteractor _audioInteractor;

    protected virtual void Awake()
    {
        buttonComponent = GetComponent<Button>();
        buttonComponent.AddListener(OnClick);

        _audioInteractor = Game.GetInteractor<AudioInteractor>();
    }

    protected virtual void OnClick()
    {
        _audioInteractor?.PlayButtonClickedSound();
    }
}
