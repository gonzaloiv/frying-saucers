using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    #region Fields / Properties

    [SerializeField] private GameObject enemiesPrefab;
    private EnemySpawner enemySpawner;
    private GameObject player;

    #endregion

    #region Mono Behaviour

    void Awake () {
        enemySpawner = Instantiate(enemiesPrefab, transform).GetComponent<EnemySpawner>();
    }

    #endregion

    #region Public Behaviour

    public void Init(GameObject player) {
        this.player = player;
    }

    public GameObject[] SpawnWaveEnemies (WaveData waveData) {
        GameObject[] currentWaveEnemies = new GameObject[waveData.WaveEnemies.EnemyGridAmount];
        for (int i = 0; i < waveData.WaveEnemies.EnemyGridAmount; i++) {
            EnemyType enemyType = waveData.WaveEnemies.EnemyTypes[Random.Range(0, waveData.WaveEnemies.EnemyTypes.Length)];
            Enemy enemy = new Enemy(enemyType, Board.GetEnemyGridPosition(waveData.WaveEnemies.EnemyGridAmount, i), waveData.EnemyRoutineTime);
            currentWaveEnemies[i] = enemySpawner.SpawnEnemy(enemy, player);
        }
        return currentWaveEnemies;
    }

    public GameObject SpawnEnemy (WaveData waveData, int index) { // TODO: This method should receive an Enemy and spawn from its information
        EnemyType enemyType = waveData.WaveEnemies.EnemyTypes[Random.Range(0, waveData.WaveEnemies.EnemyTypes.Length)];
        Enemy enemy = new Enemy(enemyType, Board.GetEnemyGridPosition(waveData.WaveEnemies.EnemyGridAmount, index), waveData.EnemyRoutineTime);
        return enemySpawner.SpawnEnemy(enemy, player);
    }

    #endregion

}