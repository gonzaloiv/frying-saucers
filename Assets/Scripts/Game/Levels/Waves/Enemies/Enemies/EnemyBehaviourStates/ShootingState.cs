using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyBehaviourStates {

  public class ShootingState : BaseState {
    
    #region Fields

    IEnumerator shootingRoutine;
    Vector2 shootingPosition;

    #endregion
    
    #region State Behaviour

    public override void Enter() {
      base.Enter();
      shootingPosition = Board.EmptyEnemyShotPosition();
      shootingRoutine = ShootingRoutine();
      StartCoroutine(shootingRoutine);
    }

    public override void Exit() {
      base.Exit();
    }

    public override void Play() {
      transform.position = Vector2.Lerp(transform.position, shootingPosition, Config.ENEMY_MAX_SPEED * Time.deltaTime);
    }

    protected override void AddListeners() {
      EventManager.StartListening<GestureInput>(OnGestureInput);
    }

    protected override void RemoveListeners() {
      EventManager.StopListening<GestureInput>(OnGestureInput);
    }

    #endregion

    #region Event Behaviour

    void OnGestureInput(GestureInput gestureInput) {
      if (gestureInput.Score < Config.GESTURE_MIN_SCORE) {       // Low score
        EventManager.TriggerEvent(new WrongGestureInput(gestureInput));
      } else {
        if ((int) gestureInput.Type != (int) enemyController.Enemy.EnemyType) { // Wrong gesture
          EventManager.TriggerEvent(new WrongGestureInput(gestureInput));
        } else { // Hit
          hit = true;
          RemoveListeners();
          enemyController.DisableRoutine();
          EventManager.TriggerEvent(new RightGestureInput(gestureInput));
        }
      }
    }

    #endregion

    #region Private Behaviour

    private IEnumerator ShootingRoutine() {

      EventManager.TriggerEvent(new EnemyAttackEvent(enemyController.Enemy.EnemyType, shootingPosition, routineTime));
      animator.Play("Shooting");

      yield return new WaitForSeconds(routineTime / 4 * 3);
      EventManager.TriggerEvent(new EnemyShotEvent(transform.position));
      transform.rotation = QuaternionToPlayer();
      GetComponent<SpriteRenderer>().flipY = true;
      laser.Play();

      yield return new WaitForSeconds(routineTime / 4);
      GetComponent<SpriteRenderer>().flipY = false;

    }

    private Quaternion QuaternionToPlayer() {
      Quaternion quaternion = Quaternion.identity;
      Vector2 moveDirection = (Vector2) transform.position - (Vector2) player.transform.position; 
      if (moveDirection != Vector2.zero) {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        quaternion = Quaternion.AngleAxis(angle + 90, Vector3.forward);
      }
      return quaternion;
    }

    #endregion

  }

}