using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingTextBehaviour : MonoBehaviour {

  #region Private Behaviour

  private Text pauseScreenLabel;
  private IEnumerator blinkingRoutine;

  #endregion

  #region Mono Behaviour

  void Awake() {
    pauseScreenLabel = GetComponent<Text>();
    blinkingRoutine = BlinkingRoutine();
  }

  void OnEnable() {
    Play();
  }

  void OnDisable() {
    Stop();
  }

  #endregion

  #region Public Behaviour

  public void Play() {
    StartCoroutine(blinkingRoutine);
  }

  public void Stop() {
    StopCoroutine(blinkingRoutine);
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
