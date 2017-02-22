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

  private Level currentLevel;
  private List<GameObject> currentLevelObjects;

  #endregion

  #region Mono Behaviour

  void Awake() {
    currentLevel = Data.Level1;
    waveController = Instantiate(wavePrefab, transform).GetComponent<WaveController>();
    playerSpawner = Instantiate(playerPrefab, transform).GetComponent<PlayerSpawner>();
    hudController = Instantiate(hudPrefab, transform).GetComponent<HUDController>();
  }

  void OnDisable() {
    StopAllCoroutines();
  }

  #endregion

  #region Public Behaviour

  public void Level() {
    currentLevelObjects = new List<GameObject>();
	  GameObject player = playerSpawner.SpawnPlayer(currentLevel.PlayerPosition);
    hudController.gameObject.SetActive(true);
    EventManager.TriggerEvent(new NewLevelEvent());
    StartCoroutine(LevelRoutine(player));
  }

  #endregion

  #region Private Behaviour

  private IEnumerator LevelRoutine(GameObject player) {
    while (currentLevel.HasMoreWaves()) {
      waveController.Wave(currentLevel.CurrentWave(), player).ForEach(x => currentLevelObjects.Add(x));
      yield return new WaitForSeconds(1);
    }
  }

  #endregion
	
}
