using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class WaveController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject enemyPrefab;
  private EnemyController enemyController;

  private Wave currentWave;
  private Wave previousWave;

  private List<GameObject> currentWaveObjects;

  #endregion

  #region Mono Behaviour

  void Awake() {
    enemyController = Instantiate(enemyPrefab, transform).GetComponent<EnemyController>();
  }

  #endregion

  #region Public Behaviour

  public List<GameObject> Wave(GameObject player) {
    previousWave = currentWave;
    currentWave = previousWave != null ? new Wave(++previousWave.WavePosition) : new Wave(0);

    return Wave(currentWave, player);
  }

  public List<GameObject> Wave(Wave wave, GameObject player) {
    currentWaveObjects = new List<GameObject>();
    foreach (Enemy enemy in wave.Enemies)
      currentWaveObjects.Add(enemyController.Enemy(enemy, player)); 
    return currentWaveObjects;
  }

  #endregion

}
