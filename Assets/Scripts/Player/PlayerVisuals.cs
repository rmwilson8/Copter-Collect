using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private float _carryingRotationSpeed = 750f;
    [SerializeField] private float _movingRotationSpeed = 500f;
    [SerializeField] private float _idleRotationSpeed = 200f;
    [SerializeField] private Transform _rotorTransform;
    [SerializeField] private ParticleSystem _particleSystem;

    private PlayerStateMachine _playerStateMachine;
    private PlayerCollector _playerCollector;

    private float _rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _playerStateMachine = GetComponentInParent<PlayerStateMachine>();
        _playerCollector = GetComponentInParent<PlayerCollector>();

        _rotationSpeed = _playerStateMachine.RotationSpeed;
        _playerStateMachine.OnSwitchStateEvent += PlayerStateMachine_OnSwitchStateEvent;
    }

    private void Update()
    {
                _rotorTransform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);

    }


    private void PlayerStateMachine_OnSwitchStateEvent(object sender, EventArgs e)
    {
        switch(_playerStateMachine.CurrentState)
        {
            case PlayerIdleState:
                _rotationSpeed = _idleRotationSpeed;

                if (_particleSystem.isPlaying)
                {
                    _particleSystem.Stop();
                }
                break;

            case PlayerMovingState:
                _rotationSpeed = _movingRotationSpeed;

                if (_particleSystem.isPlaying)
                {
                    _particleSystem.Stop();
                }
                break;

            case PlayerCarryingState:
                _rotationSpeed = _carryingRotationSpeed;

                if (!_particleSystem.isPlaying)
                {
                    _particleSystem.Play();
                }
                break;

            default:
                _rotationSpeed = _idleRotationSpeed;

                if (_particleSystem.isPlaying)
                {
                    _particleSystem.Stop();
                }
                break;
        }
    }
}
