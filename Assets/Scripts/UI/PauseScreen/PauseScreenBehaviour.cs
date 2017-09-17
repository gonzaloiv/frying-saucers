using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScreenBehaviour : MonoBehaviour, IPointerClickHandler {

  #region Fields

  private InputManager inputManager;

  #endregion

  #region Mono Behaviour

  void Awake() {
    inputManager = GameObject.FindObjectOfType<InputManager>();
  }

  void OnEnable() {
    inputManager.enabled = false;
    TimeManager.StopTime();
  }

  void OnDisable() {
    inputManager.enabled = true;
    TimeManager.StartTime();
  }

  #endregion

  #region IPointerClickHandler

  public void OnPointerClick(PointerEventData eventData) {    
    gameObject.SetActive(false);        
  }

  #endregion
	
}
