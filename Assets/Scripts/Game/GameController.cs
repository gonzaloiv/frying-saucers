﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class GameController : MonoBehaviour {

  #region Mono Behaviour

  [SerializeField] private GameObject levelPrefab;
  private LevelController levelController;

  #endregion

  #region Mono Behaviour

  void Awake() {
    levelController = Instantiate(levelPrefab, transform).GetComponent<LevelController>();
    Screen.orientation = ScreenOrientation.Portrait;
    new Board();
  }

  void Start() {
    EventManager.TriggerEvent(new NewGameEvent());
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