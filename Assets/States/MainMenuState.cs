public class MainMenuState : IState
{
    private GameModeManager _mainController;
    public MainMenuState(GameModeManager mainController)
    {
        this._mainController = mainController;
    }
}
