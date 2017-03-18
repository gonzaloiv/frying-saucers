using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Models;

public class PlayerBehaviour : MonoBehaviour {

  #region Fields

  public const float MAX_SPEED = 10f;
  public static Vector2[] PLAYER_POSITIONS;
 
  private Collider2D collider;

  private Vector2 nextPosition;
  private Vector2 enemyPosition;
  private bool rightGesture;

  #endregion

  #region Mono Behaviour

  void Awake() {
    collider = GetComponent<Collider2D>();
  }

  void Update() {
    transform.position = Vector2.Lerp(transform.position, nextPosition, MAX_SPEED * Time.deltaTime);
  }

  void OnEnable() {
    nextPosition = Config.PLAYER_INITIAL_POSITION;
    EventManager.StartListening<EnemyAttackEvent>(OnEnemyAttackEvent);
    EventManager.StartListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StartListening<EnemyShotEvent>(OnEnemyShotEvent);
  }

  void OnDisable() {
    EventManager.StopListening<EnemyAttackEvent>(OnEnemyAttackEvent);
    EventManager.StopListening<RightGestureInput>(OnRightGestureInput);
    EventManager.StopListening<EnemyShotEvent>(OnEnemyShotEvent);
  }

  #endregion

  #region Event Behaviour

  void OnEnemyAttackEvent(EnemyAttackEvent enemyAttackEvent) {
    enemyPosition = enemyAttackEvent.Position;
    rightGesture = false;
  }

  void OnEnemyShotEvent(EnemyShotEvent enemyShotEvent) {
    if(rightGesture)
      StartCoroutine(EvasionRoutine());
  }

  void OnRightGestureInput(RightGestureInput rightGestureInput) {
    nextPosition.x = enemyPosition.x;
    rightGesture = true;
  }

  #endregion

  #region Private Behaivour

  private IEnumerator EvasionRoutine() {
    nextPosition.x = nextPosition.x + new float[]{-2.5f, 2.5f}[Random.Range(0, 2)];
    collider.enabled = false;
    yield return new WaitForSeconds(1);
    collider.enabled = true;
    nextPosition.x = 0;
    rightGesture = false;
  }

  #endregion

}
  