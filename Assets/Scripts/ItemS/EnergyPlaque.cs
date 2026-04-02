using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPlaque : Item
{
    public override void ApplyEffect(PlayerStats player)
    {
        base.ApplyEffect(player);
        player.addBlockModifier(0.01f);
    }
}
