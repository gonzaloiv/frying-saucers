using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScreenTextBehaviour : MonoBehaviour {

    #region Private Behaviour

    private Text text;
    private IEnumerator blinkingRoutine;

    #endregion

    #region Mono Behaviour

    void Awake () {
        text = GetComponent<Text>();
        blinkingRoutine = BlinkingRoutine();
    }

    void OnEnable () {
        Play();
    }

    void OnDisable () {
        Stop();
    }

    #endregion

    #region Public Behaviour

    public void Play () {
        StartCoroutine(blinkingRoutine);
    }

    public void Stop () {
        StopCoroutine(blinkingRoutine);
    }

    #endregion

    #region Private Behaviour

    private IEnumerator BlinkingRoutine () {
        float timeToWait;
        while (gameObject.activeSelf) {
            text.enabled = true;
            timeToWait = Time.realtimeSinceStartup + 0.3f;
            while (Time.realtimeSinceStartup < timeToWait)
                yield return 0;
            text.enabled = false;
            timeToWait = Time.realtimeSinceStartup + 0.3f;
            while (Time.realtimeSinceStartup < timeToWait)
                yield return 0;
        }
    }

    #endregion

}
