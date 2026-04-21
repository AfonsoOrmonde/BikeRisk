using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningEndPoint : MonoBehaviour
{
    PlayerController player;
    UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        uIManager = FindAnyObjectByType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
            if(player.transform.position.x > this.transform.position.x)
            {
                uIManager.OpenWinnigMenu();
            }
    }
}
