using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _playerIdleClip;
    [SerializeField] private AudioClip _playerMovingClip;
    [SerializeField] private AudioClip _playerCarryingClip;

    private AudioSource _audioSource;
    private PlayerMover _playerMover;
    private PlayerCollector _playerCollector;
    
    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerMover = GameObject.FindFirstObjectByType<PlayerMover>();
        _playerCollector = GameObject.FindFirstObjectByType<PlayerCollector>();

        _playerMover.OnMoveEvent += PlayerMover_OnMoveEvent;
        _playerCollector.OnCollectedEvent += PlayerCollector_OnCollectedEvent;
    }

    private void OnDisable()
    {
        _playerMover.OnMoveEvent -= PlayerMover_OnMoveEvent;
        _playerCollector.OnCollectedEvent -= PlayerCollector_OnCollectedEvent;
    }

    private void PlayerMover_OnMoveEvent(object sender, bool isMoving)
    {
        if(!_playerCollector.IsCarrying) // do not want to change if playing carrying clip
        {
            if(isMoving)
            {
                _audioSource.clip = _playerMovingClip;
            }

            else
            {
                _audioSource.clip = _playerIdleClip;
            }

            _audioSource.Play();
        }

    }

    private void PlayerCollector_OnCollectedEvent(object sender, bool collected)
    {
        if(collected)
        {
            _audioSource.clip = _playerCarryingClip;
        }

        else
        {
            if(_playerMover.IsMoving)
            {
                _audioSource.clip = _playerMovingClip;
            }

            else
            {
                _audioSource.clip = _playerIdleClip;
            }
        }

        _audioSource.Play();
    }

}
