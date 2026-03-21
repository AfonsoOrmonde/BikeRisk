using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneChaseState: EnemyChaseState
{
    public DroneChaseState(Enemy enemy): base(enemy){}
    public override void Enter()
    {
        Debug.Log("Entered Chase");
    }
    public override void During()
    {
        Vector3 newTarget = enemy.getPlayer().transform.position;
        if(!enemy.InRadius(enemy.getPlayer().gameObject)){
            newTarget.y = enemy.transform.position.y;
            enemy.Move(newTarget);
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
