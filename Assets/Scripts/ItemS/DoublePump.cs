using Unity.VisualScripting;
using UnityEngine;

public class DoublePump : Item
{
    public override void ApplyEffect(PlayerStats player)
    {
        base.ApplyEffect(player);
        player.addDamageModifier(2);
    }
}