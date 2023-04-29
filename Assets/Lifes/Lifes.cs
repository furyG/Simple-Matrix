using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Lifes
{
    public static int lifes => lifesInteractor.lifes;

    private static LifesInteractor lifesInteractor;

    public static bool isInitialized { get; private set; }

    public static void Initialize(LifesInteractor interactor)
    {
        lifesInteractor = interactor;
        isInitialized = true;
    }

    public static void AddLife(object sender, int amount) => lifesInteractor.AddLife(sender, amount);

    public static void RemoveLife(object sender, int amount) => lifesInteractor.RemoveLife(sender, amount);
    public static bool IsEnoughLifes() => lifesInteractor.IsEnoughLifes();

    public static void ResetLifes() => lifesInteractor.ResetLifes();
}
