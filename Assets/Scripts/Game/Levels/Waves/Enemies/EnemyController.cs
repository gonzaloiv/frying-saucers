using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Models;

public class EnemyController : MonoBehaviour {

	#region Fields

  private EnemySpawner enemySpawner;

  #endregion

  #region Mono Behaviour

  void Awake() {
    enemySpawner = GetComponent<EnemySpawner>();
  }

  #endregion

  #region Public Behaviour

  public GameObject Enemy(Enemy enemy, GameObject player) {
    return enemySpawner.SpawnEnemy(enemy, player);
  }

  #endregion

}
