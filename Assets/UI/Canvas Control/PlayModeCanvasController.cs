public class PlayModeCanvasController : CanvasController
{
    private void Awake()
    {
        _type = CanvasType.playMode;
    }

    protected override void OnAppearAnimationOverEvent()
    {
        GameModeManager.GetInstance().StartNewGame(ButtonType.Start);
    }
}
