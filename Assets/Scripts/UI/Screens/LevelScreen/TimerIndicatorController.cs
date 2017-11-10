using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class TimerIndicatorController : MonoBehaviour {

    #region Fields / Properties

    private const float ANIMATION_TIME = 0.3f;
    [SerializeField] private List<Text> timerLabels;

    #endregion

    #region Mono Behaviour

    void Awake () {
        timerLabels.ForEach(timerLabel => timerLabel.gameObject.SetActive(false));
    }

    void OnEnable () {
        WaveController.EnemyAttackStartEvent += OnEnemyAttackStartEvent;
        WaveController.WaveEndEvent += OnWaveEndEvent;
    }

    void OnDisable () {
        WaveController.EnemyAttackStartEvent -= OnEnemyAttackStartEvent;
        WaveController.WaveEndEvent += OnWaveEndEvent;
    }

    #endregion

    #region Public Behaviour

    private void OnEnemyAttackStartEvent (float time) {
        StartCoroutine(TimerLabelRoutine(time));
    }

    private void OnWaveEndEvent () {
        StartCoroutine(WaveEndRoutine());
    }

    #endregion

    #region Private Behaviour

    private void ShowTimerLabel (Text timerLabel, float time) {
        timerLabel.gameObject.SetActive(true);
        DOTween.Sequence().Append(timerLabel.transform.DOScale(0.5f, 0.01f)).Append(timerLabel.transform.DOScale(1, time));
        DOTween.Sequence().Append(timerLabel.DOFade(0.5f, 0.01f)).Append(timerLabel.DOFade(1, time));
    }

    private IEnumerator TimerLabelRoutine (float time) {
        float fractionTime = time / timerLabels.Count;
        for (int i = 0; i < timerLabels.Count - 1; i++) {
            yield return new WaitForSeconds(fractionTime);
            ShowTimerLabel(timerLabels[i], fractionTime);
        }
        yield return new WaitForSeconds(fractionTime);
        timerLabels.ForEach(timerLabel => timerLabel.gameObject.SetActive(false));
    }

    private IEnumerator WaveEndRoutine () {
        for (int i = 0; i < timerLabels.Count - 1; i++) {
            ShowTimerLabel(timerLabels[timerLabels.Count - 1], ANIMATION_TIME);
            yield return new WaitForSeconds(ANIMATION_TIME * 1.2f);
        }
        timerLabels.ForEach(timerLabel => timerLabel.gameObject.SetActive(false));
    }

    #endregion
	
}
