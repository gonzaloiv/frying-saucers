using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditsScreenController : MonoBehaviour {

  #region Fields

  private AudioSource loungeMusic;

  #endregion

  #region Mono Behaviour

  void Awake() {
    loungeMusic = GetComponent<AudioSource>();
  }

  void Start() {
    loungeMusic.Play();
  }

  #endregion


}