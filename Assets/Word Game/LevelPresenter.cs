using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPresenter : MonoBehaviour
{
    [SerializeField] private Level lvl;
    [SerializeField] private Text lvlTxt;

    private void Start()
    {
        if (lvl != null)
        {
            lvl.LevelChanged += OnLevelChanged;
        }
        //Button.OnStartClicked += NewGame;
        Button.OnContinueClicked += NextLevel;
        UpdateView();
    }
    private void OnDestroy()
    {
        if(lvl != null) lvl.LevelChanged -= OnLevelChanged;
        Button.OnContinueClicked -= NextLevel;
        //Button.OnStartClicked -= NewGame;
    }

    public void NextLevel()
    {
        lvl?.NextLevel();
    }
    public void NewGame()
    {
        lvl?.NewGame();
    }

    public void UpdateView()
    {
        if (lvl == null) return;

        if(lvlTxt != null && lvl.CurrentLevel != 0)
        {
            lvlTxt.text = "Level: " + lvl.CurrentLevel;
        }
    }

    public void OnLevelChanged(float lvlChangeTime)
    {
        UpdateView();
    }
}
