using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class GameController : MonoBehaviour {

  #region Mono Behaviour

  [SerializeField] private GameObject levelPrefab;
  private LevelController levelController;

  private Board board;

  #endregion

	#region Mono Behaviour

  void Awake() {
    levelController = Instantiate(levelPrefab, transform).GetComponent<LevelController>();
    board = new Board(Config.BOARD_SIZE);
    Screen.orientation = ScreenOrientation.Portrait;
	Screen.SetResolution(512, 683, false);
  }

  void Start() {
    levelController.Level();
	print(Application.persistentDataPath);
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