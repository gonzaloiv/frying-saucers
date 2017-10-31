using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : StateMachine {

    #region Fields

    [SerializeField] private WaveController waveController;
    [SerializeField] private GameObject inputManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private LevelScreenController levelScreenController;
    [SerializeField] private GameObject pauseScreen;

    public WaveController WaveController { get { return waveController; } }
    public GameObject InputManagerObject { get { return inputManager; } } // Temporarily...
    public PlayerController PlayerController { get { return playerController; } }
    public LevelScreenController LevelScreenController { get { return levelScreenController; } }
    public GameObject PauseScreen { get { return pauseScreen; } }

    public LevelData CurrentLevelData { get { return currentLevelData; } }
    public WaveData CurrentWaveData { get { return currentLevelData.WavesData[currentWaveIndex]; } }

    private PlayerController playerController;
    private LevelData currentLevelData;
    private int currentWaveIndex = 0;

    #endregion

    #region Events

    public delegate void NewLevelEventHandler ();
    public static event NewLevelEventHandler NewLevelEvent = delegate {};

    public delegate void LevelEndEventHandler ();
    public static event LevelEndEventHandler LevelEndEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        playerController = Instantiate(playerPrefab, transform).GetComponent<PlayerController>();
        playerController.gameObject.SetActive(false);
        waveController.Init(playerController.gameObject);
        pauseScreen.SetActive(false);
    }

    void OnEnable() {
        InputManager.EscapeInputEvent += ToPauseState;
    }

    void OnDisable() {
        InputManager.EscapeInputEvent -= ToPauseState;
    }

    #endregion

    #region Public Behaviour

    public void InitLevel (LevelData levelData) {
        currentWaveIndex = 0;
        currentLevelData = levelData;
        new Player(levelData.PlayerInitialLives);
        ToNewLevelState();
    }

    public void ToNewLevelState () {
        NewLevelEvent.Invoke();
        ChangeState<LevelStates.LevelStartState>();
    }

    public void ToNewWaveState () {
        if (currentWaveIndex < currentLevelData.WavesData.Count()) {
            currentWaveIndex++;
            ChangeState<LevelStates.WaveStartState>();
        } else {
            LevelEndEvent.Invoke();
            ChangeState<LevelStates.StopState>();
        }
    }

    public void ToWaveState () {
        ChangeState<LevelStates.WaveState>();
    }

    public void ToRestartState () {
        ChangeState<LevelStates.WaveRestartState>();
    }

    public void ToPauseState () {
        if (Time.timeScale != 0) {
            ChangeState<LevelStates.PauseState>();
        } else {
            ChangeState<LevelStates.WaveState>();
        }
    }

    public void ToStopState () {
        ChangeState<LevelStates.StopState>();
    }

    #endregion

}