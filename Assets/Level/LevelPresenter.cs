using UnityEngine;
using UnityEngine.UI;
using Architecture;
using TMPro;

public class LevelPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lvlTxt;

    private LevelInteractor levelInteractor;

    private void Start()
    {
        levelInteractor = Game.GetInteractor<LevelInteractor>();
        if (levelInteractor != null)
        {
            levelInteractor.levelChanged += OnLevelChanged;
        }
    }
    private void OnDestroy()
    {
        if(levelInteractor != null) levelInteractor.levelChanged -= OnLevelChanged;
    }

    public void UpdateView(int currentLevel)
    {
        if (levelInteractor == null) return;

        if(lvlTxt != null && currentLevel != 0)
        {
            lvlTxt.text = "ур: " + currentLevel;
        }
    }

    public void OnLevelChanged(int currentLevel)
    {
        UpdateView(currentLevel);
    }
}
