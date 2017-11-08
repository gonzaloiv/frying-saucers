using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using GameStates;

public class GameController : StateMachine {

    #region Mono Behaviour

    [Header("Game Data")]
    [SerializeField] private GameData gameData;
    [SerializeField] private GameConfigData gameConfigData;

    [Header("Game Objects")]
    [SerializeField] private LevelController levelController;
    [SerializeField] private Camera gameCamera;

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
    public Camera GameCamera { get { return gameCamera; } }
    public GameObject MainMenuScreen { get { return mainMenuScreen; } }
    public GameObject LevelScreen { get { return levelScreen; } }
    public GameObject GameOverScreen { get { return gameOverScreen; } }
    public GameObject LeaderboardScreen { get { return leaderboardScreen; } }
    public GameObject CreditsScreen { get { return creditsScreen; } }
    public GameObject TutorialScreen { get { return tutorialScreen; } }

    public LevelData RandomLevelData { get { return gameData.Levels[(int) LevelType.RandomLevel]; } }
    public LevelData TutorialLevelData { get { return gameData.Levels[(int) LevelType.TutorialLevel]; } }

    private int currentLevelIndex = 0;

    #endregion

    #region Events

    public delegate void NewGameEventHandler ();
    public static event NewGameEventHandler NewGameEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        ResetGameScreens();
        ChangeState<InitState>();
    }
        
    void OnEnable () {
        LevelController.LevelEndEvent += OnLevelEndEvent;
    }

    void OnDisable () {
        LevelController.LevelEndEvent -= OnLevelEndEvent;
    }

    #endregion

    #region Public Behaviour

    public void ToMainMenuState () {
        NewGameEvent.Invoke();
        ChangeState<MainMenuState>();
    }

    public void ToLevelState () {
        ChangeState<LevelState>();
    }

    public void ToTutorialState () {
        ChangeState<TutorialState>();
    }

    public void ToGameOverState () {
        ChangeState<GameOverState>();
    }

    public void ToLeaderboardState () {
        ChangeState<LeaderboardState>();
    }

    public void ToCreditsState () {
        ChangeState<CreditsState>();
    }

    public void OnLevelEndEvent () {
        ToMainMenuState(); // TODO: Increasing currentLevelIndex and initializing next level, at this moment: Level ≈ Mode
    }

    #endregion

    #region Private Behaviour

    private void ResetGameScreens () {
        mainMenuScreen.SetActive(false);
        levelScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        leaderboardScreen.SetActive(false);
        creditsScreen.SetActive(false);
        tutorialScreen.SetActive(false);       
    }

    #endregion

}