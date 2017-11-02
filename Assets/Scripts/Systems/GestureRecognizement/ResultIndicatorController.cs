using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultIndicatorController : MonoBehaviour {

    #region Fields

    private const float ANIMATION_TIME = 0.3f;
    private static string[] RESULT_TEXT = new string[] {"Perfect!", "Not bad", "Too fast", "Too slow", "Gross"};
    private const string COMBO_TEXT = "x";

    [SerializeField] private Text resultLabel;
    [SerializeField] private Text comboLabel;

    private Player player;
    private Vector2 cursorPosition = Vector2.zero;

    #endregion

    #region Mono Behaviour

    void Awake () {
        comboLabel.enabled = false;
        resultLabel.enabled = false;
    }

    void Update () {
        comboLabel.text = COMBO_TEXT + player.Combo;
    }

    void OnEnable () {
        EnemyBehaviour.RightGestureInputEvent += OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent += OnWrongGestureInputEvent;
    }

    void OnDisable () {
        EnemyBehaviour.RightGestureInputEvent -= OnRightGestureInputEvent;
        EnemyBehaviour.WrongGestureInputEvent -= OnWrongGestureInputEvent;
    }

    #endregion

    #region Public Behaviour

    public void Init(Player player) {
        this.player = player;
    }

    public void SetCursorPosition (Vector2 cursorPosition) {
        this.cursorPosition = cursorPosition;
    }

    public void OnRightGestureInputEvent (RightGestureInputEventArgs rightGestureInputEventArgs) {
        StartCoroutine(ResultRoutine(rightGestureInputEventArgs.GestureInputEventArgs.Time, true));
    }

    public void OnWrongGestureInputEvent (WrongGestureInputEventArgs wrongGestureInputEventArgs) {
        StartCoroutine(ResultRoutine(GestureTime.Gross, false));
    }

    #endregion

    #region Private Behaviour

    private IEnumerator ResultRoutine (GestureTime gestureTime, bool combo) {

        resultLabel.text = RESULT_TEXT[(int) gestureTime];
        resultLabel.transform.position = cursorPosition + new Vector2(1, 0.6f);
        resultLabel.enabled = true;
        DOTween.Sequence().Append(resultLabel.DOFade(0, 0.001f)).Append(resultLabel.DOFade(1, ANIMATION_TIME));


        if (combo) {
            comboLabel.transform.position = cursorPosition + new Vector2(1, 0.2f);
            comboLabel.enabled = true;
            DOTween.Sequence().Append(comboLabel.DOFade(0, 0.001f)).Append(comboLabel.DOFade(1, ANIMATION_TIME));
        }

        yield return TimeManager.WaitForRealTime(ANIMATION_TIME);

        comboLabel.enabled = false;
        resultLabel.enabled = false;

    }

    #endregion

}