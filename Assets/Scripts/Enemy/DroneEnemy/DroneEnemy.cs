using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : Enemy
{

    public float distanceToTarget;

    void Awake()
    {
        enemyChaseState = new DroneChaseState(this);
        enemyAttackState = new DroneAttackState(this);
    }



}
