using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverScreenBehaviour : MonoBehaviour, IPointerClickHandler {

  #region Fields

  private Animator anim;
  private bool active = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    anim = GetComponent<Animator>();
  }

  void OnEnable() {
    active = true;
    anim.Play("FadeIn");
  }

  #endregion

  #region IPointerClickHandler

  public void OnPointerClick(PointerEventData eventData) {    
    Debug.Log("OnPointerClick");
    if(active)
      anim.Play("FadeOut");
  }

  #endregion

  #region Public Behaviour

  public void Disable() {
    gameObject.SetActive(false);       
    active = false;
    EventManager.TriggerEvent(new NewGameEvent());
  }

  #endregion
	
}