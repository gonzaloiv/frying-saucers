using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

  public class PlayState : BaseState {

    #region Fields

    private GameObject currentEnemy;
    private GameObject previousEnemy;
    private IEnumerator waveRoutine;
    private bool playing = false;

    #endregion

    #region State Behaviour

    public override void Enter() {
      waveRoutine = WaveRoutine();
      StartCoroutine(waveRoutine);
    }

    public override void Play() {
      if (!playing) {
        waveRoutine = WaveRoutine();
        StartCoroutine(waveRoutine);
      }
    }

    #endregion

    #region Mono Behaviour

    void OnEnable() {
      EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);
    }

    void OnDisable() {
      EventManager.StopListening<PlayerHitEvent>(OnPlayerHitEvent);
    }

    #endregion

    #region Event Behaviour

    void OnPlayerHitEvent(PlayerHitEvent playerHitEvent) {
      StopCoroutine(waveRoutine);
    }

    #endregion

    #region Private Behaviour

    private IEnumerator WaveRoutine() {
      playing = true;
      yield return new WaitForSeconds(1);
      float routineTime = Random.Range(Config.SHOOTING_ROUTINE_TIME[0], Config.SHOOTING_ROUTINE_TIME[1]);
      SetCurrentEnemy();
      currentEnemy.GetComponent<IEnemyBehaviour>().Play(routineTime);
      previousEnemy = currentEnemy;
      yield return new WaitForSeconds(routineTime);
      playing = false;
    }

    private void SetCurrentEnemy() {
      currentEnemy = previousEnemy;
      while (currentEnemy == previousEnemy)
        currentEnemy = waveController.CurrentLevelObjects[Random.Range(0, waveController.CurrentLevelObjects.Length)];
    }

    #endregion

  }


}