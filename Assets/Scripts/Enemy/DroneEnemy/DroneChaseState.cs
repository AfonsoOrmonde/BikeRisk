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
        Vector2 randomCircle = Random.insideUnitCircle.normalized;
        target = new Vector3(randomCircle.x, 0, randomCircle.y);
        enemy.setTarget(target);
    }
    public override void During()
    {
        
        Vector3 targetPoint = enemy.getPlayer().transform.position + (enemy.getTarget()* enemy.distanceToTarget);
        targetPoint.y = enemy.transform.position.y;
        if(Vector3.Distance(enemy.transform.position, targetPoint)>0.5f){
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
