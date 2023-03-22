using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Architecture;

public class PanelsController : MonoBehaviour
{
    [SerializeField] private GameObject startScreen, pointsScreen, playScreen;

    private StateMachine mainStateMachine;

    private void Start()
    {
        mainStateMachine = C.main.MainStateMachine;
        mainStateMachine.stateChanged += OnStateChanged;
    }
    private void OnDisable()
    {
        mainStateMachine.stateChanged -= OnStateChanged;
    }

    public void OnStateChanged(IState state)
    {
        startScreen.SetActive(state == mainStateMachine.idleState);
        pointsScreen.SetActive(state == mainStateMachine.countState);
    }
}
