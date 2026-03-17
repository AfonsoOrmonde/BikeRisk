using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCircle : MonoBehaviour
{
    [SerializeField] int experience;
    public void OnTriggerEnter(Collider other)
    {
            if(other.TryGetComponent(out PlayerStats player))
            {
                player.gainExperience(experience);
            }
    }
}
