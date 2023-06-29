using Architecture;
using TMPro;
using UnityEngine;

public class PointsPresenter : MonoBehaviour, IFloatingElementReportable
{
    [SerializeField] private TextMeshProUGUI pointsTxt;

    private PointsInteractor pointsInteractor;

    private void Start()
    {
        pointsInteractor = Game.GetInteractor<PointsInteractor>();

        if(pointsInteractor != null)
        {
            pointsInteractor.pointsChanged += OnPointsChangedEvent;
        }
    }

    private void OnDestroy()
    {
        if (pointsInteractor != null)
        {
            pointsInteractor.pointsChanged -= OnPointsChangedEvent;
        }
    }
    private void UpdateView()
    {
        if (pointsInteractor == null) return;

        if(pointsTxt != null)
        {
            pointsTxt.text = pointsInteractor.points.ToString();
        }
    }
    private void OnPointsChangedEvent(int points)
    {
        UpdateView();
    }

    public void FloatingElementCallBack()
    {
        Debug.Log("recieved");
    }
}
