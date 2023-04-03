using Architecture;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public StateMachine MainStateMachine => mainStateMachine;

    public int Lifes
    {
        get => _lifes;
        set => _lifes = value;
    }
    private int _lifes = 0;

    private StateMachine mainStateMachine;

    private ButtonsInteractor buttonsInteractor;
    private StartButton startButton;
    private ContinueButton continueButton;

    private void Awake()
    {
        Game.Run();
        C.main = this;

        mainStateMachine = new StateMachine(this);
    }
    private void Start()
    {
        buttonsInteractor = Game.GetInteractor<ButtonsInteractor>();

        startButton = buttonsInteractor.GetButton<StartButton>();
        continueButton = buttonsInteractor.GetButton<ContinueButton>();

        startButton.buttonClicked += StartGame;
        continueButton.buttonClicked += ContinueGame;

        mainStateMachine.Initialize(mainStateMachine.idleState);    
    }
    private void OnDisable()
    {
        startButton.buttonClicked -= StartGame;
        continueButton.buttonClicked -= ContinueGame;
    }

    private void StartGame()
    {
        mainStateMachine.TransitionTo(mainStateMachine.playState);
    }
    private void ContinueGame()
    {
        mainStateMachine.TransitionTo(mainStateMachine.playState);
    }
    private void Update()
    {
        mainStateMachine.Update();
    }
}
