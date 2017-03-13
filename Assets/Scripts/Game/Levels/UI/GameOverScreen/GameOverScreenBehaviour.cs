using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverScreenBehaviour : MonoBehaviour, IPointerClickHandler {

  #region Fields

  private InputManager inputManager;
  private Animator anim;
  private bool active = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    anim = GetComponent<Animator>();
    inputManager = GameObject.FindObjectOfType<InputManager>();

  }

  #endregion

  #region IPointerClickHandler

  public void OnPointerClick(PointerEventData eventData) {    
    if(active)
      anim.Play("FadeOut");
  }

  #endregion

  #region Public Behaviour

  public void Play() {
    gameObject.SetActive(true);
    inputManager.enabled = false;
    active = true;
    anim.Play("FadeIn");
  }

  public void Disable() {
    gameObject.SetActive(false);       
    inputManager.enabled = true;
    active = false;
    EventManager.TriggerEvent(new LeaderboardEvent());
  }


  #endregion
	
}