using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

  #region Mono Behaviour

	void Awake () {
		DontDestroyOnLoad(gameObject);
	}

  #endregion
	
}
