using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScreen : MonoBehaviour {

    #region Fields

    private Canvas canvas;

    #endregion

    #region State Behaviour

    void Awake () {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canvas.sortingLayerName = "UI";
    }

    #endregion
	
}
