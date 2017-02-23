using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

  #region Fields

  private const string levelText = "LEVEL ";
  private const string scoreTextLabel = "SCORE ";
  private int scoreTextNumber = 0;

  private Canvas canvas;
  private Text levelLabel;
  private Text scoreLabel;

  private int level = 0;
  private int score = 0;

  #endregion

  #region Mono Behaviour

  void Awake() {
	canvas = GetComponent<Canvas>();
    levelLabel = GetComponentsInChildren<Text>()[1];
    scoreLabel = GetComponentsInChildren<Text>()[0];
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Sets world camera after instantiation
  }
 
  void OnEnable() {
    EventManager.StartListening<EnemyHitEvent>(OnEnemyHitEvent);
  }
  
  void Update() {
    if(scoreTextNumber < score) {
      scoreTextNumber++;
      scoreLabel.text = scoreTextLabel + scoreTextNumber;
   }
  }

  void OnDisable() {
    EventManager.StopListening<EnemyHitEvent>(OnEnemyHitEvent);
  }

  #endregion

  #region Event Behaviour

  void OnEnemyHitEvent(EnemyHitEvent enemyHitEvent) {
    score += enemyHitEvent.Score;
  }

  #endregion

  #region Public Behaviour

  public void NewLevel() {
    level++;
    levelLabel.text = levelText + level;
  }

  #endregion
	
}
