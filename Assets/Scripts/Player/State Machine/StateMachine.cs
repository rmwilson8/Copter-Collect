using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }
    public event EventHandler OnSwitchStateEvent;

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(CurrentState);
        CurrentState?.Tick(Time.deltaTime);
        //null conditional operator ? works for pure c# classes but not Monobehaviours or scriptable objects.
    }

    public void SwitchState(State newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();

        OnSwitchStateEvent?.Invoke(this, EventArgs.Empty);
    }
}