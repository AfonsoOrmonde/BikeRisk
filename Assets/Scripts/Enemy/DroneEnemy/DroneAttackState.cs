using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttackState: EnemyAttackState
{

    public DroneAttackState(Enemy enemy) : base(enemy)
    {
    }
    public override void Enter()
    {
        Debug.Log("Entered Attack");

    }
    public override void During()
    {
        if (!enemy.InRadius(enemy.getPlayer().gameObject))
        {
            enemy.ChangeToChase();
        }
        else if (enemy.CanAttack())
        {
            enemy.Attack(enemy.getPlayer().gameObject);
        }
    }
    public override void Leave()
    {
        
    }
}
