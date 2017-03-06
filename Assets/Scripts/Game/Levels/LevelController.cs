using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using System.Linq;
using LevelStates;

public class LevelController : StateMachine {

  #region Fields

  [SerializeField] private GameObject wavePrefab;
  public WaveController WaveController { get { return waveController; } }
  private WaveController waveController;

  [SerializeField] private GameObject playerPrefab;
  public PlayerSpawner PlayerSpawner { get { return playerSpawner; } }
  private PlayerSpawner playerSpawner;

  [SerializeField] private GameObject backgroundPrefab;
  public BackgroundController BackgroundController { get { return backgroundController; } }
  private BackgroundController backgroundController;

  [SerializeField] private GameObject hudPrefab;
  public HUDController HUDController { get { return hudController; } }
  private HUDController hudController;

  public List<GameObject> CurrentLevelObjects { get { return currentLevelObjects; } set { currentLevelObjects = value; } } 
  private List<GameObject> currentLevelObjects = new List<GameObject>();

  public GameObject Player { get { return player; } set { player = value; } } 
  private GameObject player;

  private IEnumerator newLevelRoutine;
  private IEnumerator restartRoutine;
  private bool gameOver = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    waveController = Instantiate(wavePrefab, transform).GetComponent<WaveController>();
    playerSpawner = Instantiate(playerPrefab, transform).GetComponent<PlayerSpawner>();
    backgroundController = Instantiate(backgroundPrefab, transform).GetComponent<BackgroundController>();
    hudController = Instantiate(hudPrefab, transform).GetComponent<HUDController>();
    player = PlayerSpawner.SpawnPlayer(currentLevelObjects);
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

  public void Play() {
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
    yield return new WaitForSeconds(0.2f);
    ChangeState<PlayState>();
  }

  private IEnumerator RestartRoutine() {
    yield return new WaitForSeconds(0.4f);
    ChangeState<RestartState>();
    yield return new WaitForSeconds(0.2f);
    ChangeState<PlayState>();
  }

  #endregion
	
}
