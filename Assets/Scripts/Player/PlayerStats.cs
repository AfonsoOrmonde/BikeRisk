using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PlayerStats: MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float speed;
    [SerializeField] float speedTurning;
    [SerializeField] float acceleration;
    [SerializeField] float slowDown;
    [SerializeField] float damage;
    [SerializeField] float damageModifier = 1;
    [SerializeField] float radiusShooting;
    [SerializeField] float currentExperience;
    [SerializeField] float currentLevel;

    

    [SerializeField] ItemSelectorMenu selectorMenu;

    private List<ItemData> equippedItems = new List<ItemData>();


    void Start()
    {
        selectorMenu = FindAnyObjectByType<ItemSelectorMenu>();
    }

    public void LevelUP()
    {
        currentLevel++;
        Debug.Log($"Level up to Level = {currentLevel}");
        selectorMenu.OpenItemSelection(this);
    }

    public void gainExperience(float experience)
    {
        currentExperience += experience;
        if(currentExperience >= currentLevel*10) // temporary calculation of needed experience per level
            LevelUP();
    }

    public void EquipItem(ItemData item)
    {
        Debug.Log("Entered in equip Item");
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

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;
        if(health <= 0)
        {
            Debug.Log("PLayer has died");
        }
    }

    public float getHealth()
    {
        return health;
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
}