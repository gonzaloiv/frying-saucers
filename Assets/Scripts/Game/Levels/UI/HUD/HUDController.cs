using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

  #region Fields

  private const string SCORE_TEXT = "SCORE";
  private static string[] EMOJIS = new string[] { "ʘ.ʘ", "╥_╥", "＾∇＾", "˘ڡ˘" };
  private const string COMBO_TEXT = "x";
  private const string LIVES_TEXT = "LIVES";

  [SerializeField] private GameObject gameOverScreenPrefab;
  private GameObject gameOverScreen;

  private Canvas canvas;
  private Text scoreLabel;
  private Text comboLabel;
  private Animator comboLabelAnimator;
  private Text emojiLabel;

  private int score;
  private int scoreTextNumber;
  private int combo;
  private Text livesLabel;
  private int lives;


  #endregion

  #region Mono Behaviour

  void Awake() {
    canvas = GetComponent<Canvas>();
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Sets world camera after instantiation
    canvas.sortingLayerName = "UI";
    scoreLabel = GetComponentsInChildren<Text>()[0];
    emojiLabel = GetComponentsInChildren<Text>()[1];
    comboLabel = GetComponentsInChildren<Text>()[2];
    livesLabel = GetComponentsInChildren<Text>()[3];
    comboLabelAnimator = GetComponentInChildren<Animator>();
  }

  void Update() {
    if (scoreTextNumber < score)
      scoreTextNumber++;
    scoreLabel.text = SCORE_TEXT + "\n" + scoreTextNumber;
    comboLabel.text = COMBO_TEXT + combo;
  }

  void OnEnable() {
    EventManager.StartListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StartListening<WrongGestureInput>(OnWrongGestureInput);
    EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);
  }

  void OnDisable() {
    EventManager.StopListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StopListening<WrongGestureInput>(OnWrongGestureInput);
    EventManager.StopListening<PlayerHitEvent>(OnPlayerHitEvent);
  }

  #endregion

  #region Event Behaviour

  void OnRightGestureInput(RightGestureInput rightGestureInput) {

    if (combo >= 5)
      StartCoroutine(EmojiRoutine(EMOJIS[3], 3));
    else
      StartCoroutine(EmojiRoutine(EMOJIS[2], 1));

    score += Config.ENEMY_SCORE * combo;

    combo++;
    comboLabelAnimator.Play("Spawn");

  }

  void OnWrongGestureInput(WrongGestureInput wrongGestureInput) {
    combo = 1;
    StartCoroutine(EmojiRoutine(EMOJIS[1], 1));
  }

  void OnPlayerHitEvent(PlayerHitEvent playerHitEvent) {
    lives--;
    if(lives < 1) 
      EventManager.TriggerEvent(new GameOverEvent());
    else
      livesLabel.text = LIVES_TEXT + "\n" + lives;
  }

  #endregion

  #region Public Behaviour

  public void Initialize() {
    score = 0;
    scoreTextNumber = 0;
    combo = 1;
    lives = Config.PLAYER_INITIAL_LIVES;
    livesLabel.text = LIVES_TEXT + "\n" + lives;
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
