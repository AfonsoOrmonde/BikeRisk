using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPoint : MonoBehaviour
{
   PlayerController player;
   Boss boss;

   bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        boss = FindAnyObjectByType<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
            if(player.transform.position.x > this.transform.position.x)
            {
                boss.Activate();
                gameObject.SetActive(false);
            }
    }
}
