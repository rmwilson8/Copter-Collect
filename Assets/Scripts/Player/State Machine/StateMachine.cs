using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State _currentState;

    // Update is called once per frame
    private void Update()
    {
        _currentState?.Tick(Time.deltaTime);
        //null conditional operator ? works for pure c# classes but not Monobehaviours or scriptable objects.
    }

    public void SwitchState(State newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
}
