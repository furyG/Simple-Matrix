using UnityEngine;

public class MainController : MonoBehaviour
{
    public Level Lvl { get; private set; }
    public Timer Timer { get; private set; }
    public Points Points{ get; private set; }

    public StateMachine MainStateMachine => mainStateMachine;

    private StateMachine mainStateMachine;

    private void Awake()
    {
        C.main = this;

        Points = GetComponent<Points>();
        Lvl = GetComponent<Level>();
        Timer = GetComponent<Timer>();

        mainStateMachine = new StateMachine(this);
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
