using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmateSpawner : MonoBehaviour
{
    [SerializeField] GameObject inmate;
    [SerializeField] int maxEnemys = 20;
    List<GameObject> enemys;
    [SerializeField] float spawnTimer = 3f;
    float nextSpawnTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemys = new List<GameObject>();
        for(int i = 0; i < maxEnemys; i++)
        {
            GameObject enemy = Instantiate(inmate);
            enemys.Add(enemy);
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time > nextSpawnTime)
        {
            if (SpawnEnemy(this.transform.position))
            {
                nextSpawnTime = Time.time + spawnTimer;
            }
            else
            {
                nextSpawnTime = Time.time + .5f;
            }
        }
    }

    public bool SpawnEnemy(Vector3 location)
    {
        bool canSpawn = false;
        GameObject enemyToSpawn = null;
        foreach(GameObject enemy in enemys)
        {
            if (!enemy.activeInHierarchy)
            {
                enemyToSpawn = enemy;
                canSpawn = true;
            }
        }
        if (canSpawn)
        {
            enemyToSpawn.transform.position = location;
            enemyToSpawn.GetComponent<InmateController>().Spawn();
        }

        return canSpawn;
    }
}
