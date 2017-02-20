using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject wavePrefab;
  private WaveController waveController;

  private Level currentLevel;

  #endregion

  #region Mono Behaviour

  void Awake() {
    currentLevel = Data.Level1;
    waveController = Instantiate(wavePrefab, transform).GetComponent<WaveController>(); 
  }

  void OnDisable() {
    StopAllCoroutines();
  }

  #endregion

  #region Public Behaviour

  public void Level() {
    StartCoroutine(LevelRoutine());
  }

  #endregion

  #region Private Behaviour

  private IEnumerator LevelRoutine() {
    while (currentLevel.HasMoreWaves()) {
      waveController.Wave(currentLevel.CurrentWave());
      yield return new WaitForSeconds(1);
    }
  }

  #endregion
	
}
