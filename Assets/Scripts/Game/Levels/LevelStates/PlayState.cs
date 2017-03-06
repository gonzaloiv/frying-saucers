using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

  public class PlayState : BaseState {

    #region Fields

    private GameObject currentEnemy;
    private GameObject previousEnemy;

    private bool playing = false;
    private IEnumerator waveRoutine;

    #endregion

    #region State Behaviour

    public override void Play() {
      if (!playing) {
        waveRoutine = WaveRoutine();
        StartCoroutine(waveRoutine);
      }
    }

    #endregion

    #region Private Behaviour

    private IEnumerator WaveRoutine() {
      playing = true;
      float routineTime = Random.Range(2f, 2.8f);
      yield return new WaitForSeconds(routineTime * 1.2f);
      SetCurrentEnemy();
      if(currentEnemy == previousEnemy)        
        yield return new WaitForSeconds(Random.Range(0.4f, 0.6f));
      currentEnemy.GetComponent<IEnemyBehaviour>().Play(routineTime);
      previousEnemy = currentEnemy;
      playing = false;
    }

    private void SetCurrentEnemy() {
      currentEnemy = previousEnemy;
      if (currentLevelObjects.Count() > 1)
        while (currentEnemy == previousEnemy)
          currentEnemy = currentLevelObjects[Random.Range(0, currentLevelObjects.Count())];
      else
        currentEnemy = currentLevelObjects[0];    
    }

    #endregion

  }


}