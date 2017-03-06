using UnityEngine;
using System.Collections;
using PDollarGestureRecognizer;

public class InputManager : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject gestureRecognizerPrefab;
  private GestureRecognizer gestureRecognizer;

  [SerializeField] private GameObject handPrefab;
  private HandController handController;

  private Rect drawArea;
  private RuntimePlatform platform;
  private Vector3 virtualKeyPosition = Vector2.zero;

  private bool listening = false; // TODO: corregir esto con extensiones para las Coroutines
  private bool mouseUp = true;

  #endregion

  #region Mono Behaviour

  void Awake() {
    gestureRecognizer = Instantiate(gestureRecognizerPrefab, transform).GetComponent<GestureRecognizer>();
    handController = Instantiate(handPrefab, transform).GetComponent<HandController>();
    platform = Application.platform;
    drawArea = new Rect(0, 0, Screen.width, Screen.height);
  } 

  void OnDisable() {
    gestureRecognizer.ResetGestureLines();
    handController.RemoveHand();
  }

  void Update() {

    // KEYBOARD

    if (Input.GetKeyDown(KeyCode.Escape))
      EventManager.TriggerEvent(new EscapeInput());

    // MOUSE & TOUCH
    
    if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
      if (Input.touchCount > 0)
        virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
    } else {
      if (Input.GetMouseButton(0))
        virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    }

    if (drawArea.Contains(virtualKeyPosition)) {

      if (Input.GetMouseButtonDown(0)) {
        gestureRecognizer.NewLine(transform);
        mouseUp = false;
        if (!listening)
          StartCoroutine(GestureRoutine());
      }

      if (Input.GetMouseButton(0)) {
        handController.SetHand(0, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
        gestureRecognizer.NewPoint(virtualKeyPosition);
      }

    }

    if (Input.GetMouseButtonUp(0))
      mouseUp = true;

  }

  #endregion

  #region Private Behaviour

  private IEnumerator GestureRoutine() {
    listening = true;

    while (!mouseUp)
      yield return null;

    yield return new WaitForSeconds(Config.GESTURE_STROKE_TIME);

    while (!mouseUp)
      yield return null;

    if (mouseUp && gestureRecognizer.CurrentPointAmount > 5) { // The library doesn't support one click inputs 
      Result result = gestureRecognizer.RecognizeGesture();
      EventManager.TriggerEvent(new GestureInput(result.GestureClass.ToString(), result.Score));
    }

    gestureRecognizer.ResetGestureLines();

    listening = false;
  }

  #endregion

}