using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : Clickable
{
    protected override ButtonType type => ButtonType.MainMenu;

    protected override void OnClick()
    {
        GameModeManager.GetInstance().ToMainMenu();
    }
}
