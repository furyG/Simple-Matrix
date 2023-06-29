using Architecture;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BootstrapEntryPoint : MonoBehaviour
{
    [SerializeField] private Image loadingScreen;

    private IEnumerator Start()
    {
        Game.Init();
        PlayerLogin.Init();
        LeaderboardLogin.Init();

        var loadingDuration = 2f;
        while(loadingDuration > 0f)
        {
            loadingDuration -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("Gameplay_Scene");
    }
}
