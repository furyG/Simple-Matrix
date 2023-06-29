using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerLogin
{
    public static void Init()
    {
        Coroutines.StartRoutine(SetupRoutine());
    }

    private static IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
    }

    private static IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
            }
            else
            {
                Debug.Log("could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
