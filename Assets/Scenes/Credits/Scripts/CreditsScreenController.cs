using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditsScreenController : MonoBehaviour {

  #region Fields

  private Animator anim;
  private AudioSource loungeMusic;

  #endregion

  #region Mono Behaviour

  void Awake() {
    anim = GetComponent<Animator>();
    loungeMusic = GetComponent<AudioSource>();
  }

  void Start() {
    loungeMusic.Play();
  }

  #endregion


}