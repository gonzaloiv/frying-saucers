using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleSystemController : MonoBehaviour {

	#region Fields

	private List<ParticleSystem> waves = new List<ParticleSystem>();

	#endregion

	#region Mono Behaviour

	void Awake() {
    waves = GetComponentsInChildren<ParticleSystem>().ToList();
	}

	#endregion

  #region Public Behaviour

  public void Play(Vector2 position) {
    waves.ForEach(x => { x.Play(); x.transform.position = position; x.transform.localScale = Vector3.one; } );
  }

  #endregion


}
