using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingRoutineLabel : MonoBehaviour {

  #region Fields

  private static string[] signs = new string[] { "?", "3", "2", "1", "0" , "x" };
  private Text label;

  #endregion

  #region Mono Behaviour

  void Awake() {
    label = GetComponent<Text>();
    label.text = signs[0];
  }

  void OnEnable() {
    EventManager.StartListening<EnemyAttackEvent>(OnEnemyAttackEvent);
  }

  void OnDisable() {
    EventManager.StopListening<EnemyAttackEvent>(OnEnemyAttackEvent);
  }

  #endregion

  #region Event Behaviour

  void OnEnemyAttackEvent(EnemyAttackEvent enemyAttackEvent) {
    StartCoroutine(ShootingRoutine(enemyAttackEvent.SectionTime));
  }

  #endregion

  #region Private Behaviour

  private IEnumerator ShootingRoutine(float sectionTime) {
    for (int i = 0; i < signs.Length; i++) { 
      label.text = signs[i];
      yield return new WaitForSeconds(sectionTime);
    }
    label.text = signs[0]; 
   }

  #endregion
	
}
