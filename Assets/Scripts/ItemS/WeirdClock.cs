using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdClock : Item
{
    public override void ApplyEffect(PlayerStats player)
    {
        base.ApplyEffect(player);
        player.ReduceTimeSlowCooldown(5);
    }
}
