using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemyTypeLabelSpawner : MonoBehaviour {

    #region Fields

    [Header("Same order as EnemyType")]
    [SerializeField] private GameObject[] gesturePrefabs;

    private GameObjectArrayPool gesturePool;
    private List<GameObject> gestures;

    #endregion

    #region Mono Behaviour

    void Awake () {
        gesturePool = new GameObjectArrayPool("GesturePool", gesturePrefabs, 16, transform);
    }

    #endregion

    #region Public Behaviour

    public void ShowGestures (GameObject[] enemies, float time) {
        StartCoroutine(ShowGesturesRoutine(enemies, time));
    }

    public void ShowGesture (GameObject enemy, float time) {
        StartCoroutine(ShowGestureRoutine(enemy, time));
    }

    public void HideGestures() {
        if(gestures != null)
            gestures.ForEach(gesture => gesture.SetActive(false));
    }

    #endregion

    #region Private Behaviour

    private IEnumerator ShowGesturesRoutine (GameObject[] currentEnemies, float time) {
        gestures = new List<GameObject>(); 
        for (int i = 0; i < currentEnemies.Length; i++) {
            Enemy enemy = currentEnemies[i].GetComponent<EnemyController>().Enemy;
            GameObject gesture = gesturePool.PopObject((int) enemy.EnemyType);
            gesture.transform.position = enemy.Position + new Vector2(0, -0.7f);
            gesture.SetActive(true);
            gestures.Add(gesture);
        }
        yield return new WaitForSeconds(time);
        gestures.ForEach(gesture => gesture.SetActive(false));
    }

    private IEnumerator ShowGestureRoutine (GameObject currentEnemy, float time) {
        GameObject gesture = new GameObject();
        Enemy enemy = currentEnemy.GetComponent<EnemyController>().Enemy;
        gesture = gesturePool.PopObject((int) enemy.EnemyType - 1);
        gesture.transform.position = enemy.Position + new Vector2(0, -0.7f);
        gesture.SetActive(true);
        yield return new WaitForSeconds(time);
        gesture.SetActive(false);
    }

    #endregion
	
}