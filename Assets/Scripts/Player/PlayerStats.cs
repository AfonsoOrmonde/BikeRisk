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
    [SerializeField] float radiusShooting;
    [SerializeField] float currentExperience;
    [SerializeField] float currentLevel;


    public void LevelUP()
    {
        currentLevel++;
    }

    public void gainExperience(float experience)
    {
        currentExperience += experience;
        if(currentExperience >= currentLevel*10) // temporary calculation of needed experience per level
            LevelUP();
    }

    public float getHealth()
    {
        return health;
    }
    public float getSpeedBike()
    {
        return health;
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
        return health;
    }
    public float getRadiusShooting()
    {
        return radiusShooting;
    }
}