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

  public Wave CurrentWave { get { return level.Waves[currentWave]; } }
  private int currentWave = 0;

  private Level level;
  private IEnumerator newLevelRoutine;
  private IEnumerator restartRoutine;
  private bool gameOver = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    levelSpawner = GetComponent<LevelSpawner>();
    player = levelSpawner.Player();
    waveController = levelSpawner.WaveController();
  }

  void Update() {
    if (CurrentState != null)
      CurrentState.Play();
  }

  void OnEnable() {
    EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);
  }

  void OnDisable() {
    EventManager.StopListening<PlayerHitEvent>(OnPlayerHitEvent);
  }

  #endregion

  #region Event Behaviour

  void OnPlayerHitEvent(PlayerHitEvent playerHitEvent) {
    if (!gameOver) {
      restartRoutine = RestartRoutine();
      StartCoroutine(restartRoutine);
    }
  }

  #endregion

  #region Public Behaviour

  public void Play(Level level) {
    this.level = level;
    new Player();
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

  #endregion
	
}
