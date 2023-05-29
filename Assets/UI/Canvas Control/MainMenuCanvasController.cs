public class MainMenuCanvasController : CanvasController
{
    private void Awake()
    {
        _type = CanvasType.mainMenu; 
    }

    protected override void OnAppearAnimationOverEvent()
    {
        GameModeManager.GetInstance().ToMainMenu();
    }
}
