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

    private PlayerStateMachine _playerStateMachine;
    private CollectorManager _collectorManager;


    private void OnEnable()
    {
        _playerStateMachine = GameObject.FindFirstObjectByType<PlayerStateMachine>();
        _collectorManager = GameObject.FindFirstObjectByType<CollectorManager>();
        Collector.OnAnyTrashCollected += Collector_OnTrashCollected;
        _collectorManager.OnLevelCompleted += CollectorManager_OnLevelCompleted;
    }

    private void OnDisable()
    {
        Collector.OnAnyTrashCollected -= Collector_OnTrashCollected;
        _collectorManager.OnLevelCompleted -= CollectorManager_OnLevelCompleted;
    }

    void Start()
    {
        _collectedTrashImage.enabled = false;
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
        _collectedTrashText.text = $"{_collectorManager.Count} / {_collectorManager.RequiredCount}";
    }

    private void Collector_OnTrashCollected(object sender, EventArgs e)
    {
        UpdateCollectedTrashText();
    }
    private void CollectorManager_OnLevelCompleted(object sender, EventArgs e)
    {
        _collectedTrashText.enabled = false;
        _collectedTrashImage.enabled = true;
    }
}
