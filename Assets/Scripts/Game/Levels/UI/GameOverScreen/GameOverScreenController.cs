using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject gameOverScrenPrefab;
  private GameObject gameOverScreen;
  private Canvas canvas;

  #endregion

  #region State Behaviour

  void Awake() {
    gameOverScreen = Instantiate(gameOverScrenPrefab, transform);
    gameOverScreen.SetActive(false);
    canvas = gameOverScreen.GetComponent<Canvas>();
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    canvas.sortingLayerName = "UI";
  }

  void OnEnable() {
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
  }

  void OnDisable() {
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
  }

  #endregion

  #region Event Behaviour

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    gameOverScreen.SetActive(true);
  }

  #endregion

}
