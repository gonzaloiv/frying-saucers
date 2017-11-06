using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates {

    public class EnemyAttackState : BaseState {

        #region Fields / Properties

        private Vector2 enemyPosition;

        #endregion

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            enemyPosition = GetEnemyPosition();
        }

        public override void Play () {
            base.Play();
            transform.rotation = QuaternionToEnemy();
            transform.position = Vector2.Lerp(transform.position, new Vector2(enemyPosition.x, transform.position.y), GameConfig.PlayerMaxSpeed / 4 * Time.deltaTime);
        }

        public override void Exit () {
            base.Exit();
        }

        public void OnEnemyShotEvent (EnemyShotEventArgs enemyShotEventArgs) {
            StartCoroutine(WaitForGestureRoutine());
        }

        public void OnRightGestureInput (GestureInputEventArgs gestureInputEventArgs) {
            laserPS.Play();
            playerController.InvokePlayerShotEvent();
            playerController.ToIdleState();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            EnemyController.EnemyShotEvent += OnEnemyShotEvent;
            GestureManager.RightGestureInputEvent += OnRightGestureInput;
        }

        protected override void RemoveListeners () {
            EnemyController.EnemyShotEvent -= OnEnemyShotEvent;
            GestureManager.RightGestureInputEvent -= OnRightGestureInput;
        }

        #endregion

        #region Private Behaviour

        private IEnumerator WaitForGestureRoutine () {
            yield return new WaitForSeconds(0.1f);
            playerController.ToRespawnState();
        }

        #endregion

    }

}