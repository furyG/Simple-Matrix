using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Tapes;

public enum gState
{
    idle = 0,
    playing = 1,
    countPoints = 2
}

public class MainController : MonoBehaviour
{
    [SerializeField] private GameObject tapeAnchorPrefab;
    [SerializeField] private int tapeAmount = 3;
    public float numberRunTime;

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
        C.points = GetComponent<Points>();
    }

    private void Start()
    {
        State = gState.idle;

        if(tapeAnchor == null) tapeAnchor = Instantiate(tapeAnchorPrefab).transform;

        roundTime = originRoundTime;
        maxPoints = originPoints;
    }

    public void StartGame()
    {
        tapeAnchor.GetComponent<TapeSpawner>().SpawnTapes(tapeAmount);

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
                C.points.Increment(C.points.CountPoints(GetComponent<MatrixCreator>().CreateMatrix()));
                //Points += C.points.CountPoints(GetComponent<MatrixCreator>().CreateMatrix());
            }
        }
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    StartGame();
        //}
    }
    public void IncreaseLevel()
    {
        roundTime += 5;
        maxPoints += 25;
        C.ui.FillPoints(Points, maxPoints);
    }
    public void BonusUp(bType b)
    {
        Debug.Log("TAKING BONUS OF TYPE: " + b);
    }
    public void ResetPoints() => Points = 0;
}
