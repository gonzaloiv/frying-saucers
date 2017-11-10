using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GestureRecognitionStates {

    public class LineRecognitionState : BaseState {

        #region Public Behaviour

        public override void Play () {
            base.Play();
            if (Input.GetMouseButtonDown(0)) {
                gestureRecognizer.NewLine();
                gestureIndicatorController.SpawnGestureLineRenderer(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
                gestureRecognitionController.ToPointRecognitionState();
            }
        }

        #endregion
	
    }

}