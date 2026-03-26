using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideableWall : MonoBehaviour
{

    public enum WallType
    {
        Top,Left,Right,Bot
    }
    public WallType type;
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            player.DetectedNewFloor(transform.parent.gameObject,type);
        }
    }
}
