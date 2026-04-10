using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneChaseState: EnemyChaseState
{

    Vector3 target;
    new DroneEnemy enemy;
    public DroneChaseState(DroneEnemy enemy): base(enemy)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        enemy.setSpeed(enemy.getPlayer().getSpeedBike() + 10);
        target = Random.onUnitSphere;
    }
    public override void During()
    {
        Vector3 targetPoint = enemy.getPlayer().transform.position + (target* enemy.distanceToTarget);
        if(!enemy.InRadius(enemy.getPlayer().gameObject)){
            targetPoint.y = enemy.transform.position.y;
            enemy.Move(targetPoint);
        }
        else
        {
            enemy.ChangeToAttack();
        }
    }
    public override void Leave()
    {
        
    }
}
