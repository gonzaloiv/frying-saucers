using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class HUDController : MonoBehaviour {

  #region Fields

  private const string SCORE_TEXT = "SCORE";
  private static string[] EMOJIS = new string[] { "ʘ.ʘ", "╥_╥", "＾∇＾", "˘ڡ˘" };
  private const string LIVES_TEXT = "LIVES";

  [SerializeField] private GameObject gameOverScreenPrefab;
  private GameObject gameOverScreen;

  private Canvas canvas;
  private Text scoreLabel;

  private Text emojiLabel;

  public static int Lives { get { return lives; } }
  private static int lives;

  private int scoreTextNumber;
  private Text livesLabel;

  #endregion

  #region Mono Behaviour

  void Awake() {
    canvas = GetComponent<Canvas>();
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Sets world camera after instantiation
    canvas.sortingLayerName = "UI";
    scoreLabel = GetComponentsInChildren<Text>()[0];
    emojiLabel = GetComponentsInChildren<Text>()[1];
    livesLabel = GetComponentsInChildren<Text>()[2];
  }

  void Update() {
    if (scoreTextNumber < Level.Score)
      scoreTextNumber++;
    scoreLabel.text = SCORE_TEXT + "\n" + scoreTextNumber;
  }

  void OnEnable() {
    EventManager.StartListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StartListening<WrongGestureInput>(OnWrongGestureInput);
    EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
  }

  void OnDisable() {
    EventManager.StopListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StopListening<WrongGestureInput>(OnWrongGestureInput);
    EventManager.StopListening<PlayerHitEvent>(OnPlayerHitEvent);
    EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
  }

  #endregion

  #region Event Behaviour

  void OnRightGestureInput(RightGestureInput rightGestureInput) {
    if (Level.Combo >= 5)
      StartCoroutine(EmojiRoutine(EMOJIS[3], 3));
    else
      StartCoroutine(EmojiRoutine(EMOJIS[2], 1));
    Level.Combo++;
    Level.Score += Config.ENEMY_SCORE * Level.Combo;
  }

  void OnWrongGestureInput(WrongGestureInput wrongGestureInput) {
    Level.Combo = 1;
    StartCoroutine(EmojiRoutine(EMOJIS[1], 1));
  }

  void OnPlayerHitEvent(PlayerHitEvent playerHitEvent) {
    lives--;
    if(lives < 1) 
      EventManager.TriggerEvent(new GameOverEvent(Level.Score));
    livesLabel.gameObject.GetComponent<Animator>().Play("FadeIn");
    livesLabel.text = LIVES_TEXT + "\n" + lives;
  }

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    StopAllCoroutines();
    StartCoroutine(EmojiRoutine(EMOJIS[1], 4));
  }

  #endregion

  #region Public Behaviour

  public void Initialize() {
    scoreTextNumber = Level.Score;
    scoreLabel.text = SCORE_TEXT + "\n" + scoreTextNumber;
    lives = Level.Lives;
    livesLabel.text = LIVES_TEXT + "\n" + lives;
    livesLabel.gameObject.GetComponent<Animator>().Play("FadeIn");
  }

  #endregion

  #region Private Behaviour

  private IEnumerator EmojiRoutine(string emoji, float time) {
    emojiLabel.text = emoji;
    yield return new WaitForSeconds(time);
    emojiLabel.text = EMOJIS[0];
  }

  #endregion

}
