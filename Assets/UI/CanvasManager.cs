using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CanvasType
{
    MainMenu,
    GameUI,
}
public enum PopupType
{
    Pause,
    GameOver,
    Leaderboard,
    Credits,
    Rules
}

public class CanvasManager : Singleton<CanvasManager>
{   
    private List<PanelController<CanvasType>> canvasControllersList;
    private List<PanelController<PopupType>> popupControllersList;

    private List<PanelController<PopupType>> activePopups;
    private PanelController<CanvasType> lastActiveCanvas;

    public override void Awake()
    {
        base.Awake();

        canvasControllersList = GetComponentsInChildren<PanelController<CanvasType>>().ToList();
        popupControllersList = GetComponentsInChildren<PanelController<PopupType>>().ToList();

        canvasControllersList.ForEach(x => x.gameObject.SetActive(false));
        popupControllersList.ForEach(x => x.gameObject.SetActive(false));

        activePopups = new List<PanelController<PopupType>>();

        SwitchCanvas(CanvasType.MainMenu);
    }
    public void SwitchCanvas(CanvasType _type)
    {
        if (lastActiveCanvas)
        {
            lastActiveCanvas.gameObject.SetActive(false);
        }

        PanelController<CanvasType> desiredCanvas = canvasControllersList.Find(x => x.panelType == _type);

        if (desiredCanvas)
        {
            lastActiveCanvas = desiredCanvas;
            lastActiveCanvas.gameObject.SetActive(true);
        }
        CleanPopups();
    }
    public void SwitchPopup(PopupType _type)
    {
        PanelController<PopupType> desiredPopup = popupControllersList.Find(x => x.panelType == _type);

        if (desiredPopup)
        {
            if (activePopups.Contains(desiredPopup))
            {
                desiredPopup.gameObject.SetActive(false);
                activePopups.Remove(desiredPopup);
            }
            else
            {
                desiredPopup.gameObject.SetActive(true);
                activePopups.Add(desiredPopup);
            }
        }
    }
    private void CleanPopups()
    {
        if(activePopups.Count > 0)
        {
            activePopups.ForEach(x=>x.gameObject.SetActive(false));
            activePopups.Clear();
        }
    }
}
