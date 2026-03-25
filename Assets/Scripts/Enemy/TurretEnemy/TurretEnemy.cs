using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{

    void Awake()
    {
        enemyChaseState = new TurretChaseState(this);
        enemyAttackState = new TurretAttackState(this);
    }



}
