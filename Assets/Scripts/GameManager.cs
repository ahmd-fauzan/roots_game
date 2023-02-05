using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(-1)]
public class GameManager : Singleton<GameManager>
{
    [SerializeField] Transform spawnPoints;

    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] TextMeshProUGUI waveText;

    [SerializeField] private float timeBetweenSpawn;

    [SerializeField] GameObject background;
    [SerializeField] GameObject foreGround;

    [SerializeField] private WaveLevel[] waves;

    private WaveLevel currWave;

    private int currWaveIndex;

    private bool gameRun;

    public bool GameState => gameRun;

    private void Awake()
    {
        InitializeResolution();

        gameRun = PlayerPrefs.GetInt("Tutorial") != 0;

        if (gameRun)
            StartGameplay();
    }

    public void StartGameplay()
    {
        gameRun = true;

        currWaveIndex = 0;
        currWave = Instantiate(waves[currWaveIndex]);
        waveText.text = "Wave " + (currWaveIndex + 1);

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (gameRun)
        {
            if(!currWave.GoodEnemyRemains() && !currWave.BedEnemyRemains())
            {
                currWaveIndex++;

                if(currWaveIndex >= waves.Length)
                {
                    Debug.Log("Game Win");
                    gameRun = false;
                    yield return null;
                }

                currWave = Instantiate(waves[currWaveIndex]);

                waveText.text = "Wave " + (currWaveIndex + 1);
            }
            yield return new WaitForSeconds(currWave.TimeBetweenSpawn);

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

        GameObject go = Instantiate(enemyPrefabs[enemyIndex], spawnPoints.GetChild(spawnIndex).position, Quaternion.identity);

        EnemyMovement enemy = go.GetComponent<EnemyMovement>();

        
        if (enemy.resourceType == ResourceType.Good)
        {
            if (currWave.GoodEnemyRemains())
            {
                currWave.AddGoodEnemy();
            }
            else
            {
                Destroy(go);
                Spawn();
            }
        }
        else
        {
            if (currWave.BedEnemyRemains())
            {
                currWave.AddBedEnemy();
            }
            else
            {
                Destroy(go);
                Spawn();
            }
        }
    }
}
