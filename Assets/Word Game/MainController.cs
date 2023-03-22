using Architecture;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public StateMachine MainStateMachine => mainStateMachine;

    private StateMachine mainStateMachine;

    private void Awake()
    {
        Game.Run();
        C.main = this;

        mainStateMachine = new StateMachine(this);
        ButtonsController.StartButtonPushed += StartGame;
    }
    private void OnDisable()
    {
        ButtonsController.StartButtonPushed -= StartGame;
    }

    private void Start()
    {
        mainStateMachine.Initialize(mainStateMachine.idleState);    
    }

    public void StartGame()
    {
        Debug.Log("START GAME");
        mainStateMachine.TransitionTo(mainStateMachine.playState);
    }
    private void Update()
    {
        mainStateMachine.Update();
    }
    public void BonusUp(bType b)
    {
        
    }
}
