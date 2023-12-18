public abstract class BaseState_X 
{
    public abstract void EnterState(StateManager_X cube);

    public abstract void UpdateState(StateManager_X cube);

    public abstract void ExitState(StateManager_X cube);

    public abstract void OnCollisionEnter(StateManager_X cube);
}
