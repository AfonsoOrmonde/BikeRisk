using Unity.VisualScripting;
using UnityEngine;

public class DoublePump : Item
{
    public override void ApplyEffect(PlayerStats player)
    {
        base.ApplyEffect(player);
        Debug.Log("Applying item: Double Barrel");
        player.addDamageModifier(2);
    }
}