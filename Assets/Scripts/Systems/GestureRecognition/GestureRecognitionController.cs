using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GestureRecognitionStates;

public class GestureRecognitionController : StateMachine {

    #region Fields / Properties

    [SerializeField] private Camera cam;
    [SerializeField] private ResultIndicatorController resultIndicatorController;
    [SerializeField] private HandIndicatorController handIndicatorController;
    [SerializeField] private GestureIndicatorController gestureIndicatorController;

    public Camera Cam { get { return cam; } }
    public ResultIndicatorController ResultIndicatorController { get { return resultIndicatorController; } }
    public HandIndicatorController HandIndicatorController { get { return handIndicatorController; } }
    public GestureIndicatorController GestureIndicatorController { get { return gestureIndicatorController; } }
    public GestureInput GestureInput { get { return currentGestureInput; } }
    public GestureRecognizer GestureRecognizer { get { return gestureRecognizer; } set { gestureRecognizer = value; } }

    private GestureRecognizer gestureRecognizer;
    private GestureInput currentGestureInput;

    #endregion

    #region Events

    public delegate void RightGestureInputEventHandler (GestureInputEventArgs gestureInputEventArgs);
    public static event RightGestureInputEventHandler RightGestureInputEvent = delegate {};

    public delegate void WrongGestureInputEventHandler (GestureInputEventArgs gestureInputEventArgs);
    public static event WrongGestureInputEventHandler WrongGestureInputEvent = delegate {};

    #endregion

    #region Mono Behaviour

    void Awake() {
        gestureRecognizer = new GestureRecognizer();
        currentGestureInput = new GestureInput();
        GestureIndicatorController.Init(cam);
        ToIdleState();
    }

    void OnDisable () {
        handIndicatorController.RemoveHand();
        gestureIndicatorController.ResetGestureLines();
    }

    #endregion

    #region Public Behaviour

    public void ToIdleState () {
        ChangeState<IdleState>();
    }

    public void ToLineRecognitionState () {
        ChangeState<LineRecognitionState>();
    }

    public void ToPointRecognitionState () {
        ChangeState<PointRecognitionState>();
    }

    public void InvokeRightGestureInputEvent (GestureInputEventArgs gestureInputEventArgs) {
        RightGestureInputEvent.Invoke(gestureInputEventArgs);
    }

    public void InvokeWrongGestureInputEvent (GestureInputEventArgs gestureInputEventArgs) {
        WrongGestureInputEvent.Invoke(gestureInputEventArgs);
    }

    #endregion

}
