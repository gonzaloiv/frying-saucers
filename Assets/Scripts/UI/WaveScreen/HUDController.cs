using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

  #region Fields

  private const string SCORE_TEXT = "SCORE";
  private const string LIVES_TEXT = "LIVES";
  private static string[] EMOJIS = new string[] { "ʘ.ʘ", "╥_╥", "＾∇＾", "˘ڡ˘" };

  [SerializeField] private GameObject gameOverScreenPrefab;
  private GameObject gameOverScreen;

  private Canvas canvas;
  private Text scoreLabel;
  private Text emojiLabel;
  private int scoreTextNumber;
  private Text livesLabel;

  #endregion

  #region Mono Behaviour

  void Awake() {
    canvas = GetComponent<Canvas>();
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Sets world camera after instantiation
    canvas.sortingLayerName = SortingLayer.UI.ToString();
    scoreLabel = GetComponentsInChildren<Text>()[0];
    emojiLabel = GetComponentsInChildren<Text>()[1];
    livesLabel = GetComponentsInChildren<Text>()[2];
  }

  void Update() {
    if (scoreTextNumber < Player.Score)
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

    if (Player.Combo >= 5)
      StartCoroutine(EmojiRoutine(EMOJIS[3], 3));
    else
      StartCoroutine(EmojiRoutine(EMOJIS[2], 1));

    Player.Combo++;
    Player.Score += (int) Mathf.Ceil(Config.ENEMY_SCORE * Player.Combo * GestureMultiplier(rightGestureInput.GestureInput.Time));

  }

  void OnWrongGestureInput(WrongGestureInput wrongGestureInput) {
    Player.Combo = 1;
    StartCoroutine(EmojiRoutine(EMOJIS[1], 1));
  }

  void OnPlayerHitEvent(PlayerHitEvent playerHitEvent) {
    Player.Lives--;
    if (Player.Lives < 1)
      EventManager.TriggerEvent(new GameOverEvent(Player.Score));
    livesLabel.gameObject.GetComponent<Animator>().Play("FadeIn");
    SetLives();
  }

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
    StopAllCoroutines();
    StartCoroutine(EmojiRoutine(EMOJIS[1], 4));
  }

  #endregion

  #region Private Behaviour

  public void Initialize() {
    scoreTextNumber = Player.Score;
    scoreLabel.text = SCORE_TEXT + "\n" + scoreTextNumber;
    scoreLabel.gameObject.GetComponent<Animator>().Play("FadeIn");
    SetLives();
    livesLabel.gameObject.GetComponent<Animator>().Play("FadeIn");
  }

  #endregion

  #region Public Behaviour

  private IEnumerator EmojiRoutine(string emoji, float time) {
    emojiLabel.text = emoji;
    yield return new WaitForSeconds(time);
    emojiLabel.text = EMOJIS[0];
  }

  private float GestureMultiplier(GestureTime gestureTime) {
    switch (gestureTime) {
      case GestureTime.Perfect:
        return 2;
      case GestureTime.TooFast:
        return .5f;
      case GestureTime.TooSlow:
        return .5f;
      default:
        return 1;
    }
  }

  #endregion

  #region Private Behaviour

  private void SetLives() {
    if (Player.Lives > 100) // Tutorial settings
      livesLabel.text = LIVES_TEXT + "\n∞";
    else
      livesLabel.text = LIVES_TEXT + "\n" + Player.Lives;
  }

  #endregion

}
