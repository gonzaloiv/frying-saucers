using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class EnemySpawner : MonoBehaviour {

	#region Fields

  [SerializeField] GameObject[] ufoPrefabs;
  private GameObjectArrayPool ufoPool;

  #endregion

  #region Mono Behaviour

  void Awake() {
    ufoPool = new GameObjectArrayPool("UFOPool", ufoPrefabs, 5, transform);
  }

  #endregion

  #region Public Behaviour

  public GameObject SpawnEnemy(Enemy enemy, GameObject player) {
    GameObject enemyObject = ufoPool.PopObject((int) enemy.EnemyType);
    enemyObject.transform.position = enemy.Position;
    enemyObject.SetActive(true);
    enemyObject.transform.rotation = Quaternion.identity;
    enemyObject.GetComponent<UFOController>().Initialize(enemy.EnemyType);
    enemyObject.GetComponent<IEnemyBehaviour>().Initialize(player);
    enemyObject.GetComponent<IEnemyBehaviour>().Play();
    return enemyObject;
  }

  #endregion

}
