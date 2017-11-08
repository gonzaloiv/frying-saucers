using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    #region Fields

    [SerializeField] GameObject[] ufoPrefabs; // Same order than EnemyType
    private GameObjectArrayPool ufoPool;

    #endregion

    #region Mono Behaviour

    void Awake () {
        ufoPool = new GameObjectArrayPool("UFOPool", ufoPrefabs, 12, transform);
    }

    #endregion

    #region Public Behaviour

    public GameObject SpawnEnemy (Enemy enemy, GameObject player) {
        GameObject enemyObject = ufoPool.PopObject((int) enemy.EnemyType); // - 1 to match null enemy type
        enemyObject.transform.position = Board.GetRandomOutOfBoardPosition();
        enemyObject.GetComponent<SpriteRenderer>().flipY = false;
        enemyObject.GetComponent<EnemyController>().Init(enemy, player);
        enemyObject.SetActive(true);
        return enemyObject;
    }

    #endregion

}
