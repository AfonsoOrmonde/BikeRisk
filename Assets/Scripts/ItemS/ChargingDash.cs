using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingDash : Item
{
    public override void ApplyEffect(PlayerStats player)
    {
        base.ApplyEffect(player);
        player.setChargeDash(true);
    }
}
