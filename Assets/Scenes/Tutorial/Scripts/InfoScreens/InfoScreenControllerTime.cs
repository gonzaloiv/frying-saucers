using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoScreenControllerTime : MonoBehaviour, IInfoScreenController {

  #region Fields

  [SerializeField] private bool nextScreen = false;
  [SerializeField] private float routineTime = 1;
  [SerializeField] private bool stopTime = false;
  [SerializeField] private bool infoScreen = true;

  private AudioSource audioSource;
  private AudioClip[] audioClips;
  private InfoController infoController;

  #endregion

  #region Mono Behaviour

  void Awake() {
    audioSource = GetComponent<AudioSource>();
  }

  #endregion

  #region Public Behaviour

  public void Initialize(InfoController infoController) {
    this.infoController = infoController;
  }

  public void Play() {
    gameObject.SetActive(true);
    StartCoroutine(InfoScreenRoutine()); 
    audioSource.Play();
  }

  public void Stop() {
    gameObject.SetActive(false); 
  }

  #endregion

  #region Private Behaviour

  public IEnumerator InfoScreenRoutine() {    
    if (stopTime)
      TimeManager.StopTime();
    yield return TimeManager.WaitForRealTime(routineTime);
    TimeManager.StartTime();
    if (infoScreen) {
      if (nextScreen)
        infoController.NextInfoScreen();  
    } else {
      infoController.NextErrorScreen();  
    }
    Stop();
  }

  #endregion


}
