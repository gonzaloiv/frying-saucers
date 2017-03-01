using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScreenBehaviour : MonoBehaviour, IPointerClickHandler {

  #region Mono Behaviour

  void OnEnable() {
    TimeManager.StopTime();
  }

  void OnDisable() {
    TimeManager.StartTime();
  }

  #endregion

  #region IPointerClickHandler

  public void OnPointerClick(PointerEventData eventData) {    
    gameObject.SetActive(false);        
  }

  #endregion
	
}
