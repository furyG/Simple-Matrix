using UnityEngine;
using UnityEngine.UI;
using Architecture;

public class LevelPresenter : MonoBehaviour
{
    [SerializeField] private Text lvlTxt;

    private LevelInteractor levelInteractor;

    private void Start()
    {
        levelInteractor = Game.GetInteractor<LevelInteractor>();
        if (levelInteractor != null)
        {
            levelInteractor.levelChangedEvent += OnLevelChanged;
        }
    }
    private void OnDestroy()
    {
        if(levelInteractor != null) levelInteractor.levelChangedEvent -= OnLevelChanged;
    }

    public void NextLevel()
    {
        levelInteractor?.NextLevel();
    }
    public void NewGame()
    {
        levelInteractor?.NewGame();
    }

    public void UpdateView()
    {
        if (levelInteractor == null) return;

        if(lvlTxt != null && levelInteractor.currentLevel != 0)
        {
            lvlTxt.text = "Level: " + levelInteractor.currentLevel;
        }
    }

    public void OnLevelChanged()
    {
        UpdateView();
    }
}
