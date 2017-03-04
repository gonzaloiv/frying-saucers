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

  #endregion

  #region Mono Behaviour

  void Awake() {
    gestureRecognizer = Instantiate(gestureRecognizerPrefab, transform).GetComponent<GestureRecognizer>();
    handController = Instantiate(handPrefab, transform).GetComponent<HandController>();
    platform = Application.platform;
    drawArea = new Rect(0, 0, Screen.width, Screen.height);
  } 

  void Update() {

    // KEYBOARD

    if (Input.GetKeyDown(KeyCode.Return))
      EventManager.TriggerEvent(new ReturnInput());

    if (Input.GetKeyDown(KeyCode.Escape))
      EventManager.TriggerEvent(new EscapeInput());

    // MOUSE & TOUCH
    
    if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
      if (Input.touchCount > 0) {
        virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
      }
    } else {
      if (Input.GetMouseButton(0)) {
        virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
      }
    }

    if (drawArea.Contains(virtualKeyPosition)) {

      if (Input.GetMouseButtonDown(0)) {
        if (!listening)
          StartCoroutine(GestureRoutine());
        gestureRecognizer.NewLine(transform);
      }

      if (Input.GetMouseButton(0)) {
        handController.SetHand(0, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
        gestureRecognizer.NewPoint(virtualKeyPosition);
      }

    }

//    if (Input.GetMouseButtonUp(0))
//        handController.RemoveHand();

  }

  #endregion

  #region Private Behaviour

  private IEnumerator GestureRoutine() {
    listening = true;

    yield return new WaitForSeconds(Config.GESTURE_TIME);

    Result result = gestureRecognizer.RecognizeGesture();
    EventManager.TriggerEvent(new GestureInput(result.GestureClass.ToString(), result.Score));

    listening = false;
  }

  #endregion

}


