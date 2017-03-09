using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class GameController : MonoBehaviour {

  #region Mono Behaviour

  [SerializeField] private GameObject levelPrefab;
  private LevelController levelController;

  private BoardManager boardManager;

  #endregion

  #region Mono Behaviour

  void Awake() {
    levelController = Instantiate(levelPrefab, transform).GetComponent<LevelController>();
    boardManager = new BoardManager(GameObject.FindObjectOfType<Camera>());
    Screen.orientation = ScreenOrientation.Portrait;
  }

  void Start() {
    levelController.Play();
  }

  void OnEnable() {
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
    EventManager.StartListening<NewGameEvent>(OnNewGameEvent);
  }

  void OnDisable() {
    EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
    EventManager.StopListening<NewGameEvent>(OnNewGameEvent);
  }

  #endregion

  #region Event Behaviour

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    levelController.Stop();
  }

  void OnNewGameEvent(NewGameEvent newGameEvent) {
    levelController.Play();
  }

  #endregion

}