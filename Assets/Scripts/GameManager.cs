using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Transform spawnPoints;

    [SerializeField] GameObject[] enemyPrefabs;

    [SerializeField] private float timeBetweenSpawn;

    [SerializeField] GameObject background;
    [SerializeField] GameObject foreGround;

    private bool gameRun;

    public bool GameState => gameRun;

    private void Awake()
    {
        InitializeResolution();
    }

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

    private void InitializeResolution()
    {
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = Camera.main.aspect * camHalfHeight;

        // Set a new vector to the top left of the scene 
        Vector3 topLeftPosition = new Vector3(-camHalfWidth, camHalfHeight, 0) + Camera.main.transform.position;
        Vector3 topRightPosition = new Vector3(camHalfWidth, camHalfHeight, 0) + Camera.main.transform.position;
        Vector3 bottomLeftPosition = new Vector3(-camHalfWidth, -camHalfHeight, 0) + Camera.main.transform.position;

        float horizontal = Vector2.Distance(topLeftPosition, topRightPosition);
        float vertical = Vector2.Distance(topLeftPosition, bottomLeftPosition);

        background.transform.localScale = new Vector3(horizontal, vertical, 1);
        foreGround.transform.localScale = new Vector3(horizontal, vertical, 1);
    }
    private void Spawn()
    {
        int spawnIndex = Random.Range(0, spawnPoints.childCount);
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[enemyIndex], spawnPoints.GetChild(spawnIndex).position, Quaternion.identity);
    }
}
