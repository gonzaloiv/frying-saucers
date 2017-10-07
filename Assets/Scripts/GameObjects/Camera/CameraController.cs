using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    #region Fields

    private float SHAKING_TIME = 0.3f;
    private float SHAKE_AMOUNT = 0.2f;

    private float shake;

    #endregion

    #region Mono Behaviour

    void OnEnable () {
        PlayerController.PlayerHitEvent += OnPlayerHitEvent;
        EnemyBehaviour.RightGestureInputEvent += OnRightGestureInput;
    }

    void OnDisable () {
        PlayerController.PlayerHitEvent -= OnPlayerHitEvent;
        EnemyBehaviour.RightGestureInputEvent -= OnRightGestureInput;
    }

    #endregion

    #region Public Behaviour

    public void OnPlayerHitEvent (PlayerHitEventArgs playerHitEventArgs) {
        StartCoroutine(CameraShakeRoutine());
    }

    public void OnRightGestureInput (RightGestureInputEventArgs rightGestureInputEventArgs) {
        StartCoroutine(CameraShakeRoutine());
    }

    #endregion

    #region Private Behaviour

    private IEnumerator CameraShakeRoutine () {
        float currentTime = Time.unscaledTime;
        while (Time.unscaledTime < currentTime + SHAKING_TIME) {
            Vector2 position = Random.insideUnitCircle * SHAKE_AMOUNT;
            transform.localPosition = new Vector3(position.x, position.y, transform.position.z);
            yield return null;
        }
    }

    #endregion

}
