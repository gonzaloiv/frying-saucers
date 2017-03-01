using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using System.Linq;

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
  GameObject player;

  bool waveRoutine = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    currentLevel = new Level(1, 4);
    waveController = Instantiate(wavePrefab, transform).GetComponent<WaveController>();
    playerSpawner = Instantiate(playerPrefab, transform).GetComponent<PlayerSpawner>();
    backgroundController = Instantiate(backgroundPrefab, transform).GetComponent<BackgroundController>();
    hudController = Instantiate(hudPrefab, transform).GetComponent<HUDController>();
  }

  void Update() {
    currentLevelObjects.RemoveAll(x => !x.activeInHierarchy);
    if (currentLevelObjects.Count() == 0 && !waveRoutine) {
      StopCoroutine(ShootingRoutine());
      StartCoroutine(WaveRoutine());      
    }
  }

  void OnEnable() {
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
  }

  void OnDisable() {
    EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
    StopAllCoroutines();
  }

  #endregion

  #region Even Behaviour

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    StopAllCoroutines();
  }

  #endregion

  #region Public Behaviour

  public void Level() {

    // LEVEL GAME OBJECTS
    currentLevelObjects = new List<GameObject>();
	  player = playerSpawner.SpawnPlayer(currentLevelObjects);
    
    // LEVEL UI & ENVIRONMENT
    backgroundController.NewLevel();
    hudController.gameObject.SetActive(true);

    StartCoroutine(ShootingRoutine());

  }

  #endregion

  #region Private Behaviour

  private IEnumerator WaveRoutine() {
    waveRoutine = true;
    yield return new WaitForSeconds(0.5f);
    waveController.Wave(player).ForEach(x => currentLevelObjects.Add(x));
    waveRoutine = false;
  }

  private IEnumerator ShootingRoutine() {
    while(gameObject.activeSelf){
      yield return new WaitForSeconds(3);
      if(!waveRoutine)
        currentLevelObjects[Random.Range(0, currentLevelObjects.Count())].GetComponent<IEnemyBehaviour>().Play();
    }
  }

  #endregion
	
}
