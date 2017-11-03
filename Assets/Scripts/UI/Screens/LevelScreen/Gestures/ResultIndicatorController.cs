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

    [SerializeField] private GameObject resultLabelPrefab;
    [SerializeField] private GameObject comboLabelPrefab;

    private Player player;
    private Vector2 cursorPosition = Vector2.zero;
    private Text resultLabel;
    private Text comboLabel;

    #endregion

    #region Mono Behaviour

    void Awake () {
        resultLabel = Instantiate(resultLabelPrefab, transform).GetComponent<Text>();
        resultLabel.enabled = false;
        comboLabel = Instantiate(comboLabelPrefab, transform).GetComponent<Text>();
        comboLabel.enabled = false;
    }

    void Update () {
        comboLabel.text = COMBO_TEXT + player.Combo;
    }

    void OnEnable () {
        GestureManager.RightGestureInputEvent += OnRightGestureInputEvent;
        GestureManager.WrongGestureInputEvent += OnWrongGestureInputEvent;
    }

    void OnDisable () {
        GestureManager.RightGestureInputEvent -= OnRightGestureInputEvent;
        GestureManager.WrongGestureInputEvent -= OnWrongGestureInputEvent;
    }

    #endregion

    #region Public Behaviour

    public void Init(Player player) {
        this.player = player;
    }

    public void SetCursorPosition (Vector2 cursorPosition) {
        this.cursorPosition = cursorPosition;
    }

    public void OnRightGestureInputEvent (GestureInputEventArgs gestureInputEventArgs) {
        StartCoroutine(ResultRoutine(gestureInputEventArgs.Time, true));
    }

    public void OnWrongGestureInputEvent (GestureInputEventArgs gestureInputEventArgs) {
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