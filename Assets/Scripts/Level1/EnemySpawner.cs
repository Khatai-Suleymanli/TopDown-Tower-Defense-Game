using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform spawnPoint;

    // edit later. make it according to the selected difficulty level
    public float spawnInterval = 7f;

    private float timer;

    public Transform[] waypoints; //array of waypoints(enemies' path) the same as the one in enemy movement

    // Start is called before the first frame update
    void Start()
    {
        // to initilize the time after spwaninterval
        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
       timer -= Time.deltaTime;

        if(timer <= 0)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        EnemyMovement enemyMovement = enemyPrefab.GetComponent<EnemyMovement>();
        if (enemyMovement != null) { 
            enemyMovement.waypoints = waypoints;
        }
        else
        {
            Debug.Log("EnemyMovement problematic");
        }
    }
}
