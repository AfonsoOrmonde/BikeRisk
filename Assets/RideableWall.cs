using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideableWall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            player.DetectedNewFloor(transform.parent);
        }
    }
}
