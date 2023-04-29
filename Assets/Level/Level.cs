using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level
{
    private static LevelInteractor levelInteractor;
    public static void Initialize(LevelInteractor interactor)
    {
        levelInteractor = interactor;
    }

}
