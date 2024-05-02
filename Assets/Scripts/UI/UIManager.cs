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

    private PlayerStats _playerStats;
    private PlayerMover _playerMover;
    private Collector _collector;


    private void OnEnable()
    {
        _playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        _playerMover = GameObject.FindFirstObjectByType<PlayerMover>();
        _collector = GameObject.FindFirstObjectByType<Collector>();
        _collector.OnTrashCollected += Collector_OnTrashCollected;
        _collector.OnLevelCompleted += Collector_OnLevelCompleted;
    }

    private void OnDisable()
    {
        _collector.OnTrashCollected -= Collector_OnTrashCollected;
        _collector.OnLevelCompleted -= Collector_OnLevelCompleted;
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
        float fillAmount = (_playerMover.CurrentFuel / _playerStats.FuelCapacity);
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
        _collectedTrashText.text = $"{_collector.Count} / {_collector.RequiredCount}";
    }

    private void Collector_OnTrashCollected(object sender, EventArgs e)
    {
        UpdateCollectedTrashText();
    }
    private void Collector_OnLevelCompleted(object sender, EventArgs e)
    {
        _collectedTrashText.enabled = false;
        _collectedTrashImage.enabled = true;
    }
}
