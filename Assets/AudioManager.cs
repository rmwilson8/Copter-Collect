using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _playerIdleClip;
    [SerializeField] private AudioClip _playerMovingClip;
    [SerializeField] private AudioClip _playerCarryingClip;
    [SerializeField] private AudioClip _playerCarryingIdleClip;

    private AudioSource _audioSource;
    private PlayerStateMachine _playerStateMachine;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerStateMachine = GameObject.FindFirstObjectByType<PlayerStateMachine>();
        _playerStateMachine.OnSwitchStateEvent += PlayerStateMachine_OnSwitchStateEvent;
    }

    private void PlayerStateMachine_OnSwitchStateEvent(object sender, EventArgs e)
    {
        switch(_playerStateMachine.CurrentState)
        {
            case PlayerIdleState:
                _audioSource.clip = _playerIdleClip;
                break;
            case PlayerMovingState:
                _audioSource.clip = _playerMovingClip;
                break;
            case PlayerCarryingMovingState:
                _audioSource.clip = _playerCarryingClip;
                break;
            case PlayerCarryingIdleState:
                _audioSource.clip = _playerCarryingIdleClip;
                break;
            
        }

        _audioSource.Play();
    }
}
