using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreeRoot : MonoBehaviour
{
    [SerializeField] private TreeLevel[] treeLevels;

    [SerializeField] private GameObject ammoPrefab;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider energySlider;
    [SerializeField] private Image expImage;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] float attackRange;

    private float distance;

    private TreeLevel currLevel;

    private int currLevelIndex;

    GameManager gameManager;

    Coroutine attackCoroutine;

    private void Start()
    {
        currLevelIndex = 0;

        currLevel = Instantiate(treeLevels[currLevelIndex]);

        currLevel.Initialize();

        currLevel.OnLevelUpgrade += Upgrade;
        currLevel.OnTreeDeath += Death;

        healthSlider.maxValue = currLevel.Health;
        healthSlider.value = currLevel.GetCurrentHealth();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(transform.position, player.transform.position);

        gameManager = GameManager.Instance;

        StartCoroutine(SpawnAmmoEnumerator());
    }

    private void Upgrade()
    {
        currLevel.OnLevelUpgrade -= Upgrade;
        currLevel.OnTreeDeath -= Death;

        if (currLevel.ResourceNeed == 0) return;

        currLevelIndex++;

        currLevel = Instantiate(treeLevels[currLevelIndex]);

        currLevel.Initialize();

        if (currLevel.ResourceNeed == 0) levelText.text = "MAX";
        else levelText.text = (currLevelIndex + 1).ToString();

        if (currLevel.CanAttack)
        {
            if (attackCoroutine != null)
                StopCoroutine(attackCoroutine);

            attackCoroutine = StartCoroutine(TreeAttack());
        }

        healthSlider.maxValue = currLevel.Health;
        healthSlider.value = currLevel.GetCurrentHealth();

        currLevel.OnLevelUpgrade += Upgrade;
        currLevel.OnTreeDeath += Death;
    }

    private void Death()
    {

    }

    IEnumerator SpawnAmmoEnumerator()
    {
        while (gameManager.GameState)
        {
            yield return new WaitForSeconds(currLevel.TimeSpawnFruit);

            SpawnAmmo();
        }
    }

    private void SpawnAmmo()
    {

        Vector3 spawnPos = RandomPointOnXZCircle(transform.position, distance);

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
                currLevel.AddResource();
                expImage.fillAmount = currLevel.GetProgressResource();
                break;
            case ResourceType.Bad:
                currLevel.TakeDamage();
                healthSlider.value = currLevel.GetCurrentHealth();
                break;
        }
    }

    private IEnumerator TreeAttack()
    {
        while (gameManager.GameState)
        {
            yield return new WaitForSeconds(currLevel.ColdownAttack);

            while (FindBedEnemy().Count == 0)
                yield return null;

            foreach(GameObject go in FindBedEnemy())
            {
                GameObject projectile = Instantiate(ammoPrefab, transform.position, Quaternion.identity);

                projectile.GetComponent<ProjectileMovement>().Shoot(go.transform.position);
            }
        }
    }



    private List<GameObject> FindBedEnemy()
    {
        List<GameObject> enemyList = new List<GameObject>();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject go in enemies)
        {
            if (enemyList.Count > currLevel.NumberOfAttack)
                break;

            if (Vector3.Distance(transform.position, go.transform.position) < attackRange)
            {
                EnemyMovement enemy = go.GetComponent<EnemyMovement>();

                if (enemy.resourceType == ResourceType.Bad)
                    enemyList.Add(go);
            }

        }

        return enemyList;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

public enum ResourceType
{
    Good, Bad
}
