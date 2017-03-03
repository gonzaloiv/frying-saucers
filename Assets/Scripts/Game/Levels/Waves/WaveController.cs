using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class WaveController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject enemyPrefab;
  private EnemySpawner enemySpawner;

  private Wave currentWave;
  private Wave previousWave;

  private List<GameObject> currentWaveObjects;

  #endregion

  #region Mono Behaviour

  void Awake() {
    enemySpawner = Instantiate(enemyPrefab, transform).GetComponent<EnemySpawner>();
  }

  #endregion

  #region Public Behaviour

  public List<GameObject> Wave(GameObject player) {
    previousWave = currentWave;
    currentWave = new Wave(3);

    return Wave(currentWave, player);
  }

  public List<GameObject> Wave(Wave wave, GameObject player) {
    currentWaveObjects = new List<GameObject>();
    foreach (Enemy enemy in wave.Enemies)
      currentWaveObjects.Add(enemySpawner.SpawnEnemy(enemy, player)); 

    return currentWaveObjects;
  }

  #endregion

}
