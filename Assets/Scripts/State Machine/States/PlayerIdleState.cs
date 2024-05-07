using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) //basically says to call the base constructor in PlayerBaseState and passes in the state machine reference it needs.
    {
    }

    public override void Enter()
    {
        _stateMachine.SetFuelBurnRate(0);
    }
    public override void Tick(float deltaTime)
    {
        Rotate();
        Move();
        _stateMachine.CalculateFuel(_stateMachine.CurrentFuelBurnRate * deltaTime);

        float distanceToTarget = Vector3.Distance(_stateMachine.transform.position, _stateMachine.TargetPosition);

        if(distanceToTarget >= _stateMachine.DistanceToTargetThreshold)
        {
            _stateMachine.SwitchState(new PlayerMovingState(_stateMachine));
            return;
        }

    }

    public override void Exit()
    {
        
    }
}
