using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	#region Fields

  [SerializeField] GameObject ufoPrefab;
  private GameObjectPool ufoPool;

  private List<IPool> enemyPools = new List<IPool>();

  #endregion

  #region Mono Behaviour

  void Awake() {
    ufoPool = new GameObjectPool("UFOPool", ufoPrefab, 5, transform);
    enemyPools.Add(ufoPool);
  }

  #endregion

  #region Public Behaviour

  public GameObject SpawnEnemy(Enemy enemy) {
    GameObject enemyObject = enemyPools[(int) enemy.EnemyType].PopObject();
    enemyObject.transform.position = enemy.Position;
    enemyObject.SetActive(true);
    return enemyObject;
  }

  #endregion

}
