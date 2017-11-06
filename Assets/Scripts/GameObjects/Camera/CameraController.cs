using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    #region Fields

    private float SHAKE_TIME = 0.3f;
    private float SHAKE_AMOUNT = 0.1f;

    #endregion

    #region Mono Behaviour

    void OnEnable () {
        Player.PlayerHitEvent += OnPlayerHitEvent;
        GestureManager.RightGestureInputEvent += OnRightGestureInput;
    }

    void OnDisable () {
        Player.PlayerHitEvent -= OnPlayerHitEvent;
        GestureManager.RightGestureInputEvent -= OnRightGestureInput;
    }

    #endregion

    #region Public Behaviour

    public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
        StartCoroutine(CameraShakeRoutine());
    }

    public void OnRightGestureInput (GestureInputEventArgs gestureInputEventArgs) {
        StartCoroutine(CameraShakeRoutine());
    }

    #endregion

    #region Private Behaviour

    private IEnumerator CameraShakeRoutine () {
        float currentTime = Time.unscaledTime;
        while (Time.unscaledTime < currentTime + SHAKE_TIME) {
            Vector2 position = Random.insideUnitCircle * SHAKE_AMOUNT;
            transform.localPosition = new Vector3(position.x, position.y, transform.position.z);
            yield return null;
        }
    }

    #endregion

}
