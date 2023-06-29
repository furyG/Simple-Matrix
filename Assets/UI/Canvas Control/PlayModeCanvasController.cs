public class PlayModeCanvasController : CanvasController
{
    protected override void OnAppearAnimationOverEvent()
    {
        GameModeManager.GetInstance().StartNewGame(ButtonType.Start);
    }
}
