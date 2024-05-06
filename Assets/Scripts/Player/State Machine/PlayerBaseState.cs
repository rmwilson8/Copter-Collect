using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState : State
{

    protected PlayerStateMachine _stateMachine; // reference to player all States can use
    public PlayerBaseState(PlayerStateMachine stateMachine) //Constructor
    {
        _stateMachine = stateMachine;
    }

    protected void Move()
    {
        //convert mouse screen position to a ray
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        RaycastHit hit;
        //check if ray hits the water and if so, set that point as the _targetPosition
        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Water")))
        {
            //clamp the target position to the set boundaries
            Vector3 hitPoint = hit.point;
            float xPos = Mathf.Clamp(hitPoint.x, -_stateMachine.XMaxPosition, _stateMachine.XMaxPosition);
            float zPos = Mathf.Clamp(hitPoint.z, -_stateMachine.ZMaxPosition, _stateMachine.ZMaxPosition);
            _stateMachine.SetTargetPosition(new Vector3(xPos, _stateMachine.transform.position.y, zPos));
        }
        //moves player toward the mouse world position
        _stateMachine.transform.position = Vector3.MoveTowards(_stateMachine.transform.position, _stateMachine.TargetPosition, _stateMachine.CurrentSpeed * Time.deltaTime);
    }
    protected void Rotate()
    {
        //set the move direction(used in Rotate method)
        _stateMachine.MoveDirection = _stateMachine.TargetPosition - _stateMachine.transform.position;

        if (_stateMachine.MoveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_stateMachine.MoveDirection);
            _stateMachine.transform.rotation = Quaternion.RotateTowards(_stateMachine.transform.rotation, targetRotation,_stateMachine.RotationSpeed * Time.deltaTime);
        }
    }

    protected float CalculateDistanceToTarget()
    {
        return Vector3.Distance(_stateMachine.transform.position, _stateMachine.TargetPosition);
    }
}



