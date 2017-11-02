using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

public class GestureManager : MonoBehaviour {

    #region Fields / Properties

    [SerializeField] private ResultIndicatorController resultIndicatorController;
    [SerializeField] private HandIndicatorController handIndicatorController;

    private GestureRecognizer gestureRecognizer;
    private Vector3 virtualKeyPosition = Vector2.zero;
    private IEnumerator gestureRoutine;
    private bool listening = false;
    private bool mouseUp = true;
    private float sectionTime;

    #endregion

    #region Events

    public delegate void GestureInputEventHandler (GestureInputEventArgs gestureInputEventArgs);
    public static event GestureInputEventHandler GestureInputEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        gestureRecognizer = GetComponentInChildren<GestureRecognizer>();
    }

    void OnEnable () {
        EnemyBehaviour.EnemyAttackEvent += OnEnemyAttackEvent;
    }

    void Update () {

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
            if (Input.touchCount > 0)
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
        } else {
            if (Input.GetMouseButton(0))
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonDown(0)) {
            if (!listening)
                StartCoroutine(GestureRoutine(sectionTime));
            gestureRecognizer.NewLine(transform);
            mouseUp = false;
        }

        if (Input.GetMouseButton(0)) {
            handIndicatorController.SetHand(0, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
            gestureRecognizer.NewPoint(virtualKeyPosition);
        }

        if (Input.GetMouseButtonUp(0)) {
            mouseUp = true;
            resultIndicatorController.SetCursorPosition(Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
        }

    }

    void OnDisable () {
        gestureRecognizer.ResetGestureLines();
        handIndicatorController.RemoveHand();
        EnemyBehaviour.EnemyAttackEvent -= OnEnemyAttackEvent;
    }

    #endregion

    #region Public Behaviour

    public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        sectionTime = enemyAttackEventArgs.SectionTime;
    }

    #endregion

    #region Private Behaviour

    private IEnumerator GestureRoutine (float sectionTime) {

        listening = true;
        float initialTime = Time.time;

        while (!mouseUp)
            yield return null;

        yield return new WaitForSeconds(GameConfig.GestureStrokeTime);

        while (!mouseUp)
            yield return null;

        GestureTime gestureTime = SetGestureTime(initialTime, sectionTime);

        if (mouseUp && gestureRecognizer.CurrentPointAmount > 5) { // The library doesn't support one click inputs
            Result result = gestureRecognizer.RecognizeGesture();
            GestureInputEvent.Invoke(new GestureInputEventArgs(result.GestureClass.ToString(), result.Score, gestureTime));
        }

        gestureRecognizer.ResetGestureLines();

        listening = false;

    }

    private GestureTime SetGestureTime (float initialTime, float sectionTime) {
        float finalTime = Time.time - initialTime;
        if (finalTime < sectionTime) {
            return GestureTime.TooFast;
        } else if (finalTime > 5 * sectionTime) {
            return GestureTime.TooSlow; 
        } else if (finalTime > 2 * sectionTime && finalTime < 5 * sectionTime) {
            return GestureTime.Perfect; 
        } else {
            return GestureTime.Ok;
        }
    }

    #endregion

}
