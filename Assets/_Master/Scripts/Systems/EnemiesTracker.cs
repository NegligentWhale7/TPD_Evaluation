using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesTracker : MonoBehaviour
{
    public static EnemiesTracker Instance { get; private set; }

    private List<GameObject> _enemies = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        _enemies.Remove(enemy);
    }

    public bool AreEnemiesAlive()
    {
        return _enemies.Count > 0;
    }
}
