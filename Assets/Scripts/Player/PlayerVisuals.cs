using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private float _carryingSpeed = 750f;
    [SerializeField] private float _movingSpeed = 500f;
    [SerializeField] private float _idleSpeed = 200f;
    [SerializeField] private Transform _rotorTransform;
    [SerializeField] private ParticleSystem _particleSystem;

    PlayerMover _playerMover;
    PlayerCollector _playerCollector;
    // Start is called before the first frame update
    void Start()
    {
        _playerMover = GetComponentInParent<PlayerMover>();
        _playerCollector = GetComponentInParent<PlayerCollector>();
    }

    // Update is called once per frame
    void Update()
    {
        /*        if (_playerMover.IsMoving && _playerCollector.IsCarrying && _particleSystem.isPlaying)
                {

                    _rotorTransform.Rotate(Vector3.up, _carryingSpeed * Time.deltaTime);
                }

                else if (_playerMover.IsMoving && _playerCollector.IsCarrying)
                {
                    _particleSystem.Play();
                    _rotorTransform.Rotate(Vector3.up, _carryingSpeed * Time.deltaTime);
                }

                else if (_playerMover.IsMoving && _particleSystem.isPlaying)
                {
                    _particleSystem.Stop();
                    _rotorTransform.Rotate(Vector3.up, _movingSpeed * Time.deltaTime);
                }

                else if(_playerMover.IsMoving)
                {
                    _rotorTransform.Rotate(Vector3.up, _movingSpeed * Time.deltaTime);
                }

                else
                {
                    _rotorTransform.Rotate(Vector3.up, _idleSpeed * Time.deltaTime);
                }*/

        if (_playerMover.IsMoving)
        {
            if (_playerCollector.IsCarrying)
            {
                _rotorTransform.Rotate(Vector3.up, _carryingSpeed * Time.deltaTime);

                if (!_particleSystem.isPlaying)
                {
                    _particleSystem.Play();
                }
            }
            else
            {
                _rotorTransform.Rotate(Vector3.up, _movingSpeed * Time.deltaTime);

                if (_particleSystem.isPlaying)
                {
                    _particleSystem.Stop();
                }
            }
        }
        else
        {
            _rotorTransform.Rotate(Vector3.up, _idleSpeed * Time.deltaTime);
        }

        //Debug.Log(_particleSystem.isPlaying);
    }
}
