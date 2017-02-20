using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour {

	#region Fields

  private EnemySpawner enemySpawner;

  private 

  #endregion

  #region Mono Behaviour

  void Awake() {
    enemySpawner = GetComponent<EnemySpawner>();
  }

  #endregion

  #region Public Behaviour

  public GameObject Enemy(Enemy enemy) {
    return enemySpawner.SpawnEnemy(enemy);
  }

  #endregion

}
