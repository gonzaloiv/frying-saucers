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

    public GameObject[] SpawnWaveEnemies (EnemyType[] waveEnemyTypes, GameObject player) {
        currentWaveEnemyGrid = Board.EnemyGrid(waveEnemyTypes.Length);
        currentWaveEnemies = new Enemy[waveEnemyTypes.Length];
        GameObject[] currentWaveEnemyObjects = new GameObject[waveEnemyTypes.Length];
        for (int i = 0; i < waveEnemyTypes.Length; i++) {
            Enemy enemy = GetEnemyByType(waveEnemyTypes[i], i);
            currentWaveEnemies[i] = enemy;
            currentWaveEnemyObjects[i] = enemySpawner.SpawnEnemy(enemy, player);
        }
        return currentWaveEnemyObjects;
    }

    public GameObject[] SpawnRandomWaveEnemies (GameObject player) {
        currentWaveEnemyGrid = Board.EnemyGrid(GameConfig.RandomWaveEnemyAmount);
        currentWaveEnemies = GetRandomWaveEnemies(GameConfig.RandomWaveEnemyAmount);
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

    private Enemy[] GetRandomWaveEnemies (int enemyAmount) {
        Enemy[] enemies = new Enemy[enemyAmount];
        for (int i = 0; i < enemies.Length; i++) {
            EnemyType enemyType = (EnemyType) UnityEngine.Random.Range(0, EnemyType.GetNames(typeof(EnemyType)).Length - 1);
            enemies[i] = GetEnemyByType(enemyType, i);
        }
        return enemies;
    }

    private Enemy GetEnemyByType (EnemyType enemyType, int enemyIndex) {
        EnemyScore enemyScore = (EnemyScore) (int) enemyType;
        Vector2 enemyPosition = currentWaveEnemyGrid[enemyIndex];
        return new Enemy(enemyType, enemyPosition, enemyScore);
    }

    #endregion
	
}
