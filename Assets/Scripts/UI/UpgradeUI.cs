using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [Tooltip("UI Groups")]
    [SerializeField] GameObject _upgradeUIGameObject;
    [SerializeField] GameObject _sceneTransitionUIGameObject;

    [Tooltip("Upgrade Buttons")]
    [SerializeField] private Button _fuelCapacityUpgradeButton;
    [SerializeField] private Button _fuelEfficiencyUpgradeButton;
    [SerializeField] private Button _baseSpeedUpgradeButton;
    [SerializeField] private Button _carrySpeedUpgradeButton;

    [Tooltip("Scene Transition variables")]
    [SerializeField] private float _sceneTransitionDuration = 3f;
    [SerializeField] private TextMeshProUGUI _upgradeText;
    [SerializeField] private TextMeshProUGUI _factText;

    private PlayerStats _playerStats;

    private void Awake()
    {
        _sceneTransitionUIGameObject.SetActive(false);
    }
    void Start()
    {
        _playerStats = GameObject.FindFirstObjectByType<PlayerStats>();

        _fuelCapacityUpgradeButton.onClick.AddListener(_playerStats.UpgradeFuelCapacity);
        _fuelCapacityUpgradeButton.onClick.AddListener(HandleAnyButtonClicked);

        _fuelEfficiencyUpgradeButton.onClick.AddListener(_playerStats.UpgradeFuelEfficiency);
        _fuelEfficiencyUpgradeButton.onClick.AddListener(HandleAnyButtonClicked);

        _baseSpeedUpgradeButton.onClick.AddListener(_playerStats.UpgradeBaseSpeed);
        _baseSpeedUpgradeButton.onClick.AddListener(HandleAnyButtonClicked);

        _carrySpeedUpgradeButton.onClick.AddListener(_playerStats.UpgradeCarrySpeed);
        _carrySpeedUpgradeButton.onClick.AddListener(HandleAnyButtonClicked);
    }

/*    private void OnDisable()
    {
        _fuelCapacityUpgradeButton.onClick.RemoveListener(_playerStats.UpgradeFuelCapacity);
        _fuelCapacityUpgradeButton.onClick.RemoveListener(HandleAnyButtonClicked);

        _fuelEfficiencyUpgradeButton.onClick.RemoveListener(_playerStats.UpgradeFuelEfficiency);
        _fuelEfficiencyUpgradeButton.onClick.RemoveListener(HandleAnyButtonClicked);

        _baseSpeedUpgradeButton.onClick.RemoveListener(_playerStats.UpgradeBaseSpeed);
        _baseSpeedUpgradeButton.onClick.RemoveListener(HandleAnyButtonClicked);

        _carrySpeedUpgradeButton.onClick.RemoveListener(_playerStats.UpgradeCarrySpeed);
        _carrySpeedUpgradeButton.onClick.RemoveListener(HandleAnyButtonClicked);
    }*/

    public void HandleAnyButtonClicked() // set in unity editor
    {
        StartCoroutine(TransitionToNextSceneRoutine());
    }

    private IEnumerator TransitionToNextSceneRoutine()
    {
        _upgradeUIGameObject.SetActive(false);
        _sceneTransitionUIGameObject.SetActive(true);
        
        switch(_playerStats.RecentUpgrade)
        {
            case Upgrade.BaseSpeed:
                _upgradeText.text = $"Base Speed: {_playerStats.PreviousBaseSpeed} -> {_playerStats.BaseSpeed}";
                break;
            case Upgrade.CarrySpeed:
                _upgradeText.text = $"Carry Speed: {_playerStats.PreviousCarrySpeed} -> {_playerStats.CarrySpeed}";
                break;
            case Upgrade.FuelCapacity:
                _upgradeText.text = $"Fuel Capacity: {_playerStats.PreviousFuelCapacity} -> {_playerStats.FuelCapacity}";
                break;
            case Upgrade.FuelEfficiency:
                _upgradeText.text = $"Fuel Burn Rate: {_playerStats.PreviousFuelBurnRate} -> {_playerStats.FuelBurnRate} / second";
                break;
        }

        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                _factText.text = GameConstants.UPGRADE_FACT_ONE;
                break;
            case 3:
                _factText.text = GameConstants.UPGRADE_FACT_TWO;
                break;
            case 5:
                _factText.text = GameConstants.UPGRADE_FACT_THREE;
                break;
            case 7:
                _factText.text = GameConstants.END_GAME_FACT;
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(_sceneTransitionDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
