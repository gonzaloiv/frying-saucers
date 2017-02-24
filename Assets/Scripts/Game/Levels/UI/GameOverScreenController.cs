using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject gameOverScrenPrefab;
  private GameObject gameOverScreen;
  private Canvas canvas;

  #endregion

  #region State Behaviour

  void Awake() {
    gameOverScreen = Instantiate(gameOverScrenPrefab, transform);
    gameOverScreen.SetActive(false);
    canvas = gameOverScreen.GetComponent<Canvas>();
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    canvas.sortingLayerName = "Top";
  }

  void OnEnable() {
    EventManager.StartListening<ReturnInput>(OnReturnInput);
  }

  void OnDisable() {
    EventManager.StopListening<ReturnInput>(OnReturnInput);
  }

  #endregion

  #region Event Behaviour

  void OnReturnInput(ReturnInput returnInput) {
    gameOverScreen.SetActive(true);
  }

  #endregion

}
