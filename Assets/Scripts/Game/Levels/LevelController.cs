using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class LevelController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject wavePrefab;
  private WaveController waveController;

  [SerializeField] private GameObject playerPrefab;
  private PlayerSpawner playerSpawner;

  [SerializeField] private GameObject backgroundPrefab;
  private BackgroundController backgroundController;

  [SerializeField] private GameObject hudPrefab;
  private HUDController hudController;

  private Level currentLevel;
  private List<GameObject> currentLevelObjects = new List<GameObject>();

  #endregion

  #region Mono Behaviour

  void Awake() {
    currentLevel = Data.Level1;
    waveController = Instantiate(wavePrefab, transform).GetComponent<WaveController>();
    playerSpawner = Instantiate(playerPrefab, transform).GetComponent<PlayerSpawner>();
    backgroundController = Instantiate(backgroundPrefab, transform).GetComponent<BackgroundController>();
    hudController = Instantiate(hudPrefab, transform).GetComponent<HUDController>();
  }

  void OnEnable() {
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
  }

  void OnDisable() {
    EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
  }

  #endregion

  #region Even Behaviour

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    StopAllCoroutines();
  }

  #endregion

  #region Public Behaviour

  public void Level() {

    // LEVEL RESET
    if(currentLevelObjects.Count != 0) 
      currentLevelObjects.ForEach(x => x.SetActive(false));

    // LEVEL GAME OBJECTS
    currentLevelObjects = new List<GameObject>();
	  GameObject player = playerSpawner.SpawnPlayer(currentLevel.PlayerPosition, currentLevelObjects);
    currentLevelObjects.Add(player);
    
    // LEVEL UI & ENVIRONMENT
    backgroundController.NewLevel();
    hudController.gameObject.SetActive(true);
    hudController.NewLevel();

    StartCoroutine(LevelRoutine(player));

  }

  #endregion

  #region Private Behaviour

  private IEnumerator LevelRoutine(GameObject player) {
    while (currentLevel.HasMoreWaves()) {
      yield return new WaitForSeconds(1);
      waveController.Wave(currentLevel.CurrentWave(), player).ForEach(x => currentLevelObjects.Add(x));
    }
  }

  #endregion
	
}
