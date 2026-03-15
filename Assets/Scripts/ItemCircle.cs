using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCircle : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("GotItem");
        }
    }
}
