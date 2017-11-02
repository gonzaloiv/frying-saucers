﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyBehaviourStates {

    public class ShootingState : BaseState {
    
        #region Fields

        private Vector2 shootingPosition;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            shootingPosition = Board.EmptyEnemyShotPosition();
            StartCoroutine(ShootingRoutine());
        }

        public override void Exit () {
            base.Exit();
        }

        public override void Play () {
            transform.position = Vector2.Lerp(transform.position, shootingPosition, GameConfig.EnemyMaxSpeed * Time.deltaTime);
        }

        public void OnGestureInputEvent (GestureInputEventArgs gestureInputEventArgs) {
            if (gestureInputEventArgs.Score < GameConfig.GestureMinScore) { // Low score
                enemyBehaviour.InvokeWrongGestureInputEvent(new WrongGestureInputEventArgs(gestureInputEventArgs));
            } else {
                if ((int) gestureInputEventArgs.Type != (int) enemyController.Enemy.EnemyType) { // Wrong gesture
                    enemyBehaviour.InvokeWrongGestureInputEvent(new WrongGestureInputEventArgs(gestureInputEventArgs));
                } else { // Hit
                    hit = true;
                    RemoveListeners();
                    enemyController.DisableRoutine();
                    enemyBehaviour.InvokeRightGestureInputEvent(new RightGestureInputEventArgs(gestureInputEventArgs));
                }
            }
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            GestureManager.GestureInputEvent += OnGestureInputEvent;
        }

        protected override void RemoveListeners () {
            GestureManager.GestureInputEvent -= OnGestureInputEvent;
        }

        #endregion

        #region Private Behaviour

        private IEnumerator ShootingRoutine () {

            enemyBehaviour.InvokeEnemyAttackEvent(new EnemyAttackEventArgs(enemyController.Enemy.EnemyType, shootingPosition, routineTime));
            animator.Play("Shooting");

            yield return new WaitForSeconds(routineTime / 4 * 3);
            enemyBehaviour.InvokeEnemyShotEvent(new EnemyShotEventArgs(transform.position));
            transform.rotation = QuaternionToPlayer();
            GetComponent<SpriteRenderer>().flipY = true;
            laser.Play();

            yield return new WaitForSeconds(routineTime / 4);
            GetComponent<SpriteRenderer>().flipY = false;

        }

        private Quaternion QuaternionToPlayer () {
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