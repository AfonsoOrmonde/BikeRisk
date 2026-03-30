using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapBot : Item
{
    public override void ApplyEffect(PlayerStats player)
    {
        base.ApplyEffect(player);
        player.addLifeStealModifier(0.01f);
    }
}
