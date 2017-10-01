using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureSpawner : MonoBehaviour {

    #region Fields

    [SerializeField] private GameObject gesturePrefab;
    private GameObjectPool gesturePool;

    #endregion

    #region Mono Behaviour

    void Awake () {
        gesturePool = new GameObjectPool("GesturePool", gesturePrefab, 2, transform);
    }

    #endregion

    #region Public Behaviour

    public LineRenderer SpawnGestureLineRenderer (Transform parent) {

        GameObject gesture = gesturePool.PopObject();
        gesture.transform.position = parent.position;
        gesture.transform.rotation = parent.rotation;
        gesture.SetActive(true);

        LineRenderer gestureLineRenderer = gesture.GetComponent<LineRenderer>();
        gestureLineRenderer.sortingLayerName = SortingLayer.UI.ToString(); // TODO: que esto se defina al crear el objeto en la pool

        return gestureLineRenderer;

    }

    #endregion
	
}
