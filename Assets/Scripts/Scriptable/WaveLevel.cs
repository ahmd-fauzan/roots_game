using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Wave Level")]
public class WaveLevel : ScriptableObject
{
    [SerializeField] private int waveLevel;
    [SerializeField] private int bedEnemyCount;
    [SerializeField] private int goodEnemyCount;
    [SerializeField] private float timeBetweenSpawn;

    public int Level => waveLevel;
    public int BedEnemyCount => bedEnemyCount;
    public int GoodEnemyCount => goodEnemyCount;
    public float TimeBetweenSpawn => timeBetweenSpawn;

    private int currBedEnemy;
    private int currGoodEnemy;

    public void Initialize()
    {
        currBedEnemy = 0;
        currGoodEnemy = 0;
    }

    public void AddGoodEnemy()
    {
        currGoodEnemy++;

        Debug.Log("Good Enemy : " + currGoodEnemy + " => " + GoodEnemyCount);
    }

    public void AddBedEnemy()
    {
        currBedEnemy++;

        Debug.Log("Bed Enemy : " + currBedEnemy + " => " + BedEnemyCount);
    }

    public bool BedEnemyRemains()
    {
        return currBedEnemy < BedEnemyCount;
    }

    public bool GoodEnemyRemains()
    {
        return currGoodEnemy < GoodEnemyCount;
    }
}
