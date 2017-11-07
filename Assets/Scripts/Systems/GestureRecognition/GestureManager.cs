using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

public class GestureManager : MonoBehaviour {

    #region Fields / Properties

    [SerializeField] private Camera cam;
    [SerializeField] private ResultIndicatorController resultIndicatorController;
    [SerializeField] private HandIndicatorController handIndicatorController;
    [SerializeField] private GestureIndicatorController gestureIndicatorController;
    private GestureRecognizer gestureRecognizer;
    
    private EnemyAttackEventArgs currentEnemyAttack;

    #endregion

    #region Events

    public delegate void RightGestureInputEventHandler (GestureInputEventArgs gestureInputEventArgs);
    public static event RightGestureInputEventHandler RightGestureInputEvent = delegate {};

    public delegate void WrongGestureInputEventHandler (GestureInputEventArgs gestureInputEventArgs);
    public static event WrongGestureInputEventHandler WrongGestureInputEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        gestureRecognizer = new GestureRecognizer();
        gestureIndicatorController.Init(cam);
    }

    void OnEnable () {
        EnemyController.EnemyAttackEvent += OnEnemyAttackEvent;
    }

    void OnDisable () {
        EnemyController.EnemyAttackEvent -= OnEnemyAttackEvent;
        handIndicatorController.RemoveHand();
        ResetGestures();
    }

    #endregion

    #region Public Behaviour

    public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        currentEnemyAttack = enemyAttackEventArgs;
        StartCoroutine(GestureRecognitionRoutine());
    }

    #endregion

    #region Private Behaviour

    private IEnumerator GestureRecognitionRoutine () {

        float initialTime = Time.time;
        GestureTime gestureTime = GestureTime.Gross;
        Vector3 virtualKeyPosition = Vector3.zero;

        while (Time.time < initialTime + currentEnemyAttack.RoutineTime - currentEnemyAttack.RoutineTime / 2) { 

            if (Input.GetMouseButton(0))
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

            if (Input.GetMouseButtonDown(0)) {
                initialTime = Time.time;
                gestureRecognizer.NewLine();
                gestureIndicatorController.SpawnGestureLineRenderer(virtualKeyPosition);
            }

            if (Input.GetMouseButton(0)) { // Depends on the GetMouseButtonDown() above
                gestureRecognizer.NewPoint(virtualKeyPosition);
                gestureIndicatorController.SetNewPosition(virtualKeyPosition);
                handIndicatorController.SetHand(0, cam.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
            }

            if (Input.GetMouseButtonUp(0)) {
                resultIndicatorController.SetCursorPosition(cam.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
                gestureTime = SetGestureTime(initialTime, currentEnemyAttack.SectionTime);
                if (gestureRecognizer.StrokeIndex == 2 || currentEnemyAttack.EnemyType != EnemyType.Cross) // TODO: Refactoring this...
                    break;
            }

            yield return 0;

        }

        RecognizeGesture(gestureTime);
        ResetGestures();

        yield return 0;

    }

    private void RecognizeGesture (GestureTime gestureTime) {
        Result result = gestureRecognizer.RecognizeGesture();
        GestureInputEventArgs gestureInputEventArgs = new GestureInputEventArgs(result.GestureClass, result.Score, gestureTime);
        if (IsRightGesture(result)) {
            RightGestureInputEvent.Invoke(gestureInputEventArgs);
        } else if(result.Score > GameConfig.GestureMinScore / 2) { // Otherwise the event is triggered even without input by the player
            WrongGestureInputEvent.Invoke(gestureInputEventArgs);
        }
    }

    private GestureTime SetGestureTime (float initialTime, float sectionTime) {
        float finalTime = Time.time - initialTime;
        if (finalTime < sectionTime) {
            return GestureTime.TooFast;
        } else if (finalTime > 5 * sectionTime) {
            return GestureTime.TooSlow; 
        } else if (finalTime > 2 * sectionTime && finalTime < 4 * sectionTime) {
            return GestureTime.Perfect; 
        } else {
            return GestureTime.Ok;
        }
    }

    private bool IsRightGesture (Result result) {
        return result.Score > GameConfig.GestureMinScore && result.GestureClass.ToUpper() == currentEnemyAttack.EnemyType.ToString().ToUpper();
    }

    private void ResetGestures () {
        gestureRecognizer.Reset();
        gestureIndicatorController.ResetGestureLines();
    }

    #endregion

}
