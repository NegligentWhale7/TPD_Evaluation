using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCharacterSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private int minEnemies = 1;
    [SerializeField] private int maxEnemies;
    [SerializeField] private GameObject[] waveOneEnemies;
    [SerializeField] private GameObject[] waveTwoEnemies;
    [SerializeField] private GameObject[] waveThreeEnemies;
    [SerializeField] private Transform[] enemiesPositions;

    private int enemiesToSpawn;
    private int currentWave = 0;

    private void Start()
    {
        SetWaveOfEnemies();
        currentWave++;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.GameOver
           || GameManager.Instance.CurrentGameState == GameManager.GameState.InterWave) return;

        if (!EnemiesTracker.Instance.AreEnemiesAlive())
        {
            GameManager.Instance.InterWave();
            SetWaveOfEnemies();
            currentWave++;
        }
    }

    private void InstantiateWaveOfEnemies(GameObject[] enemies)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            var enemy = Instantiate(enemies[Random.Range(0, enemies.Length)]);
            enemy.transform.position = enemiesPositions[i].position;
            EnemiesTracker.Instance.AddEnemy(enemy);
        }
    }

    private void SetWaveOfEnemies()
    {
        switch(currentWave)
        {
            case 0:
            case 1:                
                enemiesToSpawn = Random.Range(minEnemies, maxEnemies);
                InstantiateWaveOfEnemies(waveOneEnemies);
                break;
            case 2:
                minEnemies = 5;
                maxEnemies = 8;
                enemiesToSpawn = Random.Range(minEnemies, maxEnemies);
                InstantiateWaveOfEnemies(waveTwoEnemies);
                break;
            case 3:
                minEnemies = 8;
                maxEnemies = 12;
                enemiesToSpawn = Random.Range(minEnemies, maxEnemies);
                InstantiateWaveOfEnemies(waveThreeEnemies);
                break;
            default:
                minEnemies = 10;
                maxEnemies = 15;
                enemiesToSpawn = Random.Range(minEnemies, maxEnemies);
                InstantiateWaveOfEnemies(waveTwoEnemies);
                InstantiateWaveOfEnemies(waveThreeEnemies);
                break;
        }
    }
}
