using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverScreenBehaviour : MonoBehaviour, IPointerClickHandler {

  #region Mono Behaviour

  void OnEnable() {
    EventManager.TriggerEvent(new GameOverEvent());
    TimeManager.StopTime();
  }

  void OnDisable() {
    TimeManager.StartTime();
  }

  #endregion

  #region IPointerClickHandler

  public void OnPointerClick(PointerEventData eventData) {    
    EventManager.TriggerEvent(new NewGameEvent());
    gameObject.SetActive(false);        
  }

  #endregion
	
}
