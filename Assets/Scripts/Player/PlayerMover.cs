using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    public bool IsMoving { get; private set; }
    public float CurrentSpeed { get; private set; }
    public float CurrentFuel {  get; private set; }
    public float FuelBurnRate { get; private set; }

    [Tooltip("Movement & Rotation Variables")]
    [SerializeField] private Vector3 _startingPosition;
    [SerializeField] private float _rotationSpeed = 50f;
    [SerializeField] private float _distanceToTargetThreshold = 0.15f;
    [SerializeField] private float _xMaxPosition = 24;
    [SerializeField] private float _zMaxPosition = 11;


    private Vector3 _targetPosition;
    private Vector3 _moveDirection;

    private PlayerCollector _playerCollector;
    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        _playerCollector = GetComponent<PlayerCollector>();
        _playerCollector.OnFuelPickedUp += PlayerCollecter_OnFuelPickedUp;
    }

    private void Start()
    {
        CurrentSpeed = _playerStats.BaseSpeed;
        CurrentFuel = _playerStats.FuelCapacity;
        FuelBurnRate = _playerStats.FuelBurnRate;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        CalculateSpeed();
        Move();
        CalculateFuel();
        CheckIsMoving();
    }

    private void CalculateSpeed()
    {
        if (!_playerCollector.IsCarrying)
        {
            CurrentSpeed = _playerStats.BaseSpeed;
        }

        else
        {
            CurrentSpeed = _playerStats.CarrySpeed;
        }
    }


    private void Move()
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
            float xPos = Mathf.Clamp(hitPoint.x, -_xMaxPosition, _xMaxPosition);
            float zPos = Mathf.Clamp(hitPoint.z, -_zMaxPosition, _zMaxPosition);
            _targetPosition = new Vector3(xPos, transform.position.y, zPos);
        }
        //moves player toward the mouse world position
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, CurrentSpeed * Time.deltaTime);
    }
    private void Rotate()
    {
        //set the move direction(used in Rotate method)
        _moveDirection = _targetPosition - transform.position;

        if (_moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void CheckIsMoving()
    {
        float distanceToTarget = Vector3.Distance(transform.position, _targetPosition);
        IsMoving = distanceToTarget > _distanceToTargetThreshold;
    }

    private void CalculateFuel()
    {
        if(_playerCollector.IsCarrying) { FuelBurnRate = _playerStats.CarryFuelBurnRate; }
        else { FuelBurnRate = _playerStats.FuelBurnRate; }

        CurrentFuel -= FuelBurnRate * Time.deltaTime;

        if (CurrentFuel <= 0) 
        {
            Destroy(gameObject);
        }
    }

    private void PlayerCollecter_OnFuelPickedUp(object sender, float e)
    {
        CurrentFuel += e;

        if (CurrentFuel > _playerStats.FuelCapacity)
        {
            CurrentFuel = _playerStats.FuelCapacity;
        }
    }
}
