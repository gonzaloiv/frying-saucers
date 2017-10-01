using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : StateMachine {

    #region Mono Behaviour

    [SerializeField] private LevelController levelController;
    [SerializeField] private GameObject inputManager;
    [SerializeField] private GameData gameData;

    [Header("Game Screens")]
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject levelScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject leaderboardScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject creditsScreen;

    public LevelController LevelController { get { return levelController; } }
    public GameObject InputManager { get { return inputManager; } }
    public GameObject MainMenuScreen { get { return mainMenuScreen; } }
    public GameObject LevelScreen { get { return levelScreen; } }
    public GameObject GameOverScreen { get { return gameOverScreen; } }
    public GameObject LeaderboardScreen { get { return leaderboardScreen; } }
    public GameObject PauseScreen { get { return pauseScreen; } }
    public GameObject CreditsScreen { get { return creditsScreen; } }
    public LevelData CurrentLevelData { get { return gameData.Levels[currentLevelIndex]; } }

    private int currentLevelIndex = 0;

    #endregion

    #region Mono Behaviour

    void Awake () {
        new Board();
        new Player(gameData.PlayerInitialLives);
        levelController.gameObject.SetActive(false);
        inputManager.SetActive(false);
        mainMenuScreen.SetActive(false);
        levelScreen.SetActive(false);
        mainMenuScreen.SetActive(false);
        leaderboardScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    void Start () {
        EventManager.TriggerEvent(new NewGameEvent());
        ToMainMenuState();
    }

    void OnEnable () {
        EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
        EventManager.StartListening<LevelEndEvent>(OnLevelEndEvent);
    }

    void OnDisable () {
        EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
        EventManager.StopListening<LevelEndEvent>(OnLevelEndEvent);
    }

    #endregion

    #region Public Behaviour

    public void ToMainMenuState () {
        ChangeState<GameStates.MainMenuState>();
    }

    public void ToLevelState () {
        ChangeState<GameStates.LevelState>();
    }

    public void ToGameOverState () {
        ChangeState<GameStates.GameOverState>();
    }

    public void ToLeaderboardState () {
        ChangeState<GameStates.LeaderboardState>();
    }

    public void ToPauseState () {
        ChangeState<GameStates.PauseState>();
    }

    public void ToCreditsState () {
        ChangeState<GameStates.CreditsState>();
    }

    public void OnGameOverEvent (GameOverEvent gameOverEvent) {
        levelController.ToStopState();
        ToGameOverState();
    }

    public void OnLevelEndEvent (LevelEndEvent levelEndEvent) {
        if (currentLevelIndex < gameData.Levels.Count - 1) {
            currentLevelIndex++;
            levelController.InitLevel(gameData.Levels[currentLevelIndex]);
        } else {
            currentLevelIndex = 0;
            int nextSceneIndex = levelEndEvent.LevelType == LevelType.TutorialLevel ? (int) GameScene.MainMenuScene : (int) GameScene.GameScene;
            StartCoroutine(LoadSceneRoutine(nextSceneIndex));
        }
    }

    #endregion

    #region Private Behaviour

    public IEnumerator LoadSceneRoutine (int sceneIndex) {
        yield return new WaitForSeconds(1);
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(sceneIndex);
        sceneLoading.allowSceneActivation = false;
        while (!sceneLoading.isDone) {
            Debug.Log("Loading...");
            if (sceneLoading.progress == 0.9f)
                sceneLoading.allowSceneActivation = true;
            yield return null;
        }
    }

    #endregion

}