using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsPresenter : MonoBehaviour
{
    [SerializeField] private GameObject startScreen, pointsScreen;

    private StateMachine mainStateMachine;

    private void Start()
    {
        mainStateMachine = C.main.MainStateMachine;
        
    }
    private void OnDisable()
    {
        
    }

    private void OnStartGame()
    {
        
    }

    public void OnStateChanged(IState state)
    {
        
    }
}
