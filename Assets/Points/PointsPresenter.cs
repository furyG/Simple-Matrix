using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsPresenter : MonoBehaviour
{
    [SerializeField] Points points;
    [SerializeField] Image pointsImage;
    [SerializeField] Text pointsTxt;

    private void Start()
    {
        if(points != null)
        {
            points.PointsChanged += OnPointsChanged;
        }
        Restart();
        UpdateView();
    }
    private void OnDestroy()
    {
        if(points != null)
        {
            points.PointsChanged -= OnPointsChanged;
        }
    }
    public void PointsDown(int amount)
    {
        points?.Decrement(amount);
    }
    public void PointsUp(int amount)
    {
        points?.Increment(amount);
    }
    public void Restart()
    {
        points?.Restart();
    }
    private void UpdateView()
    {
        if (points == null) return;

        if(pointsTxt != null && pointsImage != null && points.MaxPointsOnLvl != 0)
        {
            pointsImage.fillAmount = (float)points.CurrentPoints / (float)points.MaxPointsOnLvl;
            pointsTxt.text = points.CurrentPoints + " of " + points.MaxPointsOnLvl;
        }
    }
    public void OnPointsChanged()
    {
        UpdateView();
    }
}
