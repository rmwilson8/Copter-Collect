using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    public PlayerMovingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.SetSpeed(_stateMachine.PlayerStats.BaseSpeed);
        _stateMachine.SetFuelBurnRate(_stateMachine.PlayerStats.FuelBurnRate);
    }
    public override void Tick(float deltaTime)
    {
        Rotate();
        Move();
        _stateMachine.CalculateFuel(_stateMachine.CurrentFuelBurnRate * deltaTime);

        float distanceToTarget = CalculateDistanceToTarget();

        if (distanceToTarget <= _stateMachine.DistanceToTargetThreshold)
        {
            _stateMachine.SwitchState(new PlayerIdleState(_stateMachine));
        }

    }

    public override void Exit()
    {

    }
}
