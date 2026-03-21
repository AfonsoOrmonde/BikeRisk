using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : Enemy
{

    void Awake()
    {
        enemyChaseState = new DroneChaseState(this);
        enemyAttackState = new DroneAttackState(this);
    }



}
