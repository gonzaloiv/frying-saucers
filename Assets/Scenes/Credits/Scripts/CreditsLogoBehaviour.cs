using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsLogoBehaviour : MonoBehaviour {

  #region Private Behaviour

  private SpriteRenderer logo;
  private IEnumerator blinkingRoutine;

  #endregion

  #region Mono Behaviour

  void Awake() {
    logo = GetComponent<SpriteRenderer>();
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
      logo.enabled = true;
      timeToWait = Time.realtimeSinceStartup + 0.3f;
      while (Time.realtimeSinceStartup < timeToWait)
        yield return 0;
      logo.enabled = false;
      timeToWait = Time.realtimeSinceStartup + 0.3f;
      while (Time.realtimeSinceStartup < timeToWait)
        yield return 0;
    }
  }

  #endregion

}
