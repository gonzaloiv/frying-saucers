using System.Collections;
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
        Reset();
        ChangeState<GameStates.InitState>();
    }

    void Start () {
        NewGameEvent.Invoke();
    }

    void OnEnable () {
        Player.PlayerHitEvent += OnPlayerHitEvent;
        LevelController.LevelEndEvent += OnLevelEndEvent;
    }

    void OnDisable () {
        Player.PlayerHitEvent -= OnPlayerHitEvent;
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
        ChangeState<GameStates.CreditsState>();
    }

    public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
        if (playerHitEventArgs.IsDead) {
            DataManager.AddNewScore(new LeaderboardEntry(playerHitEventArgs.Score, DateTime.Now));
            ToGameOverState();
        }
    }

    public void OnLevelEndEvent () {
        ToMainMenuState();
    }

    #endregion

    #region Private Behaviour

    private void Reset () {
        mainMenuScreen.SetActive(false);
        levelScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        leaderboardScreen.SetActive(false);
        creditsScreen.SetActive(false);
        tutorialScreen.SetActive(false);       
    }

    #endregion

}