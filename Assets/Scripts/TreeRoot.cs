using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRoot : MonoBehaviour
{
    [Header("Tree Stat")]
    [SerializeField] private int treeHealth;
    [SerializeField] private int resourceCount;
    [SerializeField] private float timeBetweenSpawnAmmo;

    [SerializeField] private GameObject ammoPrefab;

    private float distance;

    GameManager gameManager;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(transform.position, player.transform.position);

        gameManager = GameManager.Instance;

        StartCoroutine(SpawnAmmoEnumerator());
    }

    IEnumerator SpawnAmmoEnumerator()
    {
        while (gameManager.GameState)
        {
            yield return new WaitForSeconds(timeBetweenSpawnAmmo);

            SpawnAmmo();
        }
    }

    private void SpawnAmmo()
    {

        Vector3 spawnPos = RandomPointOnXZCircle(transform.position, distance);

        /*
        if (Vector3.Distance(transform.position, spawnPos + transform.position) <= distance)
        {
            SpawnAmmo();
            return;
        }*/

        Instantiate(ammoPrefab, transform.position + spawnPos, Quaternion.identity);
    }

    Vector3 RandomPointOnXZCircle(Vector3 center, float radius)
    {
        float angle = Random.Range(0, 2f * Mathf.PI);
        return center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
    }

    public void TakeResource(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Good:
                resourceCount++;
                break;
            case ResourceType.Bad:
                treeHealth -= 10;

                if (treeHealth <= 0)
                    Debug.Log("Game Over");

                break;
        }
    }
}

public enum ResourceType
{
    Good, Bad
}
