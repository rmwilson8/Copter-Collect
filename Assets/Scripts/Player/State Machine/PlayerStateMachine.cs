using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine // inheriting StateMachine, which inherits Monobehaviour. Gains all StateMachine methods
{
    public event EventHandler OnFuelEmptyEvent;
    public PlayerStats PlayerStats { get; private set; }
    [field: SerializeField] public float CurrentSpeed { get; private set; }
    [field: SerializeField] public float CurrentFuel { get; private set; }
    [field: SerializeField] public float CurrentFuelBurnRate { get; private set; }

    [field: SerializeField] public float RotationSpeed { get; private set; } = 50f;

    [field: SerializeField] public float DistanceToTargetThreshold { get; private set; } = 0.15f;
    [field: SerializeField] public float XMaxPosition { get; private set; } = 24;
    [field: SerializeField] public float ZMaxPosition { get; private set; } = 11;
    [field: SerializeField] public Vector3 StartingPosition { get; private set; } // where you start in the game
    [field: SerializeField] public Vector3 TargetPosition { get; private set; }


    [field: SerializeField] public Vector3 MoveDirection;

    private PlayerCollector _playerCollector;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        _playerCollector = GetComponent<PlayerCollector>();
        _playerCollector.OnCollectedEvent += PlayerCollector_OnCollectedEvent;
        CurrentFuel = PlayerStats.FuelCapacity;
        SwitchState(new PlayerIdleState(this));
    }

    public void SetSpeed(float speed)
    {
        CurrentSpeed = speed;
    }

    public void SetFuelBurnRate(float fuelBurnRate)
    {
        CurrentFuelBurnRate = fuelBurnRate;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
    }

    public void CalculateFuel(float fuelBurned)
    {
        CurrentFuel -= fuelBurned;

        if(CurrentFuel <= 0)
        {
            OnFuelEmptyEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    private void PlayerCollector_OnCollectedEvent(object sender, bool e)
    {
        if(e)
        {
            SwitchState(new PlayerCarryingState(this));
        }

        else
        {
            SwitchState(new PlayerIdleState(this));
        }
    }
}
