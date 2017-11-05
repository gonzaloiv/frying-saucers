using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    #region Fields / Properties

    [SerializeField] private GameObject enemiesPrefab;
    private EnemySpawner enemySpawner;

    private Vector2[] currentWaveEnemyGrid;
    private Enemy[] currentWaveEnemies;

    #endregion

    #region Mono Behaviour

    void Awake () {
        enemySpawner = Instantiate(enemiesPrefab, transform).GetComponent<EnemySpawner>();
    }

    #endregion

    #region Public Behaviour

    public GameObject[] SpawnWaveEnemies (WaveData waveData, GameObject player) {
        currentWaveEnemyGrid = Board.EnemyGrid(waveData.EnemyTypes.Length);
        currentWaveEnemies = new Enemy[waveData.EnemyTypes.Length];
        GameObject[] currentWaveEnemyObjects = new GameObject[waveData.EnemyTypes.Length];
        for (int i = 0; i < waveData.EnemyTypes.Length; i++) {
            Enemy enemy = GetEnemyByType(waveData.EnemyTypes[i], i, waveData.RoutineTime);
            currentWaveEnemies[i] = enemy;
            currentWaveEnemyObjects[i] = enemySpawner.SpawnEnemy(enemy, player);
        }
        return currentWaveEnemyObjects;
    }

    public GameObject[] SpawnRandomWaveEnemies (WaveData waveData, GameObject player) {
        currentWaveEnemyGrid = Board.EnemyGrid(GameConfig.RandomWaveEnemyAmount);
        currentWaveEnemies = GetRandomWaveEnemies(GameConfig.RandomWaveEnemyAmount, waveData.RoutineTime);
        GameObject[] currentWaveEnemyObjects = new GameObject[GameConfig.RandomWaveEnemyAmount];
        for (int i = 0; i < currentWaveEnemies.Length; i++)
            currentWaveEnemyObjects[i] = enemySpawner.SpawnEnemy(currentWaveEnemies[i], player);
        return currentWaveEnemyObjects;
    }

    public GameObject SpawnRandomEnemy (int index, GameObject player) {
        Enemy enemy = currentWaveEnemies[index];
        enemy.SetRandomType();
        return enemySpawner.SpawnEnemy(enemy, player);
    }

    #endregion

    #region Private Behaviour

    private Enemy[] GetRandomWaveEnemies (int enemyAmount, float[] shootRoutineTime) {
        Enemy[] enemies = new Enemy[enemyAmount];
        for (int i = 0; i < enemies.Length; i++) {
            EnemyType enemyType = (EnemyType) UnityEngine.Random.Range(0, EnemyType.GetNames(typeof(EnemyType)).Length - 1);
            enemies[i] = GetEnemyByType(enemyType, i, shootRoutineTime);
        }
        return enemies;
    }

    private Enemy GetEnemyByType (EnemyType enemyType, int enemyIndex, float[] waveRoutineTime) {
        EnemyScore enemyScore = (EnemyScore) (int) enemyType;
        Vector2 enemyPosition = currentWaveEnemyGrid[enemyIndex];
        float enemyShootRoutineTime = Random.Range(waveRoutineTime[0], waveRoutineTime[1]);
        return new Enemy(enemyType, enemyPosition, enemyScore, enemyShootRoutineTime);
    }

    #endregion
	
}
