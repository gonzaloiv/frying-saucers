using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour {

    #region Fields

    [Header("Same order than EnemyType")]
    [SerializeField] List<GameObject> ufoPrefabs; 
    private List<GameObjectPool> ufoPools;

    #endregion

    #region Mono Behaviour

    void Awake () {
        ufoPools = new List<GameObjectPool>();
        ufoPrefabs.ForEach(ufoPrefab => ufoPools.Add(new GameObjectPool(ufoPrefab.name + "s", ufoPrefab, 4, transform)));
    }

    #endregion

    #region Public Behaviour

    public GameObject SpawnEnemy (Enemy enemy, GameObject player) {
        GameObject enemyObject = ufoPools[(int) enemy.EnemyType].PopObject();
        enemyObject.transform.position = Board.GetRandomOutOfBoardPosition();
        enemyObject.GetComponent<SpriteRenderer>().flipY = false;
        enemyObject.GetComponent<EnemyController>().Init(enemy, player);
        enemyObject.SetActive(true);
        return enemyObject;
    }

    #endregion

}
