using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public RectTransform[] panels;

    private Button restartGame, continueGame;
    private Transform leadersPanel, timer, points;
    private Image timerFill, pointsFill;
    private Text pointsTxt;

    private void Awake()
    {
        timer = panels[1].GetChild(0);
        timerFill = timer.GetChild(0).GetComponent<Image>();

        points = panels[1].GetChild(1);
        pointsFill = points.GetChild(0).GetComponent<Image>();

        leadersPanel = panels[0].GetChild(2);

        restartGame = panels[2].GetChild(0).GetComponent<Button>();
        continueGame = panels[2].GetChild(1).GetComponent<Button>();
        pointsTxt = panels[2].GetChild(2).GetComponent<Text>();
    }

    public void ChangeTimer(float leftTime, float roundTime)
    {
        timerFill.fillAmount = leftTime / roundTime;
    }
    /// <summary>
    /// 0 - startPanel, 1 - playPanel, 2 - countPanel
    /// </summary>
    /// <param name="num"></param>
    public void TogglePanel(int num)
    {
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].gameObject.SetActive(num == i);
        }
    }
    public void FillPoints(float points, float maxPoints)
    {
        pointsTxt.text = points + " of " + maxPoints;

        pointsFill.fillAmount = points / maxPoints;

        continueGame.interactable = points >= maxPoints;
    }
}
