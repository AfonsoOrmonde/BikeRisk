using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    protected Boss boss;
    public BossState(Boss boss)
    {
        this.boss = boss;
    }
    public virtual void Enter() { }
    public virtual void During() { }
    public virtual void Leave() { }
}
