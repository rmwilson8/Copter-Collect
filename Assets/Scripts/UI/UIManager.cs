using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _fuelBar;
    [SerializeField] private TextMeshProUGUI _collectedTrashText;
    [SerializeField] private Image _collectedTrashImage;
    [SerializeField] private GameObject _controlsMenu;

    private PlayerStateMachine _playerStateMachine;
    private CollectorManager _collectorManager;
    private InputReader _inputReader;

    private void OnEnable()
    {
        _inputReader = GameObject.FindAnyObjectByType<InputReader>();
        _inputReader.OnToggleControlsEvent += InputReader_OnToggleControlsEvent;
        _playerStateMachine = GameObject.FindFirstObjectByType<PlayerStateMachine>();
        _collectorManager = GameObject.FindFirstObjectByType<CollectorManager>();
        Collector.OnAnyTrashCollected += Collector_OnTrashCollected;
        _collectorManager.OnLevelCompleted += CollectorManager_OnLevelCompleted;
    }

    private void OnDisable()
    {
        Collector.OnAnyTrashCollected -= Collector_OnTrashCollected;
        _collectorManager.OnLevelCompleted -= CollectorManager_OnLevelCompleted;
        _inputReader.OnToggleControlsEvent -= InputReader_OnToggleControlsEvent;
    }

    void Start()
    {
        _collectedTrashImage.enabled = false;
        _controlsMenu.SetActive(false);
        UpdateCollectedTrashText();
    }


    void Update()
    {
        UpdateFuelBar();
    }

    private void UpdateFuelBar()
    {
        float fillAmount = (_playerStateMachine.CurrentFuel / _playerStateMachine.PlayerStats.FuelCapacity);
        _fuelBar.value = fillAmount;

        Image fillImage = _fuelBar.fillRect.GetComponent<Image>();

        if (fillAmount <= 0.25f)
        {
            fillImage.color = Color.red;
        }

        else if (fillAmount <= 0.5f)
        {
            fillImage.color = Color.yellow;
        }

        else
        {
           fillImage.color = Color.green;
        }

    }

    private void UpdateCollectedTrashText()
    {
        if(!GameManager.Instance.GetIsEndlessLevel())
        {
            _collectedTrashText.text = $"{_collectorManager.Count} / {_collectorManager.RequiredCount}";
        }

        else
        {
            _collectedTrashText.text = $"{_collectorManager.Count}";
        }
    }

    private void Collector_OnTrashCollected(object sender, EventArgs e)
    {
        Invoke("UpdateCollectedTrashText", .25f);
    }
    private void CollectorManager_OnLevelCompleted(object sender, EventArgs e)
    {
        _collectedTrashText.enabled = false;
        _collectedTrashImage.enabled = true;
    }

    private void InputReader_OnToggleControlsEvent(object sender, bool checkControls)
    {
        if(checkControls)
        {
            _controlsMenu.SetActive(true);
        }

        else
        {
            _controlsMenu.SetActive(false);
        }
    }
}
