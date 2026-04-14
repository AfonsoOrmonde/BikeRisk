using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class PlayerStats: MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float speed;
    [SerializeField] float speedTurning;
    [SerializeField] float acceleration;
    [SerializeField] float slowDown;
    [SerializeField] float damage;
    [SerializeField] float damageModifier = 1;
    [SerializeField] float radiusShooting;
    [SerializeField] float currentExperience;
    [SerializeField] float currentLevel;
    [SerializeField] float crashingDamageToReceive;
    [SerializeField] float maxDashEnergy;
    [SerializeField] private float timeStopCooldown;
    [SerializeField] private float timeStopDuration;
    [SerializeField] private float cooldownShooting;

    [Header("Debug")]
    [SerializeField]float dashEnergy;
    [SerializeField]private float health;
    [SerializeField]private float energyRechargeRate = 5;
    [SerializeField]private float energySpendRate = 10;
    [SerializeField]private float healingRate = 0;
    [SerializeField]private float lifeStealModifier = 0;
    [SerializeField]private float blockModifier = 0;
    [SerializeField]private bool chargingDash = false;
    [SerializeField]private bool canTimeSlow = false;
    [SerializeField]private float currentTimeStopCooldown;

    public UnityEvent HealthRegained;
    public UnityEvent HealthIncreased;

    [SerializeField] ItemSelectorMenu selectorMenu;
    PlayerController controller;
    private List<ItemData> equippedItems = new List<ItemData>();


    void Start()
    {
        selectorMenu = FindAnyObjectByType<ItemSelectorMenu>();
        controller = FindAnyObjectByType<PlayerController>();
        dashEnergy = maxDashEnergy;
        health = maxHealth;
        currentTimeStopCooldown = timeStopCooldown;
    }

    void Update()
    {
        if (chargingDash)
            dashEnergy = Mathf.Max(0, dashEnergy - energySpendRate * Time.deltaTime); // the max is just to clamp the float vlaue
        else
            dashEnergy = Mathf.Min(maxDashEnergy, dashEnergy + energyRechargeRate * Time.deltaTime); // same thing for the min
    }

    public void LevelUP()
    {
        currentLevel++;
        Debug.Log($"Level up to Level = {currentLevel}");
        AudioManager.Instance.playSFX("LevelUp");
        selectorMenu.OpenItemSelection(this);
    }

    public void gainExperience(float experience)
    {
        AudioManager.Instance.playSFX("Ring");
        currentExperience += experience;
        if(currentExperience >= currentLevel*10) // temporary calculation of needed experience per level
            LevelUP();
    }

    public void Heal(float value)
    {
        health += value;
        health = Math.Min(health, maxHealth);
        HealthRegained.Invoke();
    }

    public void HealByRate()
    {
        Heal(health*healingRate);
    }
    public void HealByLifeSteal()
    {
        Heal(damage * lifeStealModifier);
    }
    public void EquipItem(ItemData item)
    {
        GameObject newItem = Instantiate(item.itemBehaviour, this.transform);
        newItem.transform.localPosition = Vector3.zero; 
        if(newItem.TryGetComponent(out Item itemToEquip)){
            Debug.Log($"Item to equip: {item.itemName}");
            itemToEquip.ApplyEffect(this);
            equippedItems.Add(item);
        }
    }
    public void addDamageModifier(float value)
    {
        damageModifier *= value;
    }

    public void addHealingRate(float value)
    {
        healingRate += value;
    }

    public void addBlockModifier(float value)
    {
        blockModifier += value;
    }

    public void reduceGasSpendingRate(float percentage)
    {
        energySpendRate -= energySpendRate*percentage;
    }

    public void addLifeStealModifier(float value)
    {
        lifeStealModifier += value;
    }

    public void addMaxHealth(float value)
    {
        maxHealth += value;
        HealthIncreased.Invoke();
    }

    public void ReduceTimeSlowCooldown(float value)
    {
        if(!canTimeSlow){
            setClockSlowDown(true);
        }
        else
        {
            timeStopCooldown -= value;
        }
    }

    public void CheckTimeStop()
    {
        if(currentTimeStopCooldown <= 0)
        {
            StartCoroutine(GameManager.Instance.SlowDownTime(0.5f, timeStopDuration));
            currentTimeStopCooldown = timeStopCooldown;
        }
        else
            currentTimeStopCooldown -= 1;
    }

    public void setClockSlowDown(bool value)
    {
        canTimeSlow = value;
    }

    public bool getCanSlowDonw()
    {
        return canTimeSlow;
    }

    public void setChargeDash(bool value)
    {
        chargingDash = value;
    }

    public void TakeDamage(float damageToTake)
    {
        if(!RandomizerManager.RandomActivation(blockModifier)){
            health -= damageToTake;
            if(health <= 0)
            {
                Debug.Log("PLayer has died");
                controller.PlayerDied();
            }
        }
    }

    public bool getCharginDash()
    {
        return chargingDash;
    }

    public float getCrashingDamage()
    {
        return crashingDamageToReceive;
    }

    public float getHealth()
    {
        return health;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public float getMaxDash()
    {
        return maxDashEnergy;
    }

    public float getDash()
    {
        return dashEnergy;
    }

    public float getSpeedBike()
    {
        return speed;
    }
    public float getSpeedTurning()
    {
        return speedTurning;
    }
    public float getAcceleration()
    {
        return acceleration;
    }
    public float getSlowDown()
    {
        return slowDown;
    }
    public float getDamage()
    {
        return damage*damageModifier;
    }
    public float getRadiusShooting()
    {
        return radiusShooting;
    }

    public float getShootCooldown()
    {
        return cooldownShooting;
    }
}