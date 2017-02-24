using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  #region Fields

  [SerializeField] GameObject jetPrefab;
  private ParticleSystemController particleSystemController;

  private PlayerWeapon playerWeapon;

  #endregion

  #region Mono Behaviour

  void Awake() {
    particleSystemController = Instantiate(jetPrefab, transform).GetComponent<ParticleSystemController>();
    playerWeapon = GetComponent<PlayerWeapon>();
  }

  #endregion

  #region Public Behaviour

  public void Initialize() {
    particleSystemController.enabled = true;
    playerWeapon.enabled = true;
  }

  #endregion

}
