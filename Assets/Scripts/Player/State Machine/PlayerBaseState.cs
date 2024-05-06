using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{

    protected PlayerStateMachine _stateMachine; // reference to player all States can use
    public PlayerBaseState(PlayerStateMachine stateMachine) //Constructor
    {
        _stateMachine = stateMachine;
    }
}
