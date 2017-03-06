using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

  #region Fields

  private const string SCORE_TEXT = "SCORE";
  private static string[] EMOJIS = new string[] { "ʘ.ʘ", "╥_╥", "＾∇＾", "˘ڡ˘" };
  private const string COMBO_TEXT = "x";

  [SerializeField] private GameObject gameOverScreenPrefab;
  private GameObject gameOverScreen;

  private Canvas canvas;
  private Text scoreLabel;
  private Text emojiLabel;
  private Text comboLabel;
  private Animator comboLabelAnimator;

  private int score = 0;
  private int scoreTextNumber = 0;
  private int combo = 1;

  #endregion

  #region Mono Behaviour

  void Awake() {
    canvas = GetComponent<Canvas>();
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Sets world camera after instantiation
    canvas.sortingLayerName = "UI";
    scoreLabel = GetComponentsInChildren<Text>()[0];
    emojiLabel = GetComponentsInChildren<Text>()[1];
    comboLabel = GetComponentsInChildren<Text>()[2];
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
  }

  void OnDisable() {
    EventManager.StopListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StopListening<WrongGestureInput>(OnWrongGestureInput);
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

  #endregion

  #region Private Behaviour

  private IEnumerator EmojiRoutine(string emoji, float time) {
    emojiLabel.text = emoji;
    yield return new WaitForSeconds(time);
    emojiLabel.text = EMOJIS[0];
  }

  #endregion

}
