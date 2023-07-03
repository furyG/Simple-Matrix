public class GameOverPopupController : PopupController
{
    protected override void OnAppearAnimationOver()
    {
        base.OnAppearAnimationOver();

        GameModeManager.GetInstance().GameOver(false);
    }
}
