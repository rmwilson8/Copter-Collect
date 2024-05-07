using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [field: SerializeField] public float BaseSpeed {  get; private set; }

    [field: SerializeField] public float CarrySpeed { get; private set; }
    [field: SerializeField] public float FuelCapacity { get; private set; }
    [field: SerializeField] public float FuelBurnRate { get; private set; }

    [field: SerializeField] public float CarryFuelBurnFactor { get; private set; }
    [field: SerializeField] public float CarryIdleFuelBurnFactor { get; private set; }
    [field: SerializeField] public float BoostFuelBurnFactor { get; private set; }
    [field: SerializeField] public float BoostSpeedFactor { get; private set; }

    [SerializeField] private float _baseSpeedUpgradeFactor = 1.2f;
    [SerializeField] private float _carrySpeedUpgradeFactor = 1.2f;
    [SerializeField] private float _fuelCapacityUpgradeFactor = 1.2f;
    [SerializeField] private float _fuelEfficiencyUpgradeFactor = 1.1f;

    public float PreviousBaseSpeed { get; private set; }
    public float PreviousCarrySpeed { get; private set; }
    public float PreviousFuelCapacity { get; private set; }
    public float PreviousFuelBurnRate { get; private set; }
    public float PreviousCarryFuelBurnRate { get; private set; }


    public Upgrade RecentUpgrade {  get; private set; } // saves reference to most recent upgrade choice


    public void UpgradeBaseSpeed()
    {
        PreviousBaseSpeed = BaseSpeed;
        BaseSpeed *= _baseSpeedUpgradeFactor;
        RecentUpgrade = Upgrade.BaseSpeed;
    }

    public void UpgradeCarrySpeed() 
    {
        PreviousCarrySpeed =CarrySpeed;
        CarrySpeed *= _carrySpeedUpgradeFactor;
        RecentUpgrade = Upgrade.CarrySpeed;
    }

    public void UpgradeFuelCapacity()
    {
        PreviousFuelCapacity = FuelCapacity;
        FuelCapacity *= _fuelCapacityUpgradeFactor;
        RecentUpgrade = Upgrade.FuelCapacity;
    }

    public void UpgradeFuelEfficiency()
    {
        PreviousFuelBurnRate = FuelBurnRate;
        FuelBurnRate /= _fuelEfficiencyUpgradeFactor; // upgrading all 3 fuel burn rates
        CarryFuelBurnFactor /= _fuelCapacityUpgradeFactor;
        CarryIdleFuelBurnFactor /= _fuelCapacityUpgradeFactor;
        FuelBurnRate = Mathf.Round(FuelBurnRate * 10) / 10; // rounds FuelBurnRate to 2 decimal places
        RecentUpgrade = Upgrade.FuelEfficiency;
    }
}

public enum Upgrade
{
    BaseSpeed,
    CarrySpeed,
    FuelCapacity,
    FuelEfficiency
}
