using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : Enemy
{

    public float distanceToTarget;
    private Vector3 target;

    void Awake()
    {
        enemyChaseState = new DroneChaseState(this);
        enemyAttackState = new DroneAttackState(this);
    }

    public Vector3 getTarget()
    {
        return target;
    }

    public void setTarget(Vector3 newTarget)
    {
        target = newTarget;
    }


}
