using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingNanoBots : Item
{
    public override void ApplyEffect(PlayerStats player)
    {
        base.ApplyEffect(player);
        player.addHealingRate(0.01f);
        //Debug.Log("Applying item: Double Barrel");
        //player.addDamageModifier(2);
    }
}
