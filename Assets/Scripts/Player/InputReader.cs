using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event EventHandler OnPickupEvent;
    public event EventHandler OnDropEvent;

    private PlayerControls _playerControls;

    private void OnEnable()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        _playerControls.Player.Pickup.started += (ctx) => OnPickupEvent?.Invoke(this, EventArgs.Empty);
        _playerControls.Player.Pickup.canceled += (ctx) => OnDropEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        _playerControls.Player.Pickup.started -= (ctx) => OnPickupEvent?.Invoke(this, EventArgs.Empty);
        _playerControls.Player.Pickup.canceled -= (ctx) => OnDropEvent?.Invoke(this, EventArgs.Empty);
    }
}
