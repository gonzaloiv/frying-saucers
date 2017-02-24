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
  }

  void Start() {
    levelController.Level();
  }

  #endregion

}