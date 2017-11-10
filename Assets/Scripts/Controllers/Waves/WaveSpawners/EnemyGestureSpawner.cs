using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemyGestureSpawner : MonoBehaviour {

    #region Fields

    private Vector2 LABEL_POSITION = new Vector2(0, -1);

    [Header("Same order as EnemyType")]
    [SerializeField] private List<GameObject> gesturePrefabs;

    private List<GameObjectPool> gesturePools;
    private List<GameObject> gestures;

    #endregion

    #region Mono Behaviour

    void Awake () {
        gesturePools = new List<GameObjectPool>();
        gesturePrefabs.ForEach(gesturePrefab => gesturePools.Add(new GameObjectPool(gesturePrefab.name + "s", gesturePrefab, 4, transform)));
    }

    #endregion

    #region Public Behaviour

    public void ShowGestures (GameObject[] enemies, float time) {
        StartCoroutine(ShowGesturesRoutine(enemies, time));
    }

    public void HideGestures () {
        if (gestures != null)
            gestures.ForEach(gesture => gesture.SetActive(false));
    }

    #endregion

    #region Private Behaviour

    private IEnumerator ShowGesturesRoutine (GameObject[] currentEnemies, float time) {
        gestures = new List<GameObject>(); 
        for (int i = 0; i < currentEnemies.Length; i++)
            gestures.Add(ShowGesture(currentEnemies[i].GetComponent<EnemyController>().Enemy));
        yield return new WaitForSeconds(time);
        HideGestures();
    }

    private GameObject ShowGesture (Enemy enemy) {
        GameObject gesture = gesturePools[(int) enemy.EnemyType].PopObject();
        gesture.transform.position = enemy.Position + LABEL_POSITION;
        gesture.SetActive(true);
        return gesture;
    }

    #endregion
	
}