using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreenController : MonoBehaviour {

    #region Fields

    private const string SCORE_TEXT = "SCORE";
    private const string LIVES_TEXT = "LIVES";
    private static string[] EMOJIS = new string[] { "ʘ.ʘ", "╥_╥", "＾∇＾", "˘ڡ˘" };

    private Text scoreLabel;
    private Text emojiLabel;
    private int scoreTextNumber;
    private Text livesLabel;

    #endregion

    #region Events

    public delegate void GameOverEventHandler (GameOverEventArgs gameOverEventArgs);
    public static event GameOverEventHandler GameOverEvent;

    #endregion

    #region Mono Behaviour

    void Awake () {
        scoreLabel = GetComponentsInChildren<Text>()[0];
        emojiLabel = GetComponentsInChildren<Text>()[1];
        livesLabel = GetComponentsInChildren<Text>()[2];
    }

    void Update () {
        if (scoreTextNumber < Player.Score)
            scoreTextNumber++;
        scoreLabel.text = SCORE_TEXT + "\n" + scoreTextNumber;
    }

    void OnEnable () {
        EnemyBehaviour.RightGestureInputEvent += OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent += OnWrongGestureInputEvent;
        PlayerController.PlayerHitEvent += OnPlayerHitEvent;
        LevelScreenController.GameOverEvent += OnGameOverEvent;
    }

    void OnDisable () {
        EnemyBehaviour.RightGestureInputEvent -= OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent -= OnWrongGestureInputEvent;
        PlayerController.PlayerHitEvent -= OnPlayerHitEvent;
        LevelScreenController.GameOverEvent -= OnGameOverEvent;
    }

    #endregion

    #region Public Behaviour

    public void Init () {
        scoreTextNumber = Player.Score;
        scoreLabel.text = SCORE_TEXT + "\n" + scoreTextNumber;
        scoreLabel.gameObject.GetComponent<Animator>().Play("FadeIn");
        SetLives();
        livesLabel.gameObject.GetComponent<Animator>().Play("FadeIn");
    }

    public void OnRightGestureInputEvent (RightGestureInputEventArgs rightGestureInputEventArgs) {
        IEnumerator emojiRoutine = Player.Combo >= 5 ? EmojiRoutine(EMOJIS[3], 3) : EmojiRoutine(EMOJIS[2], 1);
        StartCoroutine(emojiRoutine);
        Player.Combo++;
        Player.Score += (int) Mathf.Ceil(GameConfig.EnemyScore * Player.Combo * GestureMultiplier(rightGestureInputEventArgs.GestureInputEventArgs.Time));
    }

    public void OnWrongGestureInputEvent (WrongGestureInputEventArgs wrongGestureInputArgs) {
        Player.Combo = 1;
        StartCoroutine(EmojiRoutine(EMOJIS[1], 1));
    }

    public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
        Player.Lives--;
        if (Player.Lives < 1)
            GameOverEvent.Invoke(new GameOverEventArgs(Player.Score));
        livesLabel.gameObject.GetComponent<Animator>().Play("FadeIn");
        SetLives();
    }

    public void OnGameOverEvent (GameOverEventArgs gameOverEventArgs) {
        StopAllCoroutines();
        StartCoroutine(EmojiRoutine(EMOJIS[1], 4));
    }

    #endregion

    #region Private Behaviour

    private IEnumerator EmojiRoutine (string emoji, float time) {
        emojiLabel.text = emoji;
        yield return new WaitForSeconds(time);
        emojiLabel.text = EMOJIS[0];
    }

    private float GestureMultiplier (GestureTime gestureTime) {
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

    private void SetLives () {
        if (Player.Lives > 100) // Tutorial settings
      livesLabel.text = LIVES_TEXT + "\n∞";
        else
            livesLabel.text = LIVES_TEXT + "\n" + Player.Lives;
    }

    #endregion

}
