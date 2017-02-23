using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject wavePrefab;
  private WaveController waveController;

  [SerializeField] private GameObject playerPrefab;
  private PlayerSpawner playerSpawner;

	[SerializeField] private GameObject hudPrefab;
  private HUDController hudController;

  [SerializeField] private GameObject backgroundPrefab;
  private BackgroundController backgroundController;

  private Level currentLevel;
  private List<GameObject> currentLevelObjects;

  #endregion

  #region Mono Behaviour

  void Awake() {
    currentLevel = Data.Level1;
    waveController = Instantiate(wavePrefab, transform).GetComponent<WaveController>();
    playerSpawner = Instantiate(playerPrefab, transform).GetComponent<PlayerSpawner>();
    hudController = Instantiate(hudPrefab, transform).GetComponent<HUDController>();
    backgroundController = Instantiate(backgroundPrefab, transform).GetComponent<BackgroundController>();
  }

  void OnDisable() {
    StopAllCoroutines();
  }

  #endregion

  #region Public Behaviour

  public void Level() {

    // GAME OBJECTS
    currentLevelObjects = new List<GameObject>();
	  GameObject player = playerSpawner.SpawnPlayer(currentLevel.PlayerPosition, currentLevelObjects);
    
    // LEVEL OBJECTS
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
