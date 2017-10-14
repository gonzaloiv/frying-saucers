﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class GameController : StateMachine {

    #region Mono Behaviour

    [Header("Game Data")]
    [SerializeField] private GameData gameData;
    [SerializeField] private GameConfigData gameConfigData;

    [Header("Game Controllers")]
    [SerializeField] private LevelController levelController;
    [SerializeField] private GameObject inputManager;

    [Header("Game Screens")]
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject levelScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject leaderboardScreen;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject tutorialScreen;

    public GameData GameData { get { return gameData; } }
    public GameConfigData GameConfigData { get { return gameConfigData; } }
    public LevelController LevelController { get { return levelController; } }
    public GameObject InputManager { get { return inputManager; } }
    public GameObject MainMenuScreen { get { return mainMenuScreen; } }
    public GameObject LevelScreen { get { return levelScreen; } }
    public GameObject GameOverScreen { get { return gameOverScreen; } }
    public GameObject LeaderboardScreen { get { return leaderboardScreen; } }
    public GameObject CreditsScreen { get { return creditsScreen; } }
    public GameObject TutorialScreen { get { return tutorialScreen; } }

    public LevelData CurrentLevelData { get { return gameData.Levels[currentLevelIndex]; } }
    public LevelData TutorialLevelData { get { return gameData.Levels.FirstOrDefault(level => level.LevelType == LevelType.TutorialLevel); } }

    private int currentLevelIndex = 0;

    #endregion

    #region Events

    public delegate void NewGameEventHandler ();
    public static event NewGameEventHandler NewGameEvent = delegate {};

    public delegate void CreditsEventHandler ();
    public static event CreditsEventHandler CreditsEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        ChangeState<GameStates.InitState>();
    }

    void Start () {
        NewGameEvent.Invoke();
    }

    void OnEnable () {
        LevelScreenController.GameOverEvent += OnGameOverEvent;
        LevelController.LevelEndEvent += OnLevelEndEvent;
    }

    void OnDisable () {
        LevelScreenController.GameOverEvent -= OnGameOverEvent;
        LevelController.LevelEndEvent -= OnLevelEndEvent;
    }

    #endregion

    #region Public Behaviour

    public void ToMainMenuState () {
        ChangeState<GameStates.MainMenuState>();
    }

    public void ToLevelState () {
        ChangeState<GameStates.LevelState>();
    }

    public void ToTutorialLevelState () {
        ChangeState<GameStates.TutorialState>();
    }

    public void ToGameOverState () {
        ChangeState<GameStates.GameOverState>();
    }

    public void ToLeaderboardState () {
        ChangeState<GameStates.LeaderboardState>();
    }

    public void ToCreditsState () {
        CreditsEvent.Invoke();
        ChangeState<GameStates.CreditsState>();
    }

    public void OnGameOverEvent (GameOverEventArgs gameOverEventArgs) {
        levelController.ToStopState();
        ToGameOverState();
    }

    public void OnLevelEndEvent () {
        ToMainMenuState();
    }

    #endregion

}