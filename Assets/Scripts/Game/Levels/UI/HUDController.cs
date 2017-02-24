using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

  #region Fields

  private const string LEVEL_TEXT = "LEVEL ";
  private const string SCORE_TEXT = "AUDIENCE ";

  private Canvas canvas;

  [SerializeField] private GameObject scopePrefab;
  private GameObject scope;

  [SerializeField] private GameObject gameOverScreenPrefab;
  private GameObject gameOverScreen;

  private Text levelLabel;
  private Text scoreLabel;

  private int level = 0;
  private int score = 0;
  private int scoreTextNumber = 0;

  #endregion

  #region Mono Behaviour

  void Awake() {
    canvas = GetComponent<Canvas>();
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Sets world camera after instantiation
    canvas.sortingLayerName = "UI";
    levelLabel = GetComponentsInChildren<Text>()[1];
    scoreLabel = GetComponentsInChildren<Text>()[0];
    scope = Instantiate(scopePrefab, transform);
  }
 
  void Update() {
    if(scoreTextNumber < score) {
      scoreTextNumber++;
      scoreLabel.text = SCORE_TEXT + scoreTextNumber;
    }
  }

  void OnEnable() {
    EventManager.StartListening<ClickInput>(OnClickInput);
    EventManager.StartListening<EnemyHitEvent>(OnEnemyHitEvent);
  }
  

  void OnDisable() {
    EventManager.StopListening<ClickInput>(OnClickInput);
    EventManager.StopListening<EnemyHitEvent>(OnEnemyHitEvent);
  }

  #endregion

  #region Event Behaviour

  void OnClickInput(ClickInput clickInput) {
    scope.transform.position = clickInput.Position;
    scope.GetComponent<SpriteRenderer>().enabled = true;
  }

  void OnEnemyHitEvent(EnemyHitEvent enemyHitEvent) {
    score += enemyHitEvent.Score;
  }

  #endregion

  #region Public Behaviour

  public void NewLevel() {
    level++;
    levelLabel.text = LEVEL_TEXT + level;
  }

  #endregion
	
}
