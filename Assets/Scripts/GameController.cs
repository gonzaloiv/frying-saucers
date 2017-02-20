using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

  #region Mono Behaviour

  [SerializeField] private GameObject levelPrefab;
  private LevelController levelController;

  #endregion

	#region Mono Behaviour

  void Awake() {
    levelController = Instantiate(levelPrefab, transform).GetComponent<LevelController>();
  }

  void Start() {
    levelController.Initialize();
  }

  #endregion

}
