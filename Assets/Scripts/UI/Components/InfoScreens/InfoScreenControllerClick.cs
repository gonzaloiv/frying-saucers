using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoScreenControllerClick : MonoBehaviour, IInfoScreenController, IPointerClickHandler {

    #region Fields

    [SerializeField] private bool infoScreen = true;
    [SerializeField] private bool nextScreen = false;

    private AudioSource audioSource;
    private TutorialScreenController tutorialScreenController;

    #endregion

    #region Mono Behaviour

    void Awake () {
        audioSource = GetComponent<AudioSource>();
    }

    #endregion

    #region Public Behaviour

    public void Initialize (TutorialScreenController tutorialScreenController) {
        this.tutorialScreenController = tutorialScreenController;
    }

    public void Play () {
        TimeManager.StopTime();  
        gameObject.SetActive(true); 
        audioSource.Play();
    }

    public void Stop () {
        gameObject.SetActive(false); 
        TimeManager.StartTime();
    }

    #endregion

    #region IPointerClickHandler

    public void OnPointerClick (PointerEventData e) {
        Stop();      
        if (infoScreen) {
            if (nextScreen)
                tutorialScreenController.NextInfoScreen();
        } else {
            tutorialScreenController.NextErrorScreen();    
        }
    }

    #endregion

}
