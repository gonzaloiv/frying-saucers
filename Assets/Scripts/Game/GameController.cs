using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class GameController : MonoBehaviour {

  #region Mono Behaviour

  [SerializeField] private GameObject levelPrefab;
  private LevelController levelController;

  private Camera camera;
  private Board board;

  #endregion

  #region Mono Behaviour

  void Awake() {
    camera = GameObject.FindObjectOfType<Camera>();
    levelController = Instantiate(levelPrefab, transform).GetComponent<LevelController>();
    board = new Board(camera, Config.BOARD_SIZE);
    Screen.orientation = ScreenOrientation.Portrait;
    Screen.SetResolution((int) Config.BOARD_SIZE.x, (int) Config.BOARD_SIZE.y, false);
  }

  void Start() {
    levelController.Level();
  }

  void OnEnable() {
    EventManager.StartListening<NewGameEvent>(OnNewGameEvent);
  }

  void OnDisable() {
    EventManager.StopListening<NewGameEvent>(OnNewGameEvent);
  }

  #endregion

  #region Event Behaviour

  void OnNewGameEvent(NewGameEvent newGameEvent) {
    levelController.Level();
  }

  #endregion

}