using UnityEngine;

public class PanelsController : MonoBehaviour
{
    [SerializeField] private GameObject pointsScreen, playScreen, pauseScreen;

    private ButtonsInteractor buttonsInteractor;
    private StateMachine mainStateMachine;

    private void Start()
    {
        mainStateMachine = C.main.MainStateMachine;
        mainStateMachine.onStateChanged += OnStateChanged;
    }
    private void OnDisable()
    {
        mainStateMachine.onStateChanged -= OnStateChanged;
    }

    public void OnStateChanged(IState state)
    {
        pointsScreen.SetActive(state == mainStateMachine.countState);
    }
}
