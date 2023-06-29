using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    private void Awake()
    {
        TapeObjectsFactory.GetInstance().Init();
        GameModeManager.GetInstance().board.Init();
        GameModeManager.GetInstance().UISwitcher.Init();

        GameModeManager.GetInstance().StartGameFromMainMenu();
    }
}
