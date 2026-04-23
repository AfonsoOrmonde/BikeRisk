using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningEndPoint : MonoBehaviour
{
    PlayerController player;
    UIManager uIManager;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        uIManager = FindAnyObjectByType<UIManager>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
            if(player.transform.position.x > this.transform.position.x)
            {
                GameState.Instance.unlockLevel(levelManager.LEVEL_NUMBER+1);
                uIManager.OpenWinnigMenu();
            }
    }
}
