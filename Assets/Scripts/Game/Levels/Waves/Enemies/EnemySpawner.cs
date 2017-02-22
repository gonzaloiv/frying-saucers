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

  public GameObject SpawnEnemy(Enemy enemy, GameObject player) {
    GameObject enemyObject = enemyPools[(int) enemy.EnemyType].PopObject();
    enemyObject.transform.position = new Vector2(Random.Range(-Board.BoardSize.x / 2, Board.BoardSize.x / 2),enemy.Position.y);
    enemyObject.GetComponent<IEnemyBehaviour>().Initialize(player);
    enemyObject.SetActive(true);
    enemyObject.GetComponent<IEnemyBehaviour>().Play(); // TODO: repensar cómo se inician los comportamientos de los enemigos
    return enemyObject;
  }

  #endregion

}
