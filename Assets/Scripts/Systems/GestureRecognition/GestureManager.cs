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
    
    private List<LineRenderer> gestureLines = new List<LineRenderer>();
    private LineRenderer currentGestureLine;

    private bool listening = false;
    private bool mouseUp = true;
    private float sectionTime;
    private EnemyType enemyType;

    #endregion

    #region Events

    public delegate void RightGestureInputEventHandler (GestureInputEventArgs gestureInputEventArgs);
    public static event RightGestureInputEventHandler RightGestureInputEvent = delegate {};

    public delegate void WrongGestureInputEventHandler (GestureInputEventArgs gestureInputEventArgs);
    public static event WrongGestureInputEventHandler WrongGestureInputEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake () {
        gestureRecognizer = GetComponentInChildren<GestureRecognizer>();
    }

    void OnEnable () {
        EnemyController.EnemyAttackEvent += OnEnemyAttackEvent;
    }

    void Update () {
        
        Vector3 virtualKeyPosition = Vector3.zero;

        if (Input.GetMouseButtonDown(0)) {
            if (!listening)
                StartCoroutine(GestureRoutine(sectionTime));
            currentGestureLine = gestureIndicatorController.SpawnGestureLineRenderer(transform);
            gestureLines.Add(currentGestureLine);
            gestureRecognizer.NewLine(transform);
            mouseUp = false;
        }

        if (Input.GetMouseButton(0)) {
            virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 1));
            handIndicatorController.SetHand(0, cam.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
            gestureRecognizer.NewPoint(virtualKeyPosition);
            currentGestureLine.positionCount++;
            currentGestureLine.SetPosition(currentGestureLine.positionCount - 1, worldPosition);
        }

        if (Input.GetMouseButtonUp(0)) {
            mouseUp = true;
            resultIndicatorController.SetCursorPosition(cam.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
        }

    }

    void OnDisable () {
        gestureRecognizer.ResetGestureLines();
        handIndicatorController.RemoveHand();
        EnemyController.EnemyAttackEvent -= OnEnemyAttackEvent;
    }

    #endregion

    #region Public Behaviour

    public void OnEnemyAttackEvent (EnemyAttackEventArgs enemyAttackEventArgs) {
        sectionTime = enemyAttackEventArgs.SectionTime;
        enemyType = enemyAttackEventArgs.EnemyType;
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

        if (mouseUp && gestureRecognizer.CurrentPointAmount > 5) // The library doesn't support one click inputs
            RecognizeGesture(gestureTime);

        gestureRecognizer.ResetGestureLines();

        foreach (LineRenderer lineRenderer in gestureLines) {
            lineRenderer.positionCount = 0;
            lineRenderer.gameObject.SetActive(false);
        }
        gestureLines.Clear();

        listening = false;

    }

    private void RecognizeGesture (GestureTime gestureTime) {
        Result result = gestureRecognizer.RecognizeGesture();
        GestureInputEventArgs gestureInputEventArgs = new GestureInputEventArgs(result.GestureClass.ToString(), result.Score, gestureTime);
        if (result.Score < GameConfig.GestureMinScore || (int) gestureInputEventArgs.Type != (int) enemyType) {
            WrongGestureInputEvent.Invoke(gestureInputEventArgs);
        } else {
            RightGestureInputEvent.Invoke(gestureInputEventArgs);
        }
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
