using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject pauseScreenPrefab;
  private GameObject pauseScreen;
  private Canvas canvas;

  #endregion

  #region State Behaviour

  void Awake() {
    pauseScreen = Instantiate(pauseScreenPrefab, transform);
    pauseScreen.SetActive(false);
    canvas = pauseScreen.GetComponent<Canvas>();
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    canvas.sortingLayerName = "Top";
  }

  void OnEnable() {
    EventManager.StartListening<EscapeInput>(OnEscapeInput);
  }

  void OnDisable() {
    EventManager.StopListening<EscapeInput>(OnEscapeInput);
  }

  #endregion

  #region Event Behaviour

  void OnEscapeInput(EscapeInput escapeInput) {
    pauseScreen.SetActive(true);
  }

  #endregion

}
