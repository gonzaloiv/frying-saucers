﻿using System.Collections;
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

    [SerializeField] private GameObject gestureManager;
    [SerializeField] private ResultIndicatorController resultIndicatorController;
    [SerializeField] private Text emojiLabel;
    [SerializeField] private Text scoreLabel;
    [SerializeField] private Text livesLabel;

    private Player player;

    #endregion

    #region Mono Behaviour

    void Awake() {
        gestureManager.SetActive(false);
    }

    void Update () {
        scoreLabel.text = SCORE_TEXT + "\n" + player.Score;
    }

    void OnEnable () {
        DOTween.Sequence().Append(scoreLabel.DOFade(0, FADE_IN_TIME / 2)).Append(scoreLabel.DOFade(1, FADE_IN_TIME / 2));
        DOTween.Sequence().Append(livesLabel.DOFade(0, FADE_IN_TIME / 2)).Append(livesLabel.DOFade(1, FADE_IN_TIME / 2));
        EnemyBehaviour.RightGestureInputEvent += OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent += OnWrongGestureInputEvent;
        Player.PlayerHitEvent += OnPlayerHitEvent;
        gestureManager.SetActive(true);
    }

    void OnDisable () {
        EnemyBehaviour.RightGestureInputEvent -= OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent -= OnWrongGestureInputEvent;
        Player.PlayerHitEvent -= OnPlayerHitEvent;
        gestureManager.SetActive(false);
    }

    #endregion

    #region Public Behaviour

    public void Init (Player player) {
        this.player = player;
        resultIndicatorController.Init(player);
        UpdateLivesLabel(player.Lives);
    }

    public void OnRightGestureInputEvent (RightGestureInputEventArgs rightGestureInputEventArgs) {
        IEnumerator emojiRoutine = player.Combo >= 5 ? EmojiRoutine(EMOJIS[3], 3) : EmojiRoutine(EMOJIS[2], 1);
        StartCoroutine(emojiRoutine);
        player.IncreaseCombo();
        player.IncreaseScore((int) Mathf.Ceil(GameConfig.EnemyScore * player.Combo * GestureMultiplier(rightGestureInputEventArgs.GestureInputEventArgs.Time)));
    }

    public void OnWrongGestureInputEvent (WrongGestureInputEventArgs wrongGestureInputArgs) {
        player.ResetCombo();
        StartCoroutine(EmojiRoutine(EMOJIS[1], 1));
    }

    public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
        if (playerHitEventArgs.IsDead) {
            StopAllCoroutines();
            StartCoroutine(EmojiRoutine(EMOJIS[1], 4));
        } else {
            UpdateLivesLabel(playerHitEventArgs.Lives);
        }
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

    private void UpdateLivesLabel (int amount) {
        DOTween.Sequence().Append(livesLabel.DOFade(0, FADE_IN_TIME / 2)).Append(livesLabel.DOFade(1, FADE_IN_TIME / 2));
        livesLabel.text = amount > 100 ? LIVES_TEXT + "\n∞" : LIVES_TEXT + "\n" + amount;
    }

    #endregion

}
