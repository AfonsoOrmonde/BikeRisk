using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState: BossState
{

    bool left;

    public BossAttackState(Boss boss) : base(boss)
    {
    }
    public override void Enter()
    {
        if(left)
            boss.Attack(boss.pointOfAttackLeft);
        else
            boss.Attack(boss.pointOfAttackRight);
        left = !left;
        boss.ChangeToCooldown();
    }
    public override void During()
    {
        
    }
    public override void Leave()
    {
        
    }
}
