using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class InfoScreenController : MonoBehaviour, IInfoScreenController, IPointerClickHandler {

  #region Fields

  private Animator anim;
  private AudioSource audioSource;
  private AudioClip[] audioClips;

  #endregion

  #region Mono Behaviour

  void Awake() {
    anim = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
  }

  #endregion

  #region Public Behaviour

  public void Play() {
    TimeManager.StopTime();  
    gameObject.SetActive(true); 
  }

  public void Stop() {
    gameObject.SetActive(false); 
    TimeManager.StartTime ();
  }

  #endregion

  #region IPointerClickHandler

  public void OnPointerClick(PointerEventData e) {    
    Debug.Log("HALLO");
    Stop();        
  }

  #endregion


}
