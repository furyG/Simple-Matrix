using Architecture;
using LootLocker.Requests;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const int leaderboardID = 14204;

    public void SubmitScore()
    {
        int points = Game.GetInteractor<PointsInteractor>().points;
        if(points > 0)
        {
            StartCoroutine(SubmitScoreRoutine(points));
        }
    }
    public void SetPlayerName(string playerName)
    {
        LootLockerSDKManager.SetPlayerName(playerName, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Succesfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name " + response.Error);
            }
        });
    }
    private IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed: " + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
