using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverButton : Clickable
{
    protected override ButtonType type => ButtonType.GameOver;

    protected override void OnClick()
    {
        GameModeManager.GetInstance().GameOver(type);
    }
}
