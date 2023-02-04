using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tree Level")]
public class TreeLevel : ScriptableObject
{
    [SerializeField] private int level;
    [SerializeField] private int health;
    [SerializeField] private float timeSpawnFruit;
    [SerializeField] private bool canAttack;
    [SerializeField] private float coldownAttack;
    [SerializeField] private int numberOfAttack;
    [SerializeField] private int resourceNeed;

    public int Level => level;
    public int Health => health;
    public float TimeSpawnFruit => timeSpawnFruit;
    public bool CanAttack => canAttack;
    public float ColdownAttack => coldownAttack;
    public int NumberOfAttack => numberOfAttack;
    public int ResourceNeed => resourceNeed;

    private int currHealth;
    private int currResource;

    public delegate void LevelUpgrade();
    public event LevelUpgrade OnLevelUpgrade;

    public delegate void TreeDead();
    public event TreeDead OnTreeDeath;

    public void Initialize()
    {
        currHealth = Health;
        currResource = 0;
    }

    public void AddResource()
    {
        currResource++;

        if (currResource >= ResourceNeed)
            OnLevelUpgrade?.Invoke();
    }

    public float GetProgressResource()
    {
        return currResource / ResourceNeed;
    }

    public int GetCurrentHealth()
    {
        return currHealth;
    }

    public void TakeDamage()
    {
        currHealth--;

        if (currHealth <= 0)
            OnTreeDeath?.Invoke();
    }
}
