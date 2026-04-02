using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumGas : Item
{
    public override void ApplyEffect(PlayerStats player)
    {
        base.ApplyEffect(player);
        player.reduceGasSpendingRate(0.05f);
    }
}
