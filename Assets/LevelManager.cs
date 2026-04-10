using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager: MonoBehaviour
{
    [SerializeField] float cooldownDroneSpawn;
    [SerializeField] int numberOfDrones;
    [SerializeField]int distanceToSpawnAway;
    private PlayerStats player;

    public GameObject drone;

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        StartCoroutine(SpawnEnemiesRoutinely());
    }

    public IEnumerator SpawnEnemiesRoutinely()
    {
        yield return new WaitForSeconds(cooldownDroneSpawn);
        GameObject obj = Instantiate(drone);
        obj.transform.position = new Vector3(player.transform.position.x - distanceToSpawnAway, player.transform.position.y+8,player.transform.position.z);
        
    }
}