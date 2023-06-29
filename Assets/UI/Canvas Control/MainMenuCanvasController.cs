public class MainMenuCanvasController : CanvasController
{
    protected override void OnAppearAnimationOverEvent()
    {
        GameModeManager.GetInstance().ToMainMenu();
    }
}
