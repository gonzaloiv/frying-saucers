using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

  #region Fields

  private const string levelText = "LEVEL ";
  private const string scoreTextLabel = "AUDIENCE ";
  private int scoreTextNumber = 0;

  [SerializeField] private GameObject scopePrefab;
  private GameObject scope;

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
    scope = Instantiate(scopePrefab, transform);
    scope.GetComponent<SpriteRenderer>().enabled = false;
  }
 
  void Update() {
    if(scoreTextNumber < score) {
      scoreTextNumber++;
      scoreLabel.text = scoreTextLabel + scoreTextNumber;
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
    levelLabel.text = levelText + level;
  }

  #endregion
	
}
