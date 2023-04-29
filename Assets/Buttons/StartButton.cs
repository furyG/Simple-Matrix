using UnityEngine;

public class StartButton : Clickable
{
    protected override ButtonType type { get => ButtonType.Start; }

    private GameModeManager _mainController;

    protected override void Awake()
    {
        base.Awake();

        _mainController = GameModeManager.GetInstance();
    }
    protected override void OnClick()
    {
        _mainController.StartNewGame(type);
    }
}
