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
    enemyObject.transform.position = BoardManager.GetRandomOutOfBoardPosition();
    enemyObject.SetActive(true);
    enemyObject.GetComponent<IEnemyBehaviour>().Initialize(player);
    enemyObject.GetComponent<IEnemyController>().Initialize(enemy);
    enemyObject.GetComponent<SpriteRenderer>().flipY = false;

    return enemyObject;
  }

  #endregion

}
