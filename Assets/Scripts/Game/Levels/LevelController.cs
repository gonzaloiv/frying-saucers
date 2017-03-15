using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Models;
using LevelStates;

public class LevelController : StateMachine {

  #region Fields

  private LevelSpawner levelSpawner;

  public WaveController WaveController { get { return waveController; } }
  private WaveController waveController;

  public GameObject Player { get { return player; } set { player = value; } } 
  private GameObject player;

  public HUDController HUDController { get { return hudController; } set { hudController = value; } }
  private HUDController hudController;

  public Wave CurrentWave { get { return level.Waves[currentWave]; } }
  private int currentWave = 0;

  private Level level;

  private IEnumerator newLevelRoutine;
  private IEnumerator restartRoutine;
  private IEnumerator newWaveRoutine;
  private bool gameOver = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    levelSpawner = GetComponent<LevelSpawner>();
    player = levelSpawner.Player();
    waveController = GetComponentInChildren<WaveController>();
    hudController = levelSpawner.HUDController();
  }

  void Update() {
    if (CurrentState != null)
      CurrentState.Play();
  }

  void OnEnable() {
    EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);
    EventManager.StartListening<WaveEndEvent>(OnWaveEndEvent);
  }

  void OnDisable() {
    EventManager.StopListening<PlayerHitEvent>(OnPlayerHitEvent);
    EventManager.StopListening<WaveEndEvent>(OnWaveEndEvent);
  }

  #endregion

  #region Event Behaviour

  void OnPlayerHitEvent(PlayerHitEvent playerHitEvent) {
    if (!gameOver) {
      restartRoutine = RestartRoutine();
      StartCoroutine(restartRoutine);
    }
  }

  void OnWaveEndEvent(WaveEndEvent waveEndEvent) {
    currentWave++;
    if(currentWave < level.Waves.Count()) {
      newWaveRoutine = NewWaveRoutine();
      StartCoroutine(newWaveRoutine);
    } else {
      EventManager.TriggerEvent(new LevelEndEvent());
    }
  }

  #endregion

  #region Public Behaviour

  public void Play(Level level) {
    this.level = level;
    gameOver = false;
    newLevelRoutine = NewLevelRoutine();
    StartCoroutine(newLevelRoutine);
  }

  public void Stop() {
    gameOver = true;
    ChangeState<StopState>();
  }

  #endregion

  #region Private Behaviour

  private IEnumerator NewLevelRoutine() {
    yield return new WaitForSeconds(0.4f);
    ChangeState<NewLevelState>();
    yield return new WaitForSeconds(1f);
    ChangeState<PlayState>();
  }

  private IEnumerator RestartRoutine() {
    yield return new WaitForSeconds(0.4f);
    ChangeState<RestartState>();
    yield return new WaitForSeconds(1f);
    ChangeState<PlayState>();
  }

  private IEnumerator NewWaveRoutine() {
    yield return new WaitForSeconds(0.4f);
    ChangeState<NewWaveState>();
    yield return new WaitForSeconds(1f);
    ChangeState<PlayState>();
  }

  #endregion
	
}
