using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class ResultController : MonoBehaviour {

  #region Fields

  private static string[] RESULT_TEXT = new string[] { "Perfect!", "Ok", "Too\nfast", "Too\nslow" };
  private const string COMBO_TEXT = "x";

  private Canvas canvas;

  private Text comboLabel;
  private Animator comboLabelAnimator;
  private Text resultLabel;
  private Animator resultLabelAnimator;

  private Vector2 cursorPosition = Vector2.zero;

  #endregion

  #region Mono Behaviour

  void Awake() {

    canvas = GetComponent<Canvas>();
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Sets world camera after instantiation
    canvas.sortingLayerName = "UI";

    resultLabel = GetComponentsInChildren<Text>()[0];
    resultLabelAnimator = resultLabel.gameObject.GetComponentInChildren<Animator>();
    comboLabel = GetComponentsInChildren<Text>()[1];
    comboLabelAnimator = comboLabel.gameObject.GetComponentInChildren<Animator>();

    comboLabel.enabled = false;
    resultLabel.enabled = false;

  }

  void Update() {
    comboLabel.text = COMBO_TEXT + Level.Combo;
  }

  void OnEnable() {
    EventManager.StartListening<RightGestureInput>(OnRightGestureInput);
  }

  void OnDisable() {
    EventManager.StartListening<RightGestureInput>(OnRightGestureInput);
  }

  #endregion

  #region Event Behaviour

  void OnRightGestureInput(RightGestureInput rightGestureInput) {
    StartCoroutine(ResultRoutine(rightGestureInput.GestureInput.Time));
  }

  #endregion

  #region Public Behaviour

  public void SetCursorPosition(Vector2 cursorPosition) {
    this.cursorPosition = cursorPosition;
  }

  #endregion

  #region Private Behaviour

  private IEnumerator ResultRoutine(GestureTime gestureTime) {

    resultLabel.text = RESULT_TEXT[(int) gestureTime];
    resultLabel.transform.position = cursorPosition + new Vector2(-1, 1);
    resultLabel.enabled = true;
    resultLabelAnimator.Play("Spawn");

    comboLabel.transform.position = cursorPosition + new Vector2(1, 1);
    comboLabel.enabled = true;
    comboLabelAnimator.Play("Spawn");

    yield return new WaitForSeconds(1);

    comboLabel.enabled = false;
    resultLabel.enabled = false;

  }

  #endregion

}