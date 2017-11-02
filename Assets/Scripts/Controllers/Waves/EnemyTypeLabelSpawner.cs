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
    private List<Enemy> currentEnemies;
    private List<GameObject> gestures;

    #endregion

    #region Mono Behaviour

    void Awake () {
        gesturePool = new GameObjectArrayPool("GesturePool", gesturePrefabs, 16, transform);
    }

    void OnDisable () {
        StopAllCoroutines();
    }

    #endregion

    #region Public Behaviour

    public void Init () {
        ResetGestures();
    }

    public void AddGesture (Enemy enemy) {
        currentEnemies.Add(enemy);
    }

    public void SetGestureByIndex (int index, Enemy enemy) {
        currentEnemies[index] = enemy;
    }

    public void ShowGestures (float time) {
        StartCoroutine(ShowGesturesRoutine(time));
    }

    public void ShowGesture (int index, float time) {
        StartCoroutine(ShowGestureRoutine(index, time));
    }

    public void HideGestures () {
        StartCoroutine(HideGesturesRoutine());
    }

    #endregion

    #region Private Behaviour

    private IEnumerator ShowGesturesRoutine (float time) {
        gestures = new List<GameObject>(); 
        for (int i = 0; i < currentEnemies.Count; i++) {
            GameObject gesture = gesturePool.PopObject((int) currentEnemies[i].EnemyType);
            gesture.transform.position = currentEnemies[i].Position + new Vector2(0, -0.7f);
            gesture.SetActive(true);
            gestures.Add(gesture);
        }
        yield return new WaitForSeconds(time);
        gestures.ForEach(gesture => gesture.SetActive(false));
    }

    private IEnumerator ShowGestureRoutine (int index, float time) {
        GameObject gesture = new GameObject();
        gesture = gesturePool.PopObject((int) currentEnemies[index].EnemyType - 1);
        gesture.transform.position = currentEnemies[index].Position + new Vector2(0, -0.7f);
        gesture.SetActive(true);
        yield return new WaitForSeconds(time);
        gesture.SetActive(false);
    }

    private IEnumerator HideGesturesRoutine () {
        for (int i = 0; i < gestures.Count; i++) {
            gestures[i].SetActive(false);
            yield return new WaitForSeconds(.15f);
        }
    }

    private void ResetGestures () {
        if (gestures != null)
            gestures.ForEach(gesture => gesture.SetActive(false));
        currentEnemies = new List<Enemy>();       
    }

    #endregion
	
}