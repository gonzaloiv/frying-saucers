using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureLabelController : MonoBehaviour {

  #region Fields

  private Image[] gestures; // Same order as EnemyType
  private Image currentGesture;
  private int currentGestureIndex = 0;
  private bool shooting = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    gestures = GetComponentsInChildren<Image>();
  }

  void FixedUpdate() {
    if (!shooting) {
      EnableImage(currentGestureIndex);
      currentGestureIndex = currentGestureIndex == gestures.Length - 1 ? 0 : currentGestureIndex + 1;
    }
  }

  void OnEnable() {
    EventManager.StartListening<EnemyAttackEvent>(OnEnemyAttackEvent);
  }

  void OnDisable() {
    EventManager.StopListening<EnemyAttackEvent>(OnEnemyAttackEvent);
  }

  #endregion

  #region Event Behaviour

  void OnEnemyAttackEvent(EnemyAttackEvent enemyShotEvent) {
    StartCoroutine(EnemyAttackEventRoutine((int) enemyShotEvent.EnemyType));
  }

  #endregion

  #region Private Behaviour

  private IEnumerator EnemyAttackEventRoutine(int index) {
    shooting = true;
    EnableImage(index);
    yield return new WaitForSeconds(1);
    shooting = false;
  }

  private void EnableImage(int index) {
    if(currentGesture != null)
      currentGesture.enabled = false;
    currentGesture = gestures[index];
    currentGesture.enabled = true;
  }
    
  #endregion
	
}
