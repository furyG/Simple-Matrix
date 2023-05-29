public interface IState
{
    public void Enter() { }

    public void Update() { }

    public void Exit() { }
    public void Enter(ButtonType fromButton) { }

    public void Exit(ButtonType fromButton) { }
}

