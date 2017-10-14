using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour {

    #region Fields

    private static string[] RESULT_TEXT = new string[] {"Perfect!", "Not bad", "Too fast", "Too slow", "Gross"};
    private const string COMBO_TEXT = "x";

    private Text comboLabel;
    private Animator comboLabelAnimator;
    private Text resultLabel;
    private Animator resultLabelAnimator;

    private Vector2 cursorPosition = Vector2.zero;

    #endregion

    #region Mono Behaviour

    void Awake () {
        resultLabel = GetComponentsInChildren<Text>()[0];
        resultLabelAnimator = resultLabel.gameObject.GetComponentInChildren<Animator>();
        comboLabel = GetComponentsInChildren<Text>()[1];
        comboLabelAnimator = comboLabel.gameObject.GetComponentInChildren<Animator>();
        comboLabel.enabled = false;
        resultLabel.enabled = false;
    }

    void Update () {
        comboLabel.text = COMBO_TEXT + Player.Combo;
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
        resultLabelAnimator.Play("Spawn");

        if (combo) {
            comboLabel.transform.position = cursorPosition + new Vector2(1, 0.2f);
            comboLabel.enabled = true;
            comboLabelAnimator.Play("Spawn");
        }

        yield return TimeManager.WaitForRealTime(1f);

        comboLabel.enabled = false;
        resultLabel.enabled = false;

    }

    #endregion

}