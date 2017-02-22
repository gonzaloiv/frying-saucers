using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject enemyPrefab;
  private EnemyController enemyController;

  private List<GameObject> currentWaveObjects;

  #endregion

  #region Mono Behaviour

  void Awake() {
    enemyController = Instantiate(enemyPrefab, transform).GetComponent<EnemyController>(); 
  }

  #endregion

  #region Public Behaviour

  public List<GameObject> Wave(Wave wave, GameObject player) {
    currentWaveObjects = new List<GameObject>();
    foreach (Enemy enemy in wave.Enemies)
      currentWaveObjects.Add(enemyController.Enemy(enemy, player)); 
    return currentWaveObjects;
  }

  #endregion

}
