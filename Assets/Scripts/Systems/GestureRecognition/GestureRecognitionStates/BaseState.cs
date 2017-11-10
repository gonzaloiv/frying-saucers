using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

namespace GestureRecognitionStates {

    public class BaseState : State {

        #region Fields

        protected GestureRecognitionController gestureRecognitionController;
        protected Camera cam;
        protected ResultIndicatorController resultIndicatorController;
        protected HandIndicatorController handIndicatorController;
        protected GestureIndicatorController gestureIndicatorController;
        protected GestureRecognizer gestureRecognizer;
        protected GestureInput gestureInput { get { return gestureRecognitionController.GestureInput; } }

        #endregion

        #region Mono Behaviour

        void Awake () {
            gestureRecognitionController = GetComponent<GestureRecognitionController>();
            cam = gestureRecognitionController.Cam;
            resultIndicatorController = gestureRecognitionController.ResultIndicatorController;
            handIndicatorController = gestureRecognitionController.HandIndicatorController;
            gestureIndicatorController = gestureRecognitionController.GestureIndicatorController;
            gestureRecognizer = gestureRecognitionController.GestureRecognizer;
        }

        #endregion

    }

}