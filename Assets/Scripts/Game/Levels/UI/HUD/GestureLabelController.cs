using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureLabelController : MonoBehaviour {

  #region Fields

  private Image[] gestures; // Same order as EnemyType
  private Image currentGesture;

  #endregion

  #region Mono Behaviour

  void Awake() {
    gestures = GetComponentsInChildren<Image>();
  }

  void OnEnable() {
    EventManager.StartListening<EnemyShotEvent>(OnEnemyShotEvent);  
  }

  void OnDisable() {
    EventManager.StopListening<EnemyShotEvent>(OnEnemyShotEvent);
  }

  #endregion

  #region Event Behaviour

  void OnEnemyShotEvent(EnemyShotEvent enemyShotEvent) {
    if(currentGesture != null)
      currentGesture.enabled = false;
    currentGesture = gestures[(int) enemyShotEvent.EnemyType];
    currentGesture.enabled = true;
  }

  #endregion
	
}
