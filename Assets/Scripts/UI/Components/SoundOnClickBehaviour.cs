using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundOnClickBehaviour : MonoBehaviour {

    #region Fields / Properties

    private AudioSource audioSource;

    #endregion

    #region Mono Behaviour

    void Awake() {
        audioSource = GetComponent<AudioSource>();   
    }

    void OnEnable() {
        InputManager.TapInputEvent += OnTapInputEvent;
    }

    void OnDisable() {
        InputManager.TapInputEvent -= OnTapInputEvent;
    }

    #endregion

    #region Mono Behaviour

    public void OnTapInputEvent() {
        audioSource.Play();
    }

    #endregion

}
