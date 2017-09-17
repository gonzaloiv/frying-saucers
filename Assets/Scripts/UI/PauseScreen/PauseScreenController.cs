using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenController : MonoBehaviour {

    #region Fields

    [SerializeField] private GameObject pauseScreenPrefab;
    private GameObject pauseScreen;
    private Canvas canvas;

    #endregion

    #region State Behaviour

    void Awake () {
        pauseScreen = Instantiate(pauseScreenPrefab, transform);
        pauseScreen.SetActive(false);
        canvas = pauseScreen.GetComponent<Canvas>();
        canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canvas.sortingLayerName = "UI";
    }

    void OnEnable () {
        EventManager.StartListening<EscapeInputEvent>(OnEscapeInput);
    }

    void OnDisable () {
        EventManager.StopListening<EscapeInputEvent>(OnEscapeInput);
    }

    #endregion

    #region Event Behaviour

    void OnEscapeInput (EscapeInputEvent escapeInputEvent) {
        pauseScreen.SetActive(true);
    }

    #endregion

}
