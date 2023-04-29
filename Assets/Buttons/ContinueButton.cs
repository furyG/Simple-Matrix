using Architecture;

public class ContinueButton : Clickable
{
    protected override ButtonType type => ButtonType.Continue;

    private void OnEnable()
    {
        if (Lifes.isInitialized)
        {
            buttonComponent.interactable = Lifes.IsEnoughLifes();
        }
    }
    protected override void OnClick()
    {
        GameModeManager.GetInstance().ContinueGame(type);
    }
}
