using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyBehaviourStates {

    public class ShootState : BaseState {
    
        #region Fields

        private Vector2 shootingPosition;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            shootingPosition = Board.EmptyEnemyShotPosition();
            StartCoroutine(ShootRoutine());
        }

        public override void Play () {
            transform.position = Vector2.Lerp(transform.position, shootingPosition, GameConfig.EnemyMaxSpeed * Time.deltaTime);
        }

        public void OnRightGestureInputEvent (GestureInputEventArgs gestureInputEventArgs) {
            enemyController.ToDisableState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            GestureManager.RightGestureInputEvent += OnRightGestureInputEvent;
        }

        protected override void RemoveListeners () {
            GestureManager.RightGestureInputEvent -= OnRightGestureInputEvent;
        }

        #endregion

        #region Private Behaviour

        private IEnumerator ShootRoutine () {
            enemyController.InvokeEnemyAttackEvent(new EnemyAttackEventArgs(enemyController.Enemy.EnemyType, shootingPosition, enemy.ShootRoutineTime));
            anim.Play("Shooting");
            yield return new WaitForSeconds(enemy.ShootRoutineTime / 4 * 3);
            enemyController.InvokeEnemyShotEvent(new EnemyShotEventArgs(transform.position));
            transform.rotation = QuaternionToPlayer();
            GetComponent<SpriteRenderer>().flipY = true;
            laserPS.Play();
            yield return new WaitForSeconds(enemy.ShootRoutineTime / 4);
            GetComponent<SpriteRenderer>().flipY = false;
            enemyController.ToIdleState();
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