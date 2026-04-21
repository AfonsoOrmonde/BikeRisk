using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSensor : MonoBehaviour
{
    [SerializeField] GameObject rightSensors;
    [SerializeField] GameObject leftSensors;
    [SerializeField] GameObject downSensors;
    [SerializeField] PlayerController player;
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }
    void Update()
    {
        if(!player.getDeathStatus())
            if(player.transform.position.z > leftSensors.transform.position.z 
                || player.transform.position.y < downSensors.transform.position.y
                || player.transform.position.z < rightSensors.transform.position.z)
            {
                player.PlayerDied();
            }
    }

}
