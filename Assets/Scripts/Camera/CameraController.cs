using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

  #region Fields

  private float SHAKING_TIME = 0.3f;
  private float DECREASE_FACTOR = 1f;
  private float SHAKE_AMOUNT = 0.1f;

  private float shake;

  #endregion

  #region Mono Behaviour

  void OnEnable() {
    EventManager.StartListening<EnemyShotEvent>(OnEnemyShotEvent);
  }

  void OnDisable() {
    EventManager.StartListening<EnemyShotEvent>(OnEnemyShotEvent);
  }

  #endregion

  #region Event Behaviour

  void OnEnemyShotEvent(EnemyShotEvent enemyShotEvent) {
    StartCoroutine(CameraShakeRoutine());
  }

  #endregion

  private IEnumerator CameraShakeRoutine() {
    float currentTime = Time.time;
    while (Time.time < currentTime + SHAKING_TIME) {
      Vector2 position = Random.insideUnitCircle * SHAKE_AMOUNT;
      transform.localPosition = new Vector3(position.x, position.y, transform.position.z);
      yield return null;
    }
  }

}
