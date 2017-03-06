using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Models;

public class PlayerBehaviour : MonoBehaviour {

  #region Fields

  public const float MAX_SPEED = 0.1f;
  public static Vector2[] PLAYER_POSITIONS;
 
  private Animator animator;
  private string[] animations = new string[] {"Return01", "Return02"};

  private Vector2 nextPosition;
  private Vector2 enemyPosition;

  #endregion

  #region Mono Behaviour

  void Awake() {
    animator = GetComponent<Animator>();
  }

  void Update() {
    transform.position = Vector2.Lerp(transform.position, nextPosition, MAX_SPEED * Time.timeScale);
  }

  void OnEnable() {
    nextPosition = Config.PLAYER_INITIAL_POSITION;
    EventManager.StartListening<EnemyAttackEvent>(OnEnemyAttackEvent);
    EventManager.StartListening<RightGestureInput>(OnRightGestureInput);
  }

  void OnDisable() {
    EventManager.StopListening<EnemyAttackEvent>(OnEnemyAttackEvent);
    EventManager.StopListening<RightGestureInput>(OnRightGestureInput);
  }

  #endregion

  #region Event Behaviour

  void OnEnemyAttackEvent(EnemyAttackEvent enemyAttackEvent) {
    enemyPosition = enemyAttackEvent.Position;
  }

  void OnRightGestureInput(RightGestureInput rightGestureInput) {
    nextPosition.x = enemyPosition.x;
  }

  #endregion

}
