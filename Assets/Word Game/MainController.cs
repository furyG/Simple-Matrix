using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum gState
{
    idle = 0,
    playing = 1,
    countPoints = 2
}

public class MainController : MonoBehaviour
{
    public GameObject tapePrefab;
    public int tapeAmount = 3;

    public float originRoundTime;
    private float roundTime;

    public int originPoints;
    private int maxPoints;
    public int Points
    {
        get { return _points; }
        set
        {
            _points = value;
            C.ui.FillPoints(_points, maxPoints);
        }
    }
    private int _points;

    private gState State
    {
        get { return _state; }
        set
        {
            _state = value;
            C.ui.TogglePanel((int)value);
        }
    }
    private gState _state;

    private float startTime;
    private float leftTime;
    private Transform tapeAnchor;

    private void Awake()
    {
        C.main = this;
        C.ui = GetComponent<UIController>();
        C.points = GetComponent<PointsController>();

        tapeAnchor = new GameObject("TapeAnchor").transform;
        C.tape = tapeAnchor.AddComponent<TapeController>();
    }

    private void Start()
    {
        State = gState.idle;

        roundTime = originRoundTime;
        maxPoints = originPoints;
    }

    public void StartGame()
    {
        if (tapeAnchor.childCount > 0) C.tape.ClearField();

        C.tape.SpawnTapes(tapeAnchor, tapePrefab, tapeAmount);

        State = gState.playing;
        startTime = Time.time;
    }
    private void Update()
    {
        if(State == gState.playing)
        {
            leftTime = startTime + roundTime - Time.time;
            C.ui.ChangeTimer(leftTime, roundTime);
            if (leftTime <= 0)
            {
                State = gState.countPoints;
                Points += C.points.CountPoints(C.tape.CreateMatrix());
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartGame();
        }
    }
    public void IncreaseLevel()
    {
        roundTime += 5;
        maxPoints += 25;
        C.ui.FillPoints(Points, maxPoints);
    }
    public void BonusUp(bType type)
    {
        Debug.Log("TAKING BONUS OF TYPE: " + type);
    }
    public void ResetPoints() => Points = 0;
}
