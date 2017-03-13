using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour {

  #region Fields

  [SerializeField] private GameObject leaderboardPrefab;
  private GameObject leaderboard;
  private Canvas canvas;

  #endregion

  #region State Behaviour

  void Awake() {
    leaderboard = Instantiate(leaderboardPrefab, transform);
    leaderboard.SetActive(false);
    canvas = leaderboard.GetComponent<Canvas>();
    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    canvas.sortingLayerName = "UI";
  }

  void OnEnable() {
    EventManager.StartListening<LeaderboardEvent>(OnLeaderboardEvent);
  }

  void OnDisable() {
    EventManager.StopListening<LeaderboardEvent>(OnLeaderboardEvent);
  }

  #endregion

  #region Event Behaviour

  void OnLeaderboardEvent(LeaderboardEvent leaderboardEvent) {
    leaderboard.GetComponent<LeaderboardBehaviour>().Play();
  }

  #endregion

}
