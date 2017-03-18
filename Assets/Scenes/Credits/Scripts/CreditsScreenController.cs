using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CreditsScreenController : MonoBehaviour, IPointerClickHandler {

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

  #region IPointerClickHandler Behaviour

  public void OnPointerClick(PointerEventData e) {
    SceneManager.LoadScene(2);
  }

  #endregion

}