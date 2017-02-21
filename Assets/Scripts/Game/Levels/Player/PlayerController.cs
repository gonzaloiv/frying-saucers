using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  #region Fields

  private PlayerWeapon playerWeapon;

  #endregion

  #region Mono Behaviour

  void Awake() {
    playerWeapon = GetComponent<PlayerWeapon>();
  }

  #endregion

  #region Public Behaviour

  public void Initialize() {
    playerWeapon.enabled = true;
  }

  #endregion

}
