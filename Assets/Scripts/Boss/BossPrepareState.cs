using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BossPrepareState: BossState
{
    Vector3 target;
    public BossPrepareState(Boss boss) : base(boss)
    {
        target = boss.transform.position;
    }
    public override void Enter()
    {
        target.y = 5+boss.transform.localScale.y;
        Debug.Log($"Target of Boss = {target}");
    }
    public override void During()
    {
        float step =  boss.getBossSpeed()* 3 * Time.deltaTime; 
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, target, step);

        if (Vector3.Distance(boss.transform.position, target) < 0.05f)
        {
            Debug.Log("Changing to cooldown pahse");
            boss.ChangeToCooldown();
        } 
    }
    public override void Leave()
    {
        
    }
}
