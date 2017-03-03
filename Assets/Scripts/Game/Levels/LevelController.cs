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

  #endregion

  #region Mono Behaviour

  void Awake() {
    waveController = Instantiate(wavePrefab, transform).GetComponent<WaveController>();
    playerSpawner = Instantiate(playerPrefab, transform).GetComponent<PlayerSpawner>();
    backgroundController = Instantiate(backgroundPrefab, transform).GetComponent<BackgroundController>();
    hudController = Instantiate(hudPrefab, transform).GetComponent<HUDController>();
  }

  void Update() {
    currentLevelObjects.RemoveAll(x => !x.activeInHierarchy);
    if (currentLevelObjects.Count() == 0 && CurrentState is PlayState)
      StartCoroutine(LevelRoutine());
  }

  #endregion

  #region Public Behaviour

  public void Level() {
    player = playerSpawner.SpawnPlayer(currentLevelObjects);
    ChangeState<NewLevelState>();
    StartCoroutine(LevelRoutine());
  }

  #endregion

  #region Private Behaviour

  private IEnumerator LevelRoutine() {
    ChangeState<NewWaveState>();
    yield return new WaitForSeconds(0.2f);
    ChangeState<PlayState>();
  }

  #endregion
	
}
