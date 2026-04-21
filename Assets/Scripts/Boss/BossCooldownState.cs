using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCooldownState: BossState
{

    public BossCooldownState(Boss boss) : base(boss)
    {
    }
    public override void Enter()
    {
        boss.StartCooldown();
        boss.setVelocityBoss(new Vector3(boss.getBossSpeed(),0,0));
    }
    public override void During()
    {
        if(boss.getCanAttack())
            boss.ChangeToAttack();
    }
    public override void Leave()
    {
        
    }
}
