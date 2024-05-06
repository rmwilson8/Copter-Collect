using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryingState : PlayerBaseState
{
    public PlayerCarryingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.SetSpeed(_stateMachine.PlayerStats.CarrySpeed);
        _stateMachine.SetFuelBurnRate(_stateMachine.PlayerStats.CarryFuelBurnRate);
    }


    public override void Tick(float deltaTime)
    {
        Rotate();
        Move();
        _stateMachine.CalculateFuel(_stateMachine.CurrentFuelBurnRate * deltaTime);
    }
    public override void Exit()
    {
        
    }
}
