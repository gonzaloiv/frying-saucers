using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelScreenController : MonoBehaviour {

    #region Fields

    private const float FADE_IN_TIME = 0.5f;
    private const string SCORE_TEXT = "SCORE";
    private const string LIVES_TEXT = "LIVES";
    private string[] EMOJIS = new string[] { "ʘ.ʘ", "╥_╥", "＾∇＾", "˘ڡ˘" };

    [SerializeField] private Text emojiLabel;
    [SerializeField] private Text scoreLabel;
    [SerializeField] private Text livesLabel;
    private int playerCurrentScore;

    #endregion

    #region Events

    public delegate void GameOverEventHandler (GameOverEventArgs gameOverEventArgs);
    public static event GameOverEventHandler GameOverEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Update () {
        if (playerCurrentScore < Player.Score)
            playerCurrentScore++;
        scoreLabel.text = SCORE_TEXT + "\n" + playerCurrentScore;
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
        playerCurrentScore = Player.Score;
        scoreLabel.text = SCORE_TEXT + "\n" + playerCurrentScore;
        SetLives();
        DOTween.Sequence().Append(scoreLabel.DOFade(0, FADE_IN_TIME / 2)).Append(scoreLabel.DOFade(1, FADE_IN_TIME / 2));
        DOTween.Sequence().Append(livesLabel.DOFade(0, FADE_IN_TIME / 2)).Append(livesLabel.DOFade(1, FADE_IN_TIME / 2));
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

    public void OnPlayerHitEvent () {
        Player.Lives--;
        if (Player.Lives < 1)
            GameOverEvent.Invoke(new GameOverEventArgs(Player.Score));
        DOTween.Sequence().Append(livesLabel.DOFade(0, FADE_IN_TIME / 2)).Append(livesLabel.DOFade(1, FADE_IN_TIME / 2));
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
