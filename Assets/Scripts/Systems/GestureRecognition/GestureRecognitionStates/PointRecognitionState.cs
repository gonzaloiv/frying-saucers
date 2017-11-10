using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;

namespace GestureRecognitionStates {

    public class PointRecognitionState : BaseState {

        #region Public Behaviour

        public override void Play () {
            base.Play();

            if (Input.GetMouseButton(0)) { 
                gestureInput.SetVirtualKeyPosition(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
                gestureRecognizer.NewPoint(gestureInput.VirtualKeyPosition);
                gestureIndicatorController.SetNewPosition(gestureInput.VirtualKeyPosition);
                handIndicatorController.SetHand(0, cam.ScreenToWorldPoint(new Vector3(gestureInput.VirtualKeyPosition.x, gestureInput.VirtualKeyPosition.y, 10)));
            }

            if (Input.GetMouseButtonUp(0)) {
                resultIndicatorController.SetCursorPosition(cam.ScreenToWorldPoint(new Vector3(gestureInput.VirtualKeyPosition.x, gestureInput.VirtualKeyPosition.y, 10)));
                if (gestureRecognizer.StrokeIndex < 2 && gestureInput.EnemyType == EnemyType.Cross) {
                    gestureRecognitionController.ToLineRecognitionState();
                } else { // TODO: Possibly a GestureRecognitionState would be better...
                    RecognizeGesture();
                    gestureRecognizer.Reset();
                    gestureIndicatorController.ResetGestureLines();
                    gestureRecognitionController.ToIdleState();
                }
            }

        }

        #endregion

        #region Private Behaviour

        private void RecognizeGesture () {
            Result result = gestureRecognizer.RecognizeGesture();
            GestureInputEventArgs gestureInputEventArgs = new GestureInputEventArgs(result.GestureClass, result.Score, gestureInput.GetGestureTime());
            if (IsRightGesture(result)) {
                gestureRecognitionController.InvokeRightGestureInputEvent(gestureInputEventArgs);
            } else if (result.Score > GameConfig.GestureMinScore / 2) { // Otherwise the event is triggered even without input by the player
                gestureRecognitionController.InvokeWrongGestureInputEvent(gestureInputEventArgs);
            }
        }

        private bool IsRightGesture (Result result) {
            return result.Score > GameConfig.GestureMinScore && result.GestureClass.ToUpper() == gestureInput.EnemyType.ToString().ToUpper();
        }

        #endregion

    }

}