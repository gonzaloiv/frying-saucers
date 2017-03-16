using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

  #region Fields

  private Animator anim;

  #endregion

  #region Mono Behaviour

  void Awake() {
    anim = GetComponent<Animator>();
  }

  void OnEnable() {
    EventManager.StartListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StartListening<WrongGestureInput>(OnWrongGestureInput);
    EventManager.StartListening<EnemyHitEvent>(OnEnemyHitEvent);
    EventManager.StartListening<PlayerHitEvent>(OnPlayerHitEvent);
    EventManager.StartListening<GameOverEvent>(OnGameOverEvent);
  }

  void OnDisable() {
    EventManager.StopListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StopListening<WrongGestureInput>(OnWrongGestureInput);
    EventManager.StopListening<EnemyHitEvent>(OnEnemyHitEvent);
    EventManager.StopListening<PlayerHitEvent>(OnPlayerHitEvent);
    EventManager.StopListening<GameOverEvent>(OnGameOverEvent);
  }

  #endregion

  #region Event Behaviour

  void OnRightGestureInput(RightGestureInput rightGestureInput) {
  }

  void OnWrongGestureInput(WrongGestureInput wrongGestureInput) {
  }

  void OnEnemyHitEvent(EnemyHitEvent EnemyHitEvent) {

  }

  void OnPlayerHitEvent(PlayerHitEvent playerHitEvent) {
  }

  void OnGameOverEvent(GameOverEvent gameOverEvent) {
  }

  #endregion

}
