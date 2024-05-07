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
    [field: SerializeField] public bool IsBoosting { get; private set; }

    [field: SerializeField] public float RotationSpeed { get; private set; } = 50f;

    [field: SerializeField] public float DistanceToTargetThreshold { get; private set; } = 0.15f;
    [field: SerializeField] public float XMaxPosition { get; private set; } = 24;
    [field: SerializeField] public float ZMaxPosition { get; private set; } = 11;
    [field: SerializeField] public Vector3 StartingPosition { get; private set; } // where you start in the game
    [field: SerializeField] public Vector3 TargetPosition { get; private set; }


    [field: SerializeField] public Vector3 MoveDirection;

    private PlayerCollector _playerCollector;

    private void OnEnable()
    {
        _playerCollector = GetComponent<PlayerCollector>();
        _playerCollector.OnCollectedEvent += PlayerCollector_OnCollectedEvent;
        _playerCollector.OnFuelCollected += PlayerCollector_OnFuelCollected;

        _inputReader = GetComponent<InputReader>();
        _inputReader.OnBoostEvent += InputReader_OnBoostEvent;
    }

    private void OnDisable()
    {
        _inputReader.OnBoostEvent -= InputReader_OnBoostEvent;
    }

    private InputReader _inputReader;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats = GameObject.FindFirstObjectByType<PlayerStats>();

        CurrentFuel = PlayerStats.FuelCapacity;
        SwitchState(new PlayerIdleState(this));
    }

    private void Update() //can get ride of this eventually and changed StateMachines update so it isnt protected. Just did this for testing
    {
        SetSpeed();
        base.Update();
        Debug.Log(CurrentFuelBurnRate);
    }

    public void SetSpeed()
    {
        if(IsBoosting)
        {
            switch (CurrentState)
            {
                case PlayerIdleState:
                    CurrentSpeed = 0;
                    break;
                case PlayerMovingState:
                    CurrentSpeed = PlayerStats.BaseSpeed * PlayerStats.BoostSpeedFactor;
                    break;
                case PlayerCarryingMovingState:
                    CurrentSpeed = PlayerStats.CarrySpeed * PlayerStats.BoostSpeedFactor;
                    break;
                case PlayerCarryingIdleState:
                    CurrentSpeed = 0;
                    break;

                default:
                    CurrentSpeed = 0;
                    break;
            }
        }

        else
        {
            switch (CurrentState)
            {
                case PlayerIdleState:
                    CurrentSpeed = 0;
                    break;
                case PlayerMovingState:
                    CurrentSpeed = PlayerStats.BaseSpeed;
                    break;
                case PlayerCarryingMovingState:
                    CurrentSpeed = PlayerStats.CarrySpeed;
                    break;
                case PlayerCarryingIdleState:
                    CurrentSpeed = 0;
                    break;

                default:
                    CurrentSpeed = 0;
                    break;
            }
        }


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
            SwitchState(new PlayerCarryingMovingState(this));
        }

        else
        {
            SwitchState(new PlayerIdleState(this));
        }
    }

    private void PlayerCollector_OnFuelCollected(object sender, float pickedUpFuel)
    {
        CurrentFuel += pickedUpFuel;
    }

    private void InputReader_OnBoostEvent(object sender, bool isBoosting)
    {
        IsBoosting = isBoosting;

        if(IsBoosting) 
        {
            switch (CurrentState)
            {
                case PlayerIdleState:
                    CurrentFuelBurnRate = 0;
                    break;
                case PlayerMovingState:
                    CurrentFuelBurnRate = PlayerStats.FuelBurnRate * PlayerStats.BoostFuelBurnFactor;
                    break;
                case PlayerCarryingMovingState:
                    CurrentFuelBurnRate = (PlayerStats.FuelBurnRate * PlayerStats.CarryFuelBurnFactor) * PlayerStats.BoostFuelBurnFactor;
                    break;
                case PlayerCarryingIdleState:
                    CurrentFuelBurnRate = (PlayerStats.FuelBurnRate * PlayerStats.CarryIdleFuelBurnFactor) * PlayerStats.BoostFuelBurnFactor;
                    break;

                default:
                    CurrentFuelBurnRate = 0;
                    break;
            }
        }

        else
        {
            switch (CurrentState)
            {
                case PlayerIdleState:
                    CurrentFuelBurnRate = 0;
                    break;
                case PlayerMovingState:
                    CurrentFuelBurnRate = PlayerStats.FuelBurnRate;
                    break;
                case PlayerCarryingMovingState:
                    CurrentFuelBurnRate = PlayerStats.FuelBurnRate * PlayerStats.CarryFuelBurnFactor;
                    break;
                case PlayerCarryingIdleState:
                    CurrentFuelBurnRate = PlayerStats.FuelBurnRate * PlayerStats.CarryIdleFuelBurnFactor;
                    break;

                default:
                    CurrentFuelBurnRate = 0;
                    break;
            }
        }
    }
}
