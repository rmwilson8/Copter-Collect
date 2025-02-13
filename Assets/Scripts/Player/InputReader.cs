using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event EventHandler OnPickupEvent;
    public event EventHandler OnDropEvent;
    public event EventHandler<bool> OnToggleControlsEvent;
    public event EventHandler OnPauseEvent;

    public event EventHandler<bool> OnBoostEvent;
    private PlayerControls _playerControls;

    private void OnEnable()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        _playerControls.Player.Pickup.started += (ctx) => OnPickupEvent?.Invoke(this, EventArgs.Empty);
        _playerControls.Player.Pickup.canceled += (ctx) => OnDropEvent?.Invoke(this, EventArgs.Empty);
        _playerControls.Player.Boost.started += (ctx) => OnBoostEvent?.Invoke(this, true);
        _playerControls.Player.Boost.canceled += (ctx) => OnBoostEvent?.Invoke(this, false);
        _playerControls.Player.ToggleControls.started += (ctx) => OnToggleControlsEvent?.Invoke(this, true);
        _playerControls.Player.ToggleControls.canceled += (ctx) => OnToggleControlsEvent?.Invoke(this, false);
        _playerControls.Player.Pause.performed += (ctx) => OnPauseEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        _playerControls.Player.Pickup.started -= (ctx) => OnPickupEvent?.Invoke(this, EventArgs.Empty);
        _playerControls.Player.Pickup.canceled -= (ctx) => OnDropEvent?.Invoke(this, EventArgs.Empty);
        _playerControls.Player.Boost.started -= (ctx) => OnBoostEvent?.Invoke(this, true);
        _playerControls.Player.Boost.canceled -= (ctx) => OnBoostEvent?.Invoke(this, false);
        _playerControls.Player.ToggleControls.started -= (ctx) => OnToggleControlsEvent?.Invoke(this, true);
        _playerControls.Player.ToggleControls.canceled -= (ctx) => OnToggleControlsEvent?.Invoke(this, false);
        _playerControls.Player.Pause.performed -= (ctx) => OnPauseEvent?.Invoke(this, EventArgs.Empty);

    }
}
