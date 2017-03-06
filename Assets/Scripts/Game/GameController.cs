using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class GameController : MonoBehaviour {

  #region Mono Behaviour

  [SerializeField] private GameObject levelPrefab;
  private LevelController levelController;

  private InputManager inputManager;
  private BoardManager boardManager;

  #endregion

  #region Mono Behaviour

  void Awake() {
    levelController = Instantiate(levelPrefab, transform).GetComponent<LevelController>();
    inputManager = GameObject.FindObjectOfType<InputManager>();
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
    inputManager.enabled = false;
    levelController.Stop();
  }

  void OnNewGameEvent(NewGameEvent newGameEvent) {
    inputManager.enabled = true;
    levelController.Play();
  }

  #endregion

}