using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretChaseState: EnemyChaseState
{
    public TurretChaseState(Enemy enemy): base(enemy){}
    public override void Enter()
    {
        Debug.Log("Entered Chase");
    }
    public override void During()
    {
        if(!enemy.InRadius(enemy.getPlayer().gameObject)){
            enemy.RotateEnemy(enemy.getPlayer().gameObject);
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
