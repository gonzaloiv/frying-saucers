using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

  #region Fields

  private const string SCORE_TEXT = "SCORE";
  private static string[] EMOJIS = new string[] { "ʘ.ʘ", "╥_╥", "ʘ‿ʘ" };

  private Canvas canvas;

  [SerializeField] private GameObject gameOverScreenPrefab;
  private GameObject gameOverScreen;

  private Text scoreLabel;
  private Text emojiLabel;

  private int score = 0;
  private int scoreTextNumber = 0;

  #endregion

  #region Mono Behaviour

  void Awake() {
    canvas = GetComponent<Canvas>();
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Sets world camera after instantiation
    canvas.sortingLayerName = "UI";
    scoreLabel = GetComponentsInChildren<Text>()[0];
    emojiLabel = GetComponentsInChildren<Text>()[1];
  }
 
  void Update() {
    if(scoreTextNumber < score) {
      scoreTextNumber++;
      scoreLabel.text = SCORE_TEXT + "\n" + scoreTextNumber;
    }
  }

  void OnEnable() {
    EventManager.StartListening<EnemyHitEvent>(OnEnemyHitEvent);
    EventManager.StartListening<WrongGestureInput>(OnWrongGestureInput);
  }
  

  void OnDisable() {
    EventManager.StopListening<EnemyHitEvent>(OnEnemyHitEvent);
    EventManager.StopListening<WrongGestureInput>(OnWrongGestureInput);
  }

  #endregion

  #region Event Behaviour

  void OnEnemyHitEvent(EnemyHitEvent enemyHitEvent) {
    score += enemyHitEvent.Score;
    StartCoroutine(EmojiRoutine(EMOJIS[2]));
  }

  void OnWrongGestureInput(WrongGestureInput wrongGestureInput) {
    StartCoroutine(EmojiRoutine(EMOJIS[1]));
  }

  #endregion

  #region Private Behaviour

  private IEnumerator EmojiRoutine(string emoji) {
    emojiLabel.text = emoji;
    yield return new WaitForSeconds(1);
    emojiLabel.text = EMOJIS[0];
  }

  #endregion

}
