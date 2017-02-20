using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour {

  #region Fields

  private SpriteRenderer spriteRenderer;

  #endregion

  #region Mono Behaviour

  void Awake() {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  #endregion

  #region Public Behaviour

  public void Disable() {
    Debug.Log("Disable UFO: " + transform.position);
    gameObject.SetActive(false);
  }

  #endregion

}
