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
        if (enemy.CanAttack())
        {
            enemy.Attack(enemy.getPlayer().gameObject);
        }        
        enemy.ChangeToChase();

    }
    public override void During()
    {

    }
    public override void Leave()
    {
        
    }
}
