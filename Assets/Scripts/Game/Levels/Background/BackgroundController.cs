using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	#region Fields

  [SerializeField] private GameObject starsPrefab;
  private ParticleSystem stars;

  #endregion

  #region Mono Behaviour

  void Awake() {
    stars = Instantiate(starsPrefab, transform).GetComponent<ParticleSystem>();
  }

  #endregion

  #region Public Behaviour

  public void NewLevel() {
    stars.Play();
  }

  #endregion
	
}
