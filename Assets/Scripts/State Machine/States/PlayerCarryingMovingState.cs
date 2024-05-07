using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryingMovingState : PlayerBaseState
{
    public PlayerCarryingMovingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.SetFuelBurnRate(_stateMachine.PlayerStats.FuelBurnRate * _stateMachine.PlayerStats.CarryFuelBurnFactor);
    }


    public override void Tick(float deltaTime)
    {
        Rotate();
        Move();
        _stateMachine.CalculateFuel(_stateMachine.CurrentFuelBurnRate * deltaTime);

        if(CalculateDistanceToTarget() < _stateMachine.DistanceToTargetThreshold)
        {
            _stateMachine.SwitchState(new PlayerCarryingIdleState(_stateMachine));
        }
    }
    public override void Exit()
    {
        
    }
}
