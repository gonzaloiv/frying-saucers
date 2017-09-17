using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenController : MonoBehaviour {

    #region Fields

    [SerializeField] private GameObject gameOverScrenPrefab;
    [SerializeField]  private Camera camera;
    private GameObject gameOverScreen;
    private Canvas canvas;

    #endregion

    #region State Behaviour

    void Awake () {
        gameOverScreen = Instantiate(gameOverScrenPrefab, transform);
        gameOverScreen.SetActive(false);
        canvas = gameOverScreen.GetComponent<Canvas>();
        canvas.worldCamera = camera;
        canvas.sortingLayerName = "UI";
    }

    void OnEnable () {
        EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
    }

    void OnDisable () {
        EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
    }

    #endregion

    #region Event Behaviour

    void OnGameOverEvent (GameOverEvent gameOverEvent) {
        gameOverScreen.GetComponent<GameOverScreenBehaviour>().Play();
    }

    #endregion

}
