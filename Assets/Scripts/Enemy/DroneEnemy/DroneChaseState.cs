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
        
    }
    public override void Leave()
    {
        
    }
}
