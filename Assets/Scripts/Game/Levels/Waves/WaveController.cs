using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using System.Linq;

public class WaveController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject enemyPrefab;
  private EnemySpawner enemySpawner;

  private Wave wave;
  private GameObject player;
  private List<GameObject> currentLevelObjects;

  #endregion

  #region Mono Behaviour

  void Awake() {
    enemySpawner = Instantiate(enemyPrefab, transform).GetComponent<EnemySpawner>();
  }

  #endregion

  #region Public Behaviour

  public void Wave(GameObject player, List<GameObject> currentLevelObjects) {
    this.wave = new Wave(3);
    this.player = player;
    this.currentLevelObjects = currentLevelObjects;

    for(int i = 0; i < Config.ENEMY_WAVE_AMOUNT; i++)
      AddEnemy(i);
  }

  public void FillWave() {
    for(int i = 0; i < currentLevelObjects.Count; i++) {
      if(!currentLevelObjects[i].activeInHierarchy)
        AddEnemy(i);
    }
  }

  #endregion

  #region Private Behaviour

  private void AddEnemy(int index) {
    wave.Enemies[index].RandomType();
    currentLevelObjects.Add(enemySpawner.SpawnEnemy(wave.Enemies[index], player));
  }

  #endregion

}
