using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;
using System.Linq;

public class WaveController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject enemyPrefab;
  private EnemySpawner enemySpawner;

  [SerializeField] private GameObject enemyTypeLabelPrefab;
  private EnemyTypeLabelSpawner enemyTypeLabelSpawner;

  public GameObject[] CurrentLevelObjects { get { return currentLevelObjects; } }
  private GameObject[] currentLevelObjects = new GameObject[Config.ENEMY_WAVE_AMOUNT];

  private Wave wave;
  private GameObject player;
  bool enemyHit = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    enemySpawner = Instantiate(enemyPrefab, transform).GetComponent<EnemySpawner>();
    enemyTypeLabelSpawner = Instantiate(enemyTypeLabelPrefab, transform).GetComponent<EnemyTypeLabelSpawner>();
  }

  void Update() {
    if(enemyHit)
      FillWave();
  }

  void OnEnable() {
    EventManager.StartListening<EnemyHitEvent>(OnEnemyHitEvent);
    EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
  }

  void OnDisable() {
    EventManager.StopListening<EnemyHitEvent>(OnEnemyHitEvent);
    EventManager.StopListening<PlayerHitEvent>(OnPlayerHitEvent);
    EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
  }

  #endregion

  #region Event Behaviour

  void OnEnemyHitEvent(EnemyHitEvent enemyHitEvent) {
    enemyHit = true;
  }

  void OnPlayerHitEvent(PlayerHitEvent playerHitEvent) {
    enemyTypeLabelSpawner.ShowGestures(2);
  }

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    enemyTypeLabelSpawner.HideGestures();
  }


  #endregion

  #region Public Behaviour

  public void Wave(GameObject player) {
    this.wave = new Wave(3);
    this.player = player;
    this.currentLevelObjects = currentLevelObjects;
    enemyTypeLabelSpawner.Reset();
    for (int i = 0; i < Config.ENEMY_WAVE_AMOUNT; i++) {
      currentLevelObjects[i] = enemySpawner.SpawnEnemy(wave.Enemies[i], player);
      enemyTypeLabelSpawner.SetGesture(i, wave.Enemies[i]);
    }
    enemyTypeLabelSpawner.ShowGestures(2);
  }

  public void Reset() {
    for(int i = 0; i < currentLevelObjects.Length; i++)
      if(currentLevelObjects[i] != null)
        currentLevelObjects[i].SetActive(false);
    currentLevelObjects = new GameObject[Config.ENEMY_WAVE_AMOUNT];
  }

  #endregion

  #region Private Behaviour

  private void FillWave() {
    for (int i = 0; i < currentLevelObjects.Count(); i++) {
      if (!currentLevelObjects[i].activeInHierarchy) {
        AddEnemy(i);
        enemyHit = false;
      }
    }
  }

  private void AddEnemy(int index) {
    Enemy enemy = wave.Enemies[index];
    enemy.RandomType();
    currentLevelObjects[index] = enemySpawner.SpawnEnemy(enemy, player);
    enemyTypeLabelSpawner.SetGesture(index, enemy);
    enemyTypeLabelSpawner.ShowGestures(1);
  }

  #endregion

}
