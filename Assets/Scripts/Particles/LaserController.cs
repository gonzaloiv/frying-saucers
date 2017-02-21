using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

  #region Fields

  private ParticleSystem ps;

  #endregion

  #region Mono Behaviour

  void Awake() {
    ps = GetComponent<ParticleSystem>();
  }

  #endregion

	#region Public Behaviour

  public void Emit() {
    ps.Play();
  }

  #endregion
	
}
