using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LevelStates;

public class LevelController : StateMachine {

    #region Fields

    [SerializeField] private WaveController waveController;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private LevelScreenController levelScreenController;
    [SerializeField] private GameObject pauseScreen;

    public WaveController WaveController { get { return waveController; } }
    public PlayerController PlayerController { get { return playerController; } }
    public LevelScreenController LevelScreenController { get { return levelScreenController; } }
    public GameObject PauseScreen { get { return pauseScreen; } }

    public LevelData CurrentLevelData { get { return currentLevelData; } }
    public WaveData CurrentWaveData { get { return currentLevelData.WavesData[currentWaveIndex]; } }

    private PlayerController playerController;
    private LevelData currentLevelData;
    private int currentWaveIndex;

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

    #endregion

    #region Public Behaviour

    public void ToInitState (LevelData levelData) {
        currentWaveIndex = -1;
        currentLevelData = levelData;
        NewLevelEvent.Invoke();
        ChangeState<InitState>();
    }

    public void ToWaveStartState () {
        currentWaveIndex++;
        if (currentWaveIndex >= currentLevelData.WavesData.Count) // Keep playing the last wave until the player loses
            currentWaveIndex--;
        ChangeState<WaveStartState>();
    }

    public void ToWaveState () {
        ChangeState<WaveState>();
    }

    public void ToPauseState () {
        ChangeState<PauseState>();
    }

    #endregion

}