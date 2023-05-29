using Architecture;
using LootLocker.Requests;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private LeaderBoard leaderboard;

    private int leaderboardID = 14204;

    private void Awake()
    {
        StartCoroutine(SetupRoutine());
    }
    public void SubmitScore()
    {
        int points = Game.GetInteractor<PointsInteractor>().points;
        StartCoroutine(SubmitScoreRoutine(points));
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
    private IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
    }
    private IEnumerator LoginRoutine()
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
