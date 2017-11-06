using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    #region Fields / Properties

    [SerializeField] private GameObject enemiesPrefab;
    private EnemySpawner enemySpawner;

    private Vector2[] currentWaveEnemyGrid;
    private float[] currenWaveRoutineTime;

    #endregion

    #region Mono Behaviour

    void Awake () {
        enemySpawner = Instantiate(enemiesPrefab, transform).GetComponent<EnemySpawner>();
    }

    #endregion

    #region Public Behaviour

    public GameObject[] SpawnWaveEnemies (WaveData waveData, GameObject player) {
        currenWaveRoutineTime = waveData.RoutineTime;
        currentWaveEnemyGrid = Board.EnemyGrid(waveData.EnemyTypes.Length);
        GameObject[] currentWaveEnemyObjects = new GameObject[waveData.EnemyTypes.Length];
        for (int i = 0; i < waveData.EnemyTypes.Length; i++) {
            Enemy enemy = new Enemy(waveData.EnemyTypes[i], currentWaveEnemyGrid[i], waveData.RoutineTime);
            currentWaveEnemyObjects[i] = enemySpawner.SpawnEnemy(enemy, player);
        }
        return currentWaveEnemyObjects;
    }

    public GameObject[] SpawnRandomWaveEnemies (WaveData waveData, GameObject player) {
        currenWaveRoutineTime = waveData.RoutineTime;
        currentWaveEnemyGrid = Board.EnemyGrid(GameConfig.RandomWaveEnemyAmount);
        GameObject[] currentWaveEnemyObjects = new GameObject[GameConfig.RandomWaveEnemyAmount];
        for (int i = 0; i < GameConfig.RandomWaveEnemyAmount; i++) {
            Enemy enemy = new Enemy(currentWaveEnemyGrid[i], waveData.RoutineTime);
            currentWaveEnemyObjects[i] = enemySpawner.SpawnEnemy(enemy, player);
        }
        return currentWaveEnemyObjects;
    }

    public GameObject SpawnRandomEnemy (int index, GameObject player) {
        Enemy enemy = new Enemy(currentWaveEnemyGrid[index], currenWaveRoutineTime);
        return enemySpawner.SpawnEnemy(enemy, player);
    }

    #endregion

}