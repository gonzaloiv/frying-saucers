using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingTextBehaviour : MonoBehaviour {

  #region Private Behaviour

  private Text pauseScreenLabel;

  #endregion

  #region Mono Behaviour

  void Awake() {
    pauseScreenLabel = GetComponent<Text>();
  }

  void OnEnable() {
    StartCoroutine(BlinkingRoutine());
  }

  void OnDisable() {
    StopAllCoroutines();
  }

  #endregion

  #region Private Behaviour

  private IEnumerator BlinkingRoutine() {
    float timeToWait;
    while (gameObject.activeSelf) {
      pauseScreenLabel.enabled = true;
      timeToWait = Time.realtimeSinceStartup + 0.3f;
      while (Time.realtimeSinceStartup < timeToWait)
        yield return 0;
      pauseScreenLabel.enabled = false;
      timeToWait = Time.realtimeSinceStartup + 0.3f;
      while (Time.realtimeSinceStartup < timeToWait)
        yield return 0;
    } 
  }

  #endregion

}
