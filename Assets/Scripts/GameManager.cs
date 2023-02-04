using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoints;

    [SerializeField] GameObject[] enemyPrefabs;

    [SerializeField] private float timeBetweenSpawn;

    private bool gameRun;

    // Start is called before the first frame update
    void Start()
    {
        gameRun = true;

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (gameRun)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);

            Spawn();
        }
    }

    private void Spawn()
    {
        int spawnIndex = Random.Range(0, spawnPoints.childCount);
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[enemyIndex], spawnPoints.GetChild(spawnIndex).position, Quaternion.identity);
    }
}
