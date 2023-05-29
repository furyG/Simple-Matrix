using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CanvasType
{
    mainMenu,
    playMode,
}
public enum PopupType
{
    pause,
    gameOver,
    leaderboard,
    credits,
    rules
}

public class GameUISwitcher : MonoBehaviour
{
    [SerializeField] private List<CanvasController> canvasesList;
    private Dictionary<CanvasType, CanvasController> canvasesMap;

    [SerializeField] private List<PopupController> popupsList;
    private Dictionary<PopupType, PopupController> popupsMap;

    private CanvasController _activeCanvas;
    private PopupController _activePopup;

    private void Awake()
    {
        popupsMap = new Dictionary<PopupType, PopupController>();

        if(popupsList!= null)
        {
            foreach(var popup in popupsList)
            {
                popupsMap[popup.type] = popup;
            }
        }

        canvasesMap = new Dictionary<CanvasType, CanvasController>();
        if(canvasesList != null)
        {
            foreach(var canvas in canvasesList)
            {
                canvasesMap[canvas.type] = canvas;
            }
        }

        canvasesList.ForEach(x=> x.gameObject.SetActive(false));
        popupsList.ForEach(x => x.gameObject.SetActive(false));
    }

    public IEnumerator SwitchCanvasRoutine(CanvasType type)
    {
        DeactivateActivePopup();

        if (_activeCanvas)
        {
            _activeCanvas.CanvasDisable();
            StartCoroutine(CheckForDisable(type));
            yield break;
        }
        else
        {
            _activeCanvas = canvasesMap[type];
            _activeCanvas.gameObject.SetActive(true);
        }
    }

    private IEnumerator CheckForDisable(CanvasType type)
    {
        yield return new WaitUntil(_activeCanvas.IsCanvasDeactivated);

        _activeCanvas = canvasesMap[type];
        _activeCanvas.gameObject.SetActive(true);
    }
    private void DeactivateActivePopup()
    {
        if(_activePopup != null)
        {
            _activePopup.DisablePopup();
        }

        Time.timeScale = 1;

        _activePopup = null;
    }

    public void SwitchPopup(PopupType type)
    {
        if (popupsMap[type] == null) return;

        if (_activePopup)
        {
            _activePopup.DisablePopup();
            if(_activePopup.type == type)
            {
                _activePopup = null;
                Time.timeScale = 1;
                return;
            }
        }

        _activePopup = popupsMap[type];
        _activePopup.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

}
