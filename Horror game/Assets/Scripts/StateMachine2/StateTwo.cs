using UnityEngine;

public class StateTwo : BaseState_X
{
    public override void EnterState(StateManager_X cube)
    {
        cube.cubeRenderer.material.color = Color.yellow;
    }
    public override void UpdateState(StateManager_X cube)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cube.SwithcState(cube.StateTree);
        }
    }

    public override void ExitState(StateManager_X cube)
    {
        //
    }

    public override void OnCollisionEnter(StateManager_X cube)
    {
        //
    }

}
